using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace aoc.csharp._2019
{
    internal class StandardQueue<T> : IQueue<T>
    {
        private readonly Queue<T> _queue;

        public StandardQueue()
        {
            _queue = new Queue<T>();
        }

        public int Count => _queue.Count;

        public T Dequeue()
        {
            return _queue.Dequeue();
        }

        public void Enqueue(T value)
        {
            _queue.Enqueue(value);
        }

        public bool TryDequeue([MaybeNullWhen(false)] out T value)
        {
            return _queue.TryDequeue(out value);
        }
    }
}
