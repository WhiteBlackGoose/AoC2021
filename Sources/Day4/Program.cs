#define BDN
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CodegenAnalysis.Benchmarks;

// Verify
#if DEBUG
var b = new ToBench();
b.Setup();
Console.WriteLine(b.PartI());
Console.WriteLine(b.PartII());
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
    private string[] input;

    public ToBench()
    {
        Setup();
    }

    [GlobalSetup]
    public void Setup()
    {
        #if DEBUG || !BDN
        var path = @"..\..\..\input.txt";
        #else
        var path = @"..\..\..\..\..\..\..\input.txt";
        #endif
        input = File.ReadAllLines(path);
    }

    private static List<int?[]> GetBoards(string[] input)
    {
        

        var boards = new List<int?[]>();
        var lastFilled = 5;
        foreach (var line in input.Skip(1))
        {
            if (line.Length < 5)
                continue;
            int?[] toFill;
            if (lastFilled == 5)
            {
                toFill = new int?[25];
                boards.Add(toFill);
                lastFilled = 0;
            }
            else
            {
                toFill = boards[^1];
            }
            var nums = line.Replace("  ", " ").Split(" ").Where(c => c.Length > 0).Select(int.Parse).Select(i => (int?)i).ToArray();
            nums.CopyTo(toFill, lastFilled * 5);
            lastFilled++;
        }

        return boards;
    }

    [Benchmark]
    public int PartI()
    {
        var numbers = input[0].Split(',').Select(int.Parse);
        var boards = GetBoards(input);

        var lastNum = 0;
        var lastSum = 0;

        foreach (var num in numbers)
        {
            foreach (var board2fill in boards)
                Fill(board2fill, num);
            if (boards.Where(IsBoardFilled).SingleOrDefault() is { } board)
            {
                boards.Remove(board);
                var sum = board.Where(i => i is not null).Select(i => (int)i).Sum();
                return sum * num;
            }
        }
        return 0;
    }

    [Benchmark]
    public int PartII()
    {
        var numbers = input[0].Split(',').Select(int.Parse);
        var boards = GetBoards(input);

        var lastNum = 0;
        var lastSum = 0;

        foreach (var num in numbers)
        {
            foreach (var board2fill in boards)
                Fill(board2fill, num);
            if (boards.Where(IsBoardFilled).Count() > 1)
            {
                foreach (var fakeWinner in boards.Where(IsBoardFilled).ToArray())
                    boards.Remove(fakeWinner);
            }
            else if (boards.Where(IsBoardFilled).SingleOrDefault() is { } board)
            {
                boards.Remove(board);
                var sum = board.Where(i => i is not null).Select(i => (int)i).Sum();
                lastSum = sum;
                lastNum = num;
            }
        }
        return lastSum * lastNum;
    }

    static void Fill(int?[] board, int num)
    {
        for (int i = 0; i < 25; i++)
            if (num == board[i])
                board[i] = null;
    }

    static bool IsBoardFilled(int?[] board)
    {
        for (int i = 0; i < 5; i++)
        {
            var vertical = false;
            var horizontal = false;
            for (int j = 0; j < 5; j++)
            {
                if (board[i * 5 + j] is not null)
                {
                    vertical = true;
                }
                if (board[j * 5 + i] is not null)
                {
                    horizontal = true;
                }
            }
            if (!vertical || !horizontal)
                return true;
        }
        return false;
    }
}