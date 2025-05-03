using System.Diagnostics.CodeAnalysis;

namespace aoc.csharp._2019;

public interface IQueue<T>
{
    int Count { get; }
    void Enqueue(T value);
    T Dequeue();
    bool TryDequeue([MaybeNullWhen(false)] out T value);
}
