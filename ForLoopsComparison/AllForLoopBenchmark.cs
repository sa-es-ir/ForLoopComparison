using System.Runtime.InteropServices;
using BenchmarkDotNet.Attributes;

namespace ForLoopsComparison;

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