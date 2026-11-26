
using System;

public class CircularBuffer<T>
{
    private readonly T[] buffer;
    private int head;
    private int count;

    public CircularBuffer(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }

        buffer = new T[capacity];
        head = 0;
        count = 0;
    }

    public T Read()
    {
        if (count == 0)
        {
            throw new InvalidOperationException();
        }

        T item = buffer[head];
        head = (head + 1) % buffer.Length;
        count--;

        return item;
    }

    public void Write(T value)
    {
        if (count == buffer.Length)
        {
            throw new InvalidOperationException();
        }

        int tail = (head + count) % buffer.Length;
        buffer[tail] = value;
        count++;
    }

    public void Clear()
    {
        head = 0;
        count = 0;
        Array.Clear(buffer, 0, buffer.Length);
    }

    public void Overwrite(T value)
    {
        if (count < buffer.Length)
        {
            Write(value);
            return;
        }

        buffer[head] = value;
        head = (head + 1) % buffer.Length;
    }
}
