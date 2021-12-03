/*

Performance:

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19042.1348 (20H2/October2020Update)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT

|     Method |      Mean |     Error |    StdDev |    Median | Allocated |
|----------- |----------:|----------:|----------:|----------:|----------:|
|      PartI | 12.427 us | 0.2463 us | 0.2419 us | 12.441 us |         - |
|     PartII | 12.506 us | 0.2485 us | 0.7008 us | 12.234 us |         - |
|  PartI_ptr |  9.534 us | 0.1872 us | 0.2155 us |  9.500 us |         - |
| PartII_ptr |  9.480 us | 0.1410 us | 0.1319 us |  9.508 us |         - |


JIT Codegen:

| Job             | Method              | Branches  | Calls  | StaticStackAllocations  | CodegenSize  | ILSize  |
|:---------------:|:-------------------:|:---------:|:------:|:-----------------------:|:------------:|:-------:|
| (Tier = Tier1)  | Int32 PartI()       | 22        | 1      | 88 B                    | 631 B        | 69 B    |
| (Tier = Tier1)  | Int32 PartII()      | 22        | 1      | 88 B                    | 642 B        | 69 B    |
| (Tier = Tier1)  | Int32 PartI_ptr()   | 15        |  -     | 40 B                    | 317 B        | 100 B   |
| (Tier = Tier1)  | Int32 PartII_ptr()  | 15        |  -     | 40 B                    | 347 B        | 100 B   |

*/

// BenchmarkDotNet
// CodegenAnalysis.Benchmarks

#define BDN2

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CodegenAnalysis.Benchmarks;
using System.Runtime.CompilerServices;


// Verify
#if DEBUG
var b = new ToBench();
b.Setup();
Console.WriteLine(b.PartI());
Console.WriteLine(b.PartII());
Console.WriteLine(b.PartI_ptr());
Console.WriteLine(b.PartII_ptr());
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
[CAOptions(VisualizeBackwardJumps = false)]

[CAColumn(CAColumn.Branches),
 CAColumn(CAColumn.Calls),
 CAColumn(CAColumn.StaticStackAllocations),
 CAColumn(CAColumn.CodegenSize),
 CAColumn(CAColumn.ILSize)]

[CAExport(Export.Md)]
public class ToBench
{
    private string input;

    [GlobalSetup]
    public void Setup()
    {
#if DEBUG || !BDN
        var path = @"..\..\..\input.txt";
#else
        var path = @"..\..\..\..\..\..\..\input.txt";
#endif
        input = File.ReadAllText(path);
    }

    public ToBench()
    {
        Setup();
    }


    private ref struct SortaLexer
    {
        private readonly ReadOnlySpan<char> s;
        private int curr;

        public SortaLexer(ReadOnlySpan<char> s)
        {
            this.s = s;
            curr = 0;
        }

        public void Advance(int s)
        {
            curr += s;
        }

        private int ReadInt()
        {
            var res = s[curr] - '0';
            curr++;
            return res;
        }

        private void GoToNextChar()
        {
            while (curr < s.Length && (s[curr] is < 'a' or > 'z'))
            {
                curr++;
            }
        }

        public (int X, int Y, int Aim) DoI(int x, int y, int aim)
        {
            switch (s[curr])
            {
                case 'f':
                    Advance(8);
                    x += ReadInt();
                    GoToNextChar();
                    break;
                case 'd':
                    Advance(5);
                    y += ReadInt();
                    GoToNextChar();
                    break;
                case 'u':
                    Advance(3);
                    y -= ReadInt();
                    GoToNextChar();
                    break;
            }
            return (x, y, aim);
        }

        public (int X, int Y, int Aim) DoII(int x, int y, int aim)
        {
            switch (s[curr])
            {
                case 'f':
                    Advance(8);
                    var delta = ReadInt();
                    x += delta;
                    y += delta * aim;
                    GoToNextChar();
                    break;
                case 'd':
                    Advance(5);
                    aim += ReadInt();
                    GoToNextChar();
                    break;
                case 'u':
                    Advance(3);
                    aim -= ReadInt();
                    GoToNextChar();
                    break;
            }
            return (x, y, aim);
        }

        public bool Valid => curr < s.Length;
    }

    [Benchmark]
    [CAAnalyze]
    public int PartI()
    {
        var aaa = new SortaLexer(input.AsSpan());

        var (x, y, aim) = (0, 0, 0);

        while (aaa.Valid)
        {
            (x, y, aim) = aaa.DoI(x, y, aim);
        }

        return x * y;
    }

    [Benchmark]
    [CAAnalyze]
    public int PartII()
    {
        var aaa = new SortaLexer(input.AsSpan());

        var (x, y, aim) = (0, 0, 0);

        while (aaa.Valid)
        {
            (x, y, aim) = aaa.DoII(x, y, aim);
        }

        return x * y;
    }

    private unsafe ref struct SortaLexerPtr
    {
        private readonly char* s;
        private int curr;
        private readonly int length;

        public SortaLexerPtr(char* s, int length)
        {
            this.s = s;
            curr = 0;
            this.length = length;
        }

        public void Advance(int s)
        {
            curr += s;
        }

        private int ReadInt()
        {
            var res = s[curr] - '0';
            curr++;
            return res;
        }

        private void GoToNextChar()
        {
            while (curr < length && (s[curr] is < 'a' or > 'z'))
            {
                curr++;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (int X, int Y, int Aim) DoI(int x, int y, int aim)
        {
            switch (s[curr])
            {
                case 'f':
                    Advance(8);
                    x += ReadInt();
                    GoToNextChar();
                    break;
                case 'd':
                    Advance(5);
                    y += ReadInt();
                    GoToNextChar();
                    break;
                case 'u':
                    Advance(3);
                    y -= ReadInt();
                    GoToNextChar();
                    break;
            }
            return (x, y, aim);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (int X, int Y, int Aim) DoII(int x, int y, int aim)
        {
            switch (s[curr])
            {
                case 'f':
                    Advance(8);
                    var delta = ReadInt();
                    x += delta;
                    y += delta * aim;
                    GoToNextChar();
                    break;
                case 'd':
                    Advance(5);
                    aim += ReadInt();
                    GoToNextChar();
                    break;
                case 'u':
                    Advance(3);
                    aim -= ReadInt();
                    GoToNextChar();
                    break;
            }
            return (x, y, aim);
        }

        public bool Valid => curr < length;
    }

    [Benchmark]
    [CAAnalyze]
    public unsafe int PartI_ptr()
    {
        fixed (char* g = input)
        {
            var aaa = new SortaLexerPtr(g, input.Length);

            var (x, y, aim) = (0, 0, 0);

            while (aaa.Valid)
            {
                (x, y, aim) = aaa.DoI(x, y, aim);
            }

            return x * y;
        }
    }

    [Benchmark]
    [CAAnalyze]
    public unsafe int PartII_ptr()
    {
        fixed (char* g = input)
        {
            var aaa = new SortaLexerPtr(g, input.Length);

            var (x, y, aim) = (0, 0, 0);

            while (aaa.Valid)
            {
                (x, y, aim) = aaa.DoII(x, y, aim);
            }

            return x * y;
        }
    }
}
