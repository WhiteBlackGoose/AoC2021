/*

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19042.1348 (20H2/October2020Update)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT

|   Method        |         Mean |       Error |      StdDev | Ratio |   Gen 0 | Allocated |
|---------------- |-------------:|------------:|------------:|------:|--------:|----------:|
| PartI_v1        | 257,175.5 ns | 5,019.23 ns | 9,303.46 ns | 1.000 | 20.7520 |  65,424 B |
| PartI_v2        |  80,643.7 ns | 1,542.59 ns | 1,894.44 ns | 0.312 |       - |         - |
| PartI_v3        |   4,148.2 ns |   143.58 ns |   421.10 ns | 0.015 |       - |         - |
| PartI_v4        |   3,558.7 ns |    97.39 ns |   285.62 ns | 0.014 |       - |         - |
| PartI_v5        |     846.3 ns |    16.79 ns |    37.91 ns | 0.003 |       - |         - |
| PartI_v5_unroll |     758.8 ns |    19.42 ns |    57.25 ns | 0.003 |       - |         - |

|    Method |     Mean |     Error |    StdDev | Ratio |   Gen 0 | Allocated |
|---------- |---------:|----------:|----------:|------:|--------:|----------:|
| PartII_v1 | 4.787 ms | 0.1272 ms | 0.3730 ms |  1.00 | 23.4375 |     73 KB |

*/

#define BDN
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CodegenAnalysis.Benchmarks;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;


// Verify
#if DEBUG
var b = new ToBench();
b.Setup();
Console.WriteLine(b.PartI_v1());
Console.WriteLine(b.PartI_v2());
Console.WriteLine(b.PartI_v3());
Console.WriteLine(b.PartI_v4());
Console.WriteLine(b.PartI_v5());
Console.WriteLine(b.PartI_v5_unroll());
// Console.WriteLine(b.PartII_v1());
#endif

// Get the codegen analysis
#if !DEBUG && !BDN
CodegenBenchmarkRunner.Run<ToBench>();
#endif

// Get the perf results
#if !DEBUG && BDN
BenchmarkRunner.Run<ToBench>();
#endif

[MemoryDiagnoser]

[CAColumn(CAColumn.Branches),
 CAColumn(CAColumn.Calls),
 CAColumn(CAColumn.StaticStackAllocations),
 CAColumn(CAColumn.CodegenSize),
 CAColumn(CAColumn.ILSize)]

[CAExport(Export.Md),
 CAExport(Export.Html)]
public class ToBench
{
    private string input;
    private byte[] inputBytes;

    public ToBench()
    {
        Setup();
    }

    public void Setup()
    {
#if DEBUG || !BDN
        var path = @"..\..\..\input.txt";
#else
        var path = @"..\..\..\..\..\..\..\input.txt";
#endif
        input = File.ReadAllText(path);
        inputBytes = File.ReadAllBytes(path);
    }
    
    private static readonly Vector128<ushort> CharZeros = Vector128.Create((ushort)'0');
    private static readonly Vector128<byte> CharZerosByte = Vector128.Create((byte)'0');
    private static readonly Vector256<ushort> CharZeros256 = Vector256.Create((ushort)'0');

    // dst[i] = dst[i] + (a[i] - '0')
    private static unsafe void SubtractChar0Add24BytesUShort(ushort* a, ushort* dst)
    {
        var vecA = *(Vector128<ushort>*)a;
        var vecB = *(Vector128<ushort>*)dst;
        var subtracted = Avx2.Subtract(vecA, CharZeros);
        var added = Avx2.Add(vecB, subtracted);
        *(Vector128<ushort>*)dst = added;

        var tailA = a + 8;
        var tailB = dst + 8;
        var toAdd = *(ulong*)tailA - (((ulong)'0' << 48) + ((ulong)'0' << 32) + ((ulong)'0' << 16) + ((ulong)'0' << 0));
        *(ulong*)tailB += toAdd;
    }

    //[Benchmark(Baseline = true)]
    public int PartI_v1()
    {
        var lines = input.Split("\n")[0..^1];

        var ga = 0;
        var co = 0;

        for (int i = 0; i < lines[0].Length; i++)
        {
            ga = ga * 2;
            co = co * 2;
            if (lines.Select(s => s[i]).Count(c => c == '0') > lines.Length / 2)
            {
                co++;
            }
            else
            {
                ga++;
            }
        }

        return co * ga;
    }

    //[Benchmark]
    public unsafe int PartI_v2()
    {
        Span<int> counts = stackalloc int[12];

        var len = input.Length / 13;

        var id = 0;
        var overall = 0;
        foreach (var c in input)
        {
            if (c is '1' or '0')
            {
                counts[id] += c - '0';
                id++;
                overall++;
            }
            if (id == 12)
                id = 0;
        }

        var ga = 0;
        var co = 0;
        var half = overall / 12 / 2;
        foreach (var c in counts)
        {
            ga = ga * 2;
            co = co * 2;
            if (c >= half)
                ga++;
            else
                co++;
        }

        return co * ga;
    }

    //[Benchmark]
    public unsafe int PartI_v3()
    {        
        var counts = stackalloc ushort[12];

        var len = input.Length / 13;

        fixed (char* c = input)
        {
            for (int i = 0; i < len; i++)
                SubtractChar0Add24BytesUShort((ushort*)(c + i * 13), counts);
        }

        var ga = 0;
        var co = 0;
        var half = len / 2;
        for (var i = 0; i < 12; i++)
        {
            ga = ga * 2;
            co = co * 2;
            if (counts[i] >= half)
                ga++;
            else
                co++;
        }

        return co * ga;
    }

    //[Benchmark]
    public unsafe int PartI_v4()
    {
        var counts = Vector256.Create((ushort)0);

        var len = input.Length / 13;

        fixed (char* c = input)
        {
            for (int i = 0; i < len - 1; i++)
            {
                var src = (Vector256<ushort>*)(c + i * 13);
                var sub = Avx2.Subtract(*src, CharZeros256);
                counts = Avx2.Add(sub, counts);
            }
        }

        var ga = 0;
        var co = 0;
        var half = len / 2;
        for (var i = 0; i < 12; i++)
        {
            ga = ga * 2;
            co = co * 2;
            if (counts.GetElement(i) + (input[(len - 1) * 13 + i] - '0') >= half)
                ga++;
            else
                co++;
        }

        return co * ga;
    }

    [Benchmark, CAAnalyze, SkipLocalsInit]
    public unsafe int PartI_v5()
    {
        var len = input.Length / 13;
        Span<Vector128<byte>> counters = stackalloc Vector128<byte>[4];
        counters.Clear();

        const int BatchSize = 255;

        fixed (byte* c = inputBytes)
        {
            for (int o = 0; o < len / BatchSize + 1; o++)
            {
                var counter = Vector128.Create((byte)0);
                var upper = Math.Min(len - o * BatchSize, BatchSize) + o * BatchSize;
                var charZeros = Vector128.Create((byte)'0');

                var upperPtr = upper * 13 + c;
                //for (int i = o * 255; i < upper; i++)
                for (var ptr = o * BatchSize * 13 + c; ptr < upperPtr; ptr += 13)
                {
                    var src = (Vector128<byte>*)ptr;
                    var sub = Avx2.Subtract(*src, charZeros);
                    counter = Avx2.Add(sub, counter);
                }
                counters[o] = counter;
            }
        }

        /*
       fixed (byte* c = inputBytes)
       {
           for (int o = 0; o < len / 255 + 1; o++)
           {
               var counter = Vector128.Create((int)0);
               var upper = Math.Min(len - o * 255, 255) + o * 255;
               for (int i = o * 255; i < upper; i++)
               {
                   var src = (Vector128<int>*)(c + i * 13);
                   var sub = Avx2.Subtract(*src, CharZerosByte.AsInt32());
                   counter = Avx2.Add(sub, counter);
               }
               counters[o] = counter.AsByte();
           }
       }*/

        var ga = 0;
        var co = 0;
        var half = len / 2;
        for (var i = 0; i < 12; i++)
        {
            ga = ga * 2;
            co = co * 2;
            var overall = 0;
            foreach (var vec in counters)
                overall += vec.GetElement(i);
            if (overall + (input[(len - 1) * 13 + i] - '0') >= half)
                ga++;
            else
                co++;
        }

        return co * ga;
    }

    [Benchmark, CAAnalyze, SkipLocalsInit]
    public unsafe int PartI_v5_unroll()
    {
        var len = input.Length / 13;
        Span<Vector128<byte>> counters = stackalloc Vector128<byte>[4];
        counters.Clear();

        const int BatchSize = 252;

        fixed (byte* c = inputBytes)
        {
            for (int o = 0; o < len / BatchSize + 1; o++)
            {
                var counter = Vector128.Create((byte)0);
                var upper = Math.Min(len - o * BatchSize, BatchSize) + o * BatchSize;
                var charZeros = Vector128.Create((byte)'0');

                var upperPtr = upper * 13 + c;
                for (var ptr = o * BatchSize * 13 + c; ptr < upperPtr; ptr += 13)
                {
                    var src = (Vector128<byte>*)ptr;
                    var sub = Avx2.Subtract(*src, charZeros);
                    counter = Avx2.Add(sub, counter);
                    ptr += 13;
                    src = (Vector128<byte>*)ptr;
                    sub = Avx2.Subtract(*src, charZeros);
                    counter = Avx2.Add(sub, counter);
                    ptr += 13;
                    src = (Vector128<byte>*)ptr;
                    sub = Avx2.Subtract(*src, charZeros);
                    counter = Avx2.Add(sub, counter);
                    ptr += 13;
                    src = (Vector128<byte>*)ptr;
                    sub = Avx2.Subtract(*src, charZeros);
                    counter = Avx2.Add(sub, counter);
                }
                counters[o] = counter;
            }
        }

        var ga = 0;
        var co = 0;
        var half = len / 2;
        for (var i = 0; i < 12; i++)
        {
            ga = ga * 2;
            co = co * 2;
            var overall = 0;
            foreach (var vec in counters)
                overall += vec.GetElement(i);
            if (overall + (input[(len - 1) * 13 + i] - '0') >= half)
                ga++;
            else
                co++;
        }

        return co * ga;
    }



    // [Benchmark]
    public int PartII_v1()
    {
        var input = this.input.Split("\n")[0..^1];
        var sel0 = (IEnumerable<string>)input;
        var sel1 = (IEnumerable<string>)input;

        for (int i = 0; i < input[0].Length; i++)
        {
            var li = i;
            if (sel0.Count() > 1)
            if (sel0.Select(s => s[i]).Count(c => c == '0') > sel0.Count() / 2)
            {

                sel0 = sel0.Where(c => c[li] == '0');
            }
            else
            {
                sel0 = sel0.Where(c => c[li] == '1') is var notEmpty && notEmpty.Any() ? notEmpty : sel0;
            }

            if (sel1.Count() > 1)
            if (sel1.Select(s => s[i]).Count(c => c == '0') > sel1.Count() / 2)
            {
                sel1 = sel1.Where(c => c[li] == '1') is var notEmpty && notEmpty.Any() ? notEmpty : sel1;
            }
            else
        
                sel1 = sel1.Where(c => c[li] == '0');
        }

        return Convert.ToInt32(sel0.Single(), 2) * Convert.ToInt32(sel1.Single(), 2);
    }
}