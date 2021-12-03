/*

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19042.1348 (20H2/October2020Update)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.100
  [Host]     : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT
  DefaultJob : .NET 6.0.0 (6.0.21.52210), X64 RyuJIT


| Method |       Mean |    Error |    StdDev |  Gen 0 | Allocated |
|------- |-----------:|---------:|----------:|-------:|----------:|
|  PartI |   213.8 us |  4.26 us |  10.13 us | 0.2441 |      1 KB |
| PartII | 3,822.3 us | 76.45 us | 181.68 us |      - |     10 KB |

*/

#define BDN
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
    private string[] input;

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
        input = File.ReadAllLines(path);
    }
    
    [Benchmark]
    public int PartI()
    {
        var ga = 0;
        var co = 0;



        for (int i = 0; i < input[0].Length; i++)
        {
            ga = ga * 2;
            co = co * 2;
            if (input.Select(s => s[i]).Count(c => c == '0') > input.Length / 2)
            {
                co++;
            }
            else
            {
                ga++;
            }
        }

        // Console.WriteLine(string.Join(", ", oxy));
        // Console.WriteLine(string.Join(", ", co2));
        // Console.WriteLine(Convert.ToInt32(oxy.First(), 2) * Convert.ToInt32(co2.First(), 2));

        // Console.WriteLine(oxyS + " " + co2S);
        return co * ga;
    }

    [Benchmark]
    public int PartII()
    {
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
                /*
                if (!gammaStop)
                {
                    gamma += "0";
                    gammaInt = gammaInt * 2;
                }
                if (!epsStop)
                {
                    epsilon += "1";
                    epsilonInt = epsilonInt * 2 + 1;
                }*/
                sel0 = sel0.Where(c => c[li] == '0');
                // sel1 = sel1.Where(c => c[li] == '1');
            }
            else
            {
                /*
                if (!gammaStop)
                {
                    gamma += "1";
                    gammaInt = gammaInt * 2 + 1;
                }
                if (!epsStop)
                {
                    epsilon += "0";
                    epsilonInt = epsilonInt * 2;
                }*/
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
    }
}