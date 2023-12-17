using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtensions
{
    public static TState Aggregate<TSource, TState>(this IEnumerable<TSource> seq, Func<TSource, TState, TState> aggregator, TState initial)
    {
        var state = initial;
        foreach (var x in seq)
            state = aggregator(x, state);
        return state;
    }
}

public static class Program
{
    public static void Main(string[] args)
    {
        var seq = Enumerable.Range(1, 10);

        var sumResult = seq.Aggregate((a, b) => a + b, 0);
        var productResult = seq.Aggregate((a, b) => a * b, 1);

        Console.WriteLine($"Сума: {sumResult}");
        Console.WriteLine($"Добуток: {productResult}");

    }
}
