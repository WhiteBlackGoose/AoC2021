#define BDN
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CodegenAnalysis.Benchmarks;
using System.Runtime.CompilerServices;

// Verify
#if DEBUG
var b = new ToBench();
b.Setup();
Console.WriteLine(b.PartI_v1());
Console.WriteLine(b.PartI_v2());
// Console.WriteLine(b.PartII());
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
    private string[] inputLines;
    private string input;

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
        inputLines = File.ReadAllLines(path);
        input = File.ReadAllText(path);
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
    public int PartI_v1()
    {
        var numbers = inputLines[0].Split(',').Select(int.Parse);
        var boards = GetBoards(inputLines);

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

    private unsafe struct IntReader
    {
        private char* nums;
        private readonly char* upper;

        public IntReader(char* nums, int length)
        {
            this.nums = nums;
            upper = nums + length;
        }

        public void JumpToDigit()
        {
            while (nums < upper && nums[0] is < '0' or > '9')
                nums++;
        }

        public void NextLineUnsafe()
        {
            while (nums[0] != '\n')
                nums++;
        }

        public int Read()
        {
            var res = nums[0] - '0';
            nums++;
            if (char.IsDigit(nums[0]))
            {
                res = 10 * res + nums[0] - '0';
                nums++;
            }
            JumpToDigit();
            return res;
        }

        public bool EOF => nums >= upper;
    }

    public static unsafe List<int[]> GetBoardsFast(string input)
    {
        var res = new List<int[]>();
        fixed (char* c = input)
        {
            var reader = new IntReader(c, input.Length);
            reader.NextLineUnsafe();
            reader.JumpToDigit();
            while (!reader.EOF)
            {
                var arr = new int[25];
                for (int i = 0; i < 25; i++)
                    arr[i] = reader.Read();
                res.Add(arr);
            }
        }
        return res;
    }

    [Benchmark]
    public int PartI_v2()
    {
        var nums = new int?[100];
        var inputNums = inputLines[0].Split(',').Select(int.Parse).ToArray();
        for (var i = 0; i < inputNums.Length; i++)
        {
            nums[inputNums[i]] = i;
        }

        var boards = GetBoardsFast(input);

        var firstBoardId = -1;
        var stepAfterWhichFirstBoardWins = 100;
        var lastBoardId = -1;
        var stepAfterWhichLastBoardWins = -1;

        foreach (var (a, b) in new [] { (5, 1), (1, 5) })
        for (int i = 0; i < boards.Count; i++)
        {
            for (int x = 0; x < 5; x++)
            {
                var neverCanceled = false;
                var lowest = 0;
                var highest = 100;
                for (int y = 0; y < 5; y++)
                {
                    var step = nums[(int)boards[i][x * a + y * b]];
                    if (step is { } valid)
                    {
                        lowest = Math.Max(valid, lowest);
                        highest = Math.Min(valid, highest);
                    }
                    else
                    {
                        neverCanceled = true;
                        break;
                    }
                }
                if (!neverCanceled)
                {
                    if (lowest < stepAfterWhichFirstBoardWins)
                    {
                        stepAfterWhichFirstBoardWins = lowest;
                        firstBoardId = i;
                    }
                    if (highest > stepAfterWhichLastBoardWins)
                    {
                        stepAfterWhichLastBoardWins = highest;
                        lastBoardId = i;
                    }
                }

            }
        }

        /*
        for (int i = 0; i < boards.Count; i++)
        {
            for (int y = 0; y < 5; y++)
            {
                var neverCanceled = false;
                var lowest = 0;
                var highest = 100;
                for (int x = 0; x < 5; x++)
                {
                    var step = nums[(int)boards[i][x * 5 + y]];
                    if (step is { } valid)
                    {
                        lowest = Math.Max(valid, lowest);
                        // highest = Math.Min(valid, highest);
                    }
                    else
                    {
                        neverCanceled = true;
                        break;
                    }
                }
                if (!neverCanceled)
                {
                    if (lowest < stepAfterWhichFirstBoardWins)
                    {
                        stepAfterWhichFirstBoardWins = lowest;
                        firstBoardId = i;
                    }
                    if (highest > stepAfterWhichLastBoardWins)
                    {
                        stepAfterWhichLastBoardWins = highest;
                        lastBoardId = i;
                    }
                }

            }
        }*/

        var lowestSum = 0;
        for (int i = 0; i < 25; i++)
        {
            if (nums[(int)boards[firstBoardId][i]] is null || (nums[(int)boards[firstBoardId][i]] > stepAfterWhichFirstBoardWins))
                lowestSum += (int)boards[firstBoardId][i];
        }

        return lowestSum * inputNums[stepAfterWhichFirstBoardWins];
    }

    // [Benchmark]
    public int PartII_v2()
    {
        var numbers = inputLines[0].Split(',').Select(int.Parse);
        var boards = GetBoards(inputLines);

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