
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
