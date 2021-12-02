/*

Performance:

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19042.1348 (20H2/October2020Update)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT

| Method |     Mean |    Error |   StdDev | Allocated |
|------- |---------:|---------:|---------:|----------:|
|  PartI | 13.34 us | 0.261 us | 0.503 us |         - |
| PartII | 13.26 us | 0.263 us | 0.555 us |         - |


JIT Codegen:

| Method          | Branches  | Calls  | StaticStackAllocations  | CodegenSize  | ILSize  |
|:---------------:|:---------:|:------:|:-----------------------:|:------------:|:-------:|
| Int32 PartI()   | 22        | 1      | 88 B                    | 631 B        | 69 B    |
| Int32 PartII()  | 22        | 1      | 88 B                    | 642 B        | 69 B    |

*/

// BenchmarkDotNet
// CodegenAnalysis.Benchmarks


using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CodegenAnalysis.Benchmarks;
using System.Runtime.CompilerServices;

// Verify
// var b = new ToBench();
// b.Setup();
// Console.WriteLine(b.PartI());

// Get the codegen analysis
// CodegenBenchmarkRunner.Run<ToBench>();

// Get the perf results
BenchmarkRunner.Run<ToBench>();

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

    private string input;

    [GlobalSetup]
    public void Setup()
    {
        var path = @"..\..\..\input.txt";
        input = File.ReadAllText(path);
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
}
