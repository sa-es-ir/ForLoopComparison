# A comparison of all For loops in c#
This project is only for curiosity :)
```csharp 
[MemoryDiagnoser(false)]
public class AllForLoopBenchmark
{
    [Params(1000, 10_000, 100_000)]
    public int ItemCount { get; set; }

    private List<int> _items;

    [GlobalSetup]
    public void GlobalSetup()
    {
        _items = Enumerable.Range(0, ItemCount).ToList();
    }

    [Benchmark]
    public void For()
    {
        for (int i = 0; i < ItemCount; i++)
        {
            DoSomeThing(_items[i]);
        }
    }

    [Benchmark]
    public void ForEach()
    {
        foreach (var item in _items)
        {
            DoSomeThing(item);
        }
    }

    [Benchmark]
    public void ForEach_Linq()
    {
        _items.ForEach(DoSomeThing);
    }

    [Benchmark]
    public void ForEach_Parallel()
    {
        Parallel.ForEach(_items, DoSomeThing);
    }

    [Benchmark]
    public void ForEach_LinqParallel()
    {
        _items.AsParallel().ForAll(DoSomeThing);
    }

    [Benchmark]
    public void For_Span()
    {
        var span = CollectionsMarshal.AsSpan(_items);
        for (int i = 0; i < span.Length; i++)
        {
            DoSomeThing(span[i]);
        }
    }

    [Benchmark]
    public void ForEach_Span()
    {
        foreach (var item in CollectionsMarshal.AsSpan(_items))
        {
            DoSomeThing(item);
        }
    }

    private void DoSomeThing(int i)
    {
        _ = i;
    }
}
```

## Benchmark Result:
```csharp
|               Method | ItemCount |         Mean |       Error |       StdDev |       Median | Allocated |
|--------------------- |---------- |-------------:|------------:|-------------:|-------------:|----------:|
|                  For |      1000 |     809.5 ns |    46.99 ns |    137.08 ns |     745.4 ns |         - |
|              ForEach |      1000 |   1,051.8 ns |    20.76 ns |     41.93 ns |   1,043.6 ns |         - |
|         ForEach_Linq |      1000 |   3,674.8 ns |    71.84 ns |    111.84 ns |   3,653.3 ns |      64 B |
|     ForEach_Parallel |      1000 |   9,445.2 ns |    78.39 ns |     73.33 ns |   9,423.6 ns |    2202 B |
| ForEach_LinqParallel |      1000 |  13,245.2 ns |   264.20 ns |    533.70 ns |  13,261.8 ns |    2904 B |
|             For_Span |      1000 |     702.9 ns |     9.86 ns |      8.74 ns |     700.0 ns |         - |
|         ForEach_Span |      1000 |     791.3 ns |    17.23 ns |     50.79 ns |     790.0 ns |         - |
|                  For |     10000 |  11,752.6 ns |   230.83 ns |    323.59 ns |  11,682.0 ns |         - |
|              ForEach |     10000 |  16,554.0 ns |   442.16 ns |  1,303.72 ns |  16,730.6 ns |         - |
|         ForEach_Linq |     10000 |  43,653.7 ns | 1,291.37 ns |  3,807.63 ns |  42,947.3 ns |      64 B |
|     ForEach_Parallel |     10000 |  53,834.0 ns | 1,072.38 ns |  3,161.94 ns |  53,535.9 ns |    2218 B |
| ForEach_LinqParallel |     10000 |  61,958.7 ns | 1,228.99 ns |  3,506.39 ns |  61,623.8 ns |    2904 B |
|             For_Span |     10000 |   7,900.8 ns |   156.28 ns |    269.58 ns |   7,872.7 ns |         - |
|         ForEach_Span |     10000 |   7,818.2 ns |   144.31 ns |    319.78 ns |   7,703.8 ns |         - |
|                  For |    100000 |  98,984.3 ns | 4,551.07 ns | 13,418.92 ns |  98,272.7 ns |         - |
|              ForEach |    100000 | 160,831.6 ns | 5,814.17 ns | 17,143.22 ns | 157,615.5 ns |         - |
|         ForEach_Linq |    100000 | 428,260.2 ns | 7,608.56 ns |  7,117.05 ns | 427,985.6 ns |      64 B |
|     ForEach_Parallel |    100000 | 414,901.2 ns | 8,142.42 ns | 14,260.79 ns | 412,322.3 ns |    2292 B |
| ForEach_LinqParallel |    100000 | 492,701.4 ns | 9,754.53 ns | 19,480.85 ns | 492,097.3 ns |    2919 B |
|             For_Span |    100000 |  69,455.3 ns |   921.59 ns |    769.57 ns |  69,499.4 ns |         - |
|         ForEach_Span |    100000 |  69,146.4 ns |   630.21 ns |    589.50 ns |  69,162.3 ns |         - |
```
