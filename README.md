# A comparison of all For loops in c#
This project is only for curiosity :)
```csharp 
[MemoryDiagnoser(false)]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class AllForLoopBenchmark
{
    [Params(100_000)]
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
| Method               | Job      | Runtime  | ItemCount | Mean      | Error    | StdDev   | Allocated |
|--------------------- |--------- |--------- |---------- |----------:|---------:|---------:|----------:|
| For                  | .NET 6.0 | .NET 6.0 | 100000    |  38.25 us | 0.242 us | 0.202 us |         - |
| For                  | .NET 8.0 | .NET 8.0 | 100000    |  36.58 us | 0.098 us | 0.087 us |         - |
| For                  | .NET 9.0 | .NET 9.0 | 100000    |  36.59 us | 0.103 us | 0.092 us |         - |
| ForEach              | .NET 6.0 | .NET 6.0 | 100000    |  60.99 us | 0.184 us | 0.163 us |         - |
| ForEach              | .NET 8.0 | .NET 8.0 | 100000    |  36.58 us | 0.158 us | 0.132 us |         - |
| ForEach              | .NET 9.0 | .NET 9.0 | 100000    |  36.47 us | 0.134 us | 0.118 us |         - |
| ForEach_Linq         | .NET 6.0 | .NET 6.0 | 100000    | 146.08 us | 0.511 us | 0.453 us |      64 B |
| ForEach_Linq         | .NET 8.0 | .NET 8.0 | 100000    |  48.78 us | 0.063 us | 0.053 us |      64 B |
| ForEach_Linq         | .NET 9.0 | .NET 9.0 | 100000    |  48.98 us | 0.111 us | 0.098 us |      64 B |
| ForEach_Parallel     | .NET 6.0 | .NET 6.0 | 100000    | 132.79 us | 1.509 us | 1.337 us |    3135 B |
| ForEach_Parallel     | .NET 8.0 | .NET 8.0 | 100000    |  90.47 us | 0.337 us | 0.315 us |    3206 B |
| ForEach_Parallel     | .NET 9.0 | .NET 9.0 | 100000    |  95.16 us | 0.277 us | 0.216 us |    3207 B |
| ForEach_LinqParallel | .NET 6.0 | .NET 6.0 | 100000    | 170.72 us | 1.645 us | 1.539 us |    4156 B |
| ForEach_LinqParallel | .NET 8.0 | .NET 8.0 | 100000    | 104.26 us | 2.008 us | 1.879 us |    4210 B |
| ForEach_LinqParallel | .NET 9.0 | .NET 9.0 | 100000    | 116.52 us | 0.587 us | 0.549 us |    4215 B |
| For_Span             | .NET 6.0 | .NET 6.0 | 100000    |  32.10 us | 0.164 us | 0.146 us |         - |
| For_Span             | .NET 8.0 | .NET 8.0 | 100000    |  32.00 us | 0.097 us | 0.081 us |         - |
| For_Span             | .NET 9.0 | .NET 9.0 | 100000    |  26.42 us | 0.206 us | 0.193 us |         - |
| ForEach_Span         | .NET 6.0 | .NET 6.0 | 100000    |  32.01 us | 0.061 us | 0.057 us |         - |
| ForEach_Span         | .NET 8.0 | .NET 8.0 | 100000    |  31.96 us | 0.071 us | 0.063 us |         - |
| ForEach_Span         | .NET 9.0 | .NET 9.0 | 100000    |  26.21 us | 0.046 us | 0.041 us |         - |
```
