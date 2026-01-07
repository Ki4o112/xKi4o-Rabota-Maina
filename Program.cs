using System;

public class CircularQueue<T>
{
    private T[] items;
    private int startIndex;
    private int endIndex;
    private int count;

    public CircularQueue(int initialCapacity = 8)
    {
        if (initialCapacity < 1)
            throw new ArgumentException("Initial capacity must be positive.");

        items = new T[initialCapacity];
        startIndex = 0;
        endIndex = 0;
        count = 0;
    }

    public int Count => count;

    public int Capacity => items.Length;

    public void Enqueue(T element)
    {
        if (count == items.Length)
        {
            Grow(); // удвояваме капацитета при запълване
        }

        items[endIndex] = element;
        endIndex = (endIndex + 1) % items.Length; // въртим индекса
        count++;
    }

    public T Dequeue()
    {
        if (count == 0)
            throw new InvalidOperationException("Queue is empty.");

        T element = items[startIndex];
        items[startIndex] = default(T); // по желание: изчистваме
        startIndex = (startIndex + 1) % items.Length;
        count--;

        return element;
    }

    public T Peek()
    {
        if (count == 0)
            throw new InvalidOperationException("Queue is empty.");

        return items[startIndex];
    }

    private void Grow()
    {
        int newCapacity = items.Length * 2;
        T[] newArray = new T[newCapacity];

        // Копираме елементите в новия масив от 0 нататък
        for (int i = 0; i < count; i++)
        {
            newArray[i] = items[(startIndex + i) % items.Length];
        }

        items = newArray;
        startIndex = 0;
        endIndex = count;
    }

    public T[] ToArray()
    {
        T[] result = new T[count];
        for (int i = 0; i < count; i++)
        {
            result[i] = items[(startIndex + i) % items.Length];
        }
        return result;
    }
}
