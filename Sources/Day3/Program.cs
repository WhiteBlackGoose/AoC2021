/*

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19042.1348 (20H2/October2020Update)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT

|   Method |       Mean |     Error |     StdDev | Ratio | RatioSD |   Gen 0 | Allocated |
|--------- |-----------:|----------:|-----------:|------:|--------:|--------:|----------:|
| PartI_v1 | 262.791 us | 7.0995 us | 20.8215 us |  1.00 |    0.00 | 20.5078 |  65,424 B |
| PartI_v2 |  88.051 us | 1.7548 us |  3.7016 us |  0.34 |    0.03 |       - |         - |
| PartI_v3 |   4.258 us | 0.1130 us |  0.3332 us |  0.02 |    0.00 |       - |         - |

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
// Console.WriteLine(b.PartII());
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
public class ToBench
{
    private string input;

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
    }
    
    private static readonly Vector128<ushort> CharZeros = Vector128.Create((ushort)'0');

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

    [Benchmark(Baseline = true)]
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

    [Benchmark]
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

    [Benchmark]
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

    

    // [Benchmark]
    public int PartII()
    {
        return 0;
        /*
        var sel0 = (IEnumerable<string>)input;
        var sel1 = (IEnumerable<string>)input;
        var gamma = "";
        var epsilon = "";
        var gammaInt = 0;
        var epsilonInt = 0;

        var gammaStop = false;
        var epsStop = false;

        for (int i = 0; i < input[0].Length; i++)
        {
            gammaStop = sel0.Where(s => s.StartsWith(gamma)).Count() <= 1;
            epsStop = sel1.Where(s => s.StartsWith(epsilon)).Count() <= 1;
            var li = i;
            if (sel0.Count() > 1)
            if (sel0.Select(s => s[i]).Count(c => c == '0') > sel0.Count() / 2)
            {

                sel0 = sel0.Where(c => c[li] == '0');
                // sel1 = sel1.Where(c => c[li] == '1');
            }
            else
            {
                sel0 = sel0.Where(c => c[li] == '1') is var notEmpty && notEmpty.Any() ? notEmpty : sel0;
                // sel1 = sel1.Where(c => c[li] == '0');
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
        */
    }
}