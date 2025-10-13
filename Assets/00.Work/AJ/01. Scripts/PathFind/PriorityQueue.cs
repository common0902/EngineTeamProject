using System;
using System.Collections.Generic;

public class PriorityQueue<T> where T : IComparable<T>
{
    public List<T> heap = new List<T>();
    public int Count => heap.Count;

    public void Clear() => heap?.Clear();

    public T Contains(T target)
    {
        int idx = heap.IndexOf(target);
        if (idx < 0) return default;
        return heap[idx];
    }

    public void Push(T data)
    {
        heap.Add(data);
        int now = heap.Count - 1;
        
        while (now > 0)
        {
            int next = (now - 1) / 2;
            if (heap[now].CompareTo(heap[next]) < 0)
                break;
            
            (heap[now], heap[next]) = (heap[next], heap[now]);
            now = next;
        }
    }

    public T Pop()
    {
        T ret = heap[0];
        
        int lastIndex = heap.Count - 1;
        heap[0] = heap[lastIndex];
        heap.RemoveAt(lastIndex); // 마지막을 지운다.
        lastIndex--;

        int now = 0;
        while (true)
        {
            int left = 2 * now + 1;
            int right = 2 * now + 2;
            int next = now; // next를 나로 두고

            if (left <= lastIndex && heap[next].CompareTo(heap[left]) < 0)
                next = left; // 왼쪽이 나보다 작으면 next는 왼쪽으로 놓고
            
            if (right <= lastIndex && heap[next].CompareTo(heap[right]) < 0)
                next = right; // next하고 오른쪽 값을 비교해서 작은 애가 next가 됨.
            // 결국 셋중에 가장 작은애임.
            
            if (next == now) // 내 자식 중 누구와도 같지 않다면
                break; // 나감.
            
            (heap[now], heap[next]) = (heap[next], heap[now]); // 그렇지 않으면 다시 교환.
            now = next; // 그리고 now가 next와 바뀜.
        }
        
        return ret;
    }

    public T Peek()
    {
        return heap.Count == 0 ? default : heap[0];
    }
}
