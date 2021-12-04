#define BDN
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using CodegenAnalysis.Benchmarks;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Verify
#if DEBUG
var b = new ToBench();
b.Setup();
Console.WriteLine(b.PartI_v1());
// Console.WriteLine(b.PartI_v2());
Console.WriteLine(b.PartI_v3());
Console.WriteLine(b.PartI_v4());
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

[CAOptions(VisualizeBackwardJumps = false)]
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

    // [Benchmark]
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
            int res = (int)(nums[0] - '0');
            nums++;
            if (char.IsDigit(nums[0]))
            {
                res = (int)(10 * res + nums[0] - '0');
                nums++;
            }
            JumpToDigit();
            return res;
        }

        public bool EOF => nums >= upper;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit, Size = 32)]
    public unsafe struct Bytes32
    {
        public byte this[int i]
        {
            get
            {
                return Unsafe.Add(ref Unsafe.As<Bytes32, byte>(ref this), i);
            }
            set
            {
                Unsafe.Add(ref Unsafe.As<Bytes32, byte>(ref this), i) = value;
            }
        }
    }

    public static unsafe List<int[]> GetBoardsFast(string input)
    {
        var res = new List<int[]>(100);
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
    /*
    public static unsafe List<Bytes32> GetBoardsVeryFast(string input)
    {
        var res = new List<Bytes32>(100);
        fixed (char* c = input)
        {
            var reader = new IntReader(c, input.Length);
            reader.NextLineUnsafe();
            reader.JumpToDigit();
            while (!reader.EOF)
            {
                var arr = new Bytes32();
                for (int i = 0; i < 25; i++)
                    arr[i] = reader.Read();
                res.Add(arr);
            }
        }
        return res;
    }*/

    public static unsafe Span<int> GetBoardsBlazinglyFast(string input)
    {
        var res = new List<int>(100);
        fixed (char* c = input)
        {
            var curr = c;
            while (curr[0] != '\n')
                curr++;
            var upper = c + input.Length;
            for (; curr < upper; curr++)
            {
                if (curr[0] is >= '0' and <= '9')
                {
                    var n = curr[0] - '0';
                    curr++;
                    if (curr[0] is >= '0' and <= '9')
                    {
                        n = 10 * n + curr[0] - '0';
                        curr++;
                    }
                    res.Add(n);
                }
            }
        }
        return CollectionsMarshal.AsSpan(res);
    }


    // [Benchmark]
    public List<int[]> PartI_v2_boards()
        => GetBoardsFast(input);

        /*
    [Benchmark, CAAnalyze, CASubject(typeof(ToBench), "GetBoardsVeryFast")]
    public List<Bytes32> PartI_v2_vf_boards()
        => GetBoardsVeryFast(input);*/

    // [Benchmark]
    public Span<int> PartI_v3_b_boards()
        => GetBoardsBlazinglyFast(input);

    // [Benchmark]
    /*
    public int PartI_v2()
    {
        var nums = new int[100];

        var inputNums = inputLines[0].Split(',').Select(int.Parse).ToArray();
        for (var i = 0; i < inputNums.Length; i++)
        {
            nums[inputNums[i]] = i;
        }

        Span<Bytes32> boards = CollectionsMarshal.AsSpan(GetBoardsVeryFast(input));

        var firstBoardId = -1;
        var stepAfterWhichFirstBoardWins = 100;
        // var lastBoardId = -1;
        // var stepAfterWhichLastBoardWins = -1;

        foreach (var (a, b) in new [] { (5, 1), (1, 5) })
        for (int i = 0; i < boards.Length; i++)
        {
            for (int x = 0; x < 5; x++)
            {
                var neverCanceled = false;
                var lowest = 0;
                // var highest = 100;
                for (int y = 0; y < 5; y++)
                {
                    var step = nums[(int)boards[i][x * a + y * b]];
                    lowest = Math.Max(step, lowest);
                    // highest = Math.Min(step, highest);
                }
                // if (!neverCanceled)
                {
                    if (lowest < stepAfterWhichFirstBoardWins)
                    {
                        stepAfterWhichFirstBoardWins = lowest;
                        firstBoardId = i;
                    }
                }

            }
        }

        var lowestSum = 0;
        for (int i = 0; i < 25; i++)
        {
            if ((nums[(int)boards[firstBoardId][i]] > stepAfterWhichFirstBoardWins))
                lowestSum += (int)boards[firstBoardId][i];
        }

        return lowestSum * inputNums[stepAfterWhichFirstBoardWins];
    }*/

    // [Benchmark]
    public int PartI_v3()
    {
        Span<int> nums = stackalloc int[100];

        var inputNums = inputLines[0].Split(',').Select(int.Parse).ToArray();
        for (var i = 0; i < inputNums.Length; i++)
        {
            nums[inputNums[i]] = i;
        }

        var boards = GetBoardsBlazinglyFast(input);
        var len = boards.Length / 25;

        var firstBoardId = -1;
        var stepAfterWhichFirstBoardWins = 100;
        // var lastBoardId = -1;
        // var stepAfterWhichLastBoardWins = -1;

        foreach (var (a, b) in new[] { (5, 1), (1, 5) })
            for (int i = 0; i < len; i++)
            {
                for (int x = 0; x < 5; x++)
                {
                    var neverCanceled = false;
                    var lowest = 0;
                    // var highest = 100;
                    for (int y = 0; y < 5; y++)
                    {
                        var step = nums[(int)boards[i * 25 + x * a + y * b]];
                        lowest = Math.Max(step, lowest);
                        // highest = Math.Min(step, highest);
                    }
                    // if (!neverCanceled)
                    {
                        if (lowest < stepAfterWhichFirstBoardWins)
                        {
                            stepAfterWhichFirstBoardWins = lowest;
                            firstBoardId = i;
                        }
                        /*
                        if (highest > stepAfterWhichLastBoardWins)
                        {
                            stepAfterWhichLastBoardWins = highest;
                            lastBoardId = i;
                        }*/
                    }

                }
            }

        var lowestSum = 0;
        for (int i = 0; i < 25; i++)
        {
            if ((nums[(int)boards[firstBoardId * 25 + i]] > stepAfterWhichFirstBoardWins))
                lowestSum += (int)boards[firstBoardId * 25 + i];
        }

        return lowestSum * inputNums[stepAfterWhichFirstBoardWins];
    }

    // [Benchmark]
    public int[]? Aaa() => inputLines[0].Split(',').Select(int.Parse).ToArray();

    [Benchmark, SkipLocalsInit, CAAnalyze]
    public unsafe int PartI_v4()
    {
        fixed (char* c = input)
        {
            var nums = stackalloc int[100];
            var inputNums = stackalloc int[100];
            var reader = new IntReader(c, input.Length);
            reader.JumpToDigit();

            for (var i = 0; i < 100; i++)
            {
                inputNums[i] = reader.Read();
                nums[inputNums[i]] = i;
            }

            var bestScore = 0;
            var lowest = 100;
            
            var board = stackalloc int[25];

            while (!reader.EOF)
            {
                for (int i = 0; i < 25; i++)
                    board[i] = reader.Read();
                var bestRow =
                    Min(
                        Max(nums, board, 0 + 5 * 0, 1 + 5 * 0, 2 + 5 * 0, 3 + 5 * 0, 4 + 5 * 0),
                        Max(nums, board, 0 + 5 * 1, 1 + 5 * 1, 2 + 5 * 1, 3 + 5 * 1, 4 + 5 * 1),
                        Max(nums, board, 0 + 5 * 2, 1 + 5 * 2, 2 + 5 * 2, 3 + 5 * 2, 4 + 5 * 2),
                        Max(nums, board, 0 + 5 * 3, 1 + 5 * 3, 2 + 5 * 3, 3 + 5 * 3, 4 + 5 * 3),
                        Max(nums, board, 0 + 5 * 4, 1 + 5 * 4, 2 + 5 * 4, 3 + 5 * 4, 4 + 5 * 4)
                    );
                var bestCol =
                    Min(
                        Max(nums, board, 0 * 5 + 0, 1 * 5 + 0, 2 * 5 + 0, 3 * 5 + 0, 4 * 5 + 0),
                        Max(nums, board, 0 * 5 + 1, 1 * 5 + 1, 2 * 5 + 1, 3 * 5 + 1, 4 * 5 + 1),
                        Max(nums, board, 0 * 5 + 2, 1 * 5 + 2, 2 * 5 + 2, 3 * 5 + 2, 4 * 5 + 2),
                        Max(nums, board, 0 * 5 + 3, 1 * 5 + 3, 2 * 5 + 3, 3 * 5 + 3, 4 * 5 + 3),
                        Max(nums, board, 0 * 5 + 4, 1 * 5 + 4, 2 * 5 + 4, 3 * 5 + 4, 4 * 5 + 4)
                    );
                var best = Math.Min(bestRow, bestCol);
                if (best < lowest)
                {
                    lowest = best;
                    bestScore = CountSum(nums, board, best);
                }
            }
            return bestScore * inputNums[lowest];
        }

        static int CountSum(int* nums, int* src, int step)
        {
            var s = 0;
            for (int i = 0; i < 25; i++)
                if (nums[src[i]] > step)
                    s += src[i];
            return s;
        }

        static int Max(int* nums, int* src, int a, int b, int c, int d, int e)
            => Math.Max(nums[src[a]], Math.Max(nums[src[b]], Math.Max(nums[src[c]], Math.Max(nums[src[d]], nums[src[e]]))));

        static int Min(int a, int b, int c, int d, int e)
            => Math.Min(a, Math.Min(b, Math.Min(c, Math.Min(d, e))));
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