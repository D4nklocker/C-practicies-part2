using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace rocket_bot;

public class Channel<T> where T : class
{
    private List<T> channel = new List<T> { };
    private object obj = new object();
    /// <summary>
    /// ���������� ������� �� ������� ��� null, ���� ������ �������� ���.
    /// ��� ���������� ������� ��� �������� �����.
    /// ���� ������ � �������� ����� ������� ���������, �������� ��� Append.
    /// </summary>
    public T this[int index]
    {
        get
        {
            lock (channel)
            {
                return index >= 0 && channel.Count > index ? channel[index] : null;
            }
        }
        set
        {
            lock (channel)
            {
                channel.RemoveRange(index, channel.Count - index);
                channel.Add(value);
            }
        }
    }

    /// <summary>
    /// ���������� ��������� ������� ��� null, ���� ������ �������� ���
    /// </summary>
    public T LastItem()
    {
        lock (channel)
        {
            return channel.LastOrDefault();
        }
    }

    /// <summary>
    /// ��������� item � ����� ������ ���� lastItem �������� ��������� ���������
    /// </summary>
	public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
    {
        lock (channel)
        {
            if (channel.LastOrDefault() == knownLastItem)
                channel.Add(item);
        }
    }

    /// <summary>
    /// ���������� ���������� ��������� � ���������
    /// </summary>
    public int Count
    {
        get
        {
            lock (channel)
            {
                return channel.Count;
            }
        }
    }
}