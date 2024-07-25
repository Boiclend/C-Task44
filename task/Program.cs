// Написать программу, которая должна выполнять следующие операции:

// Создавать двусвязный кольцевой  список.
// Операцию дополнения списка
// Операцию просмотра списка
// Операцию удаления элементов списка.
// Копирование элементов списка в массив.
// Сортировка методом Пузырька.
// Поиск (в лоб) или линейный поиск элемента в массиве, направление поиска в списке — прямое.
// Операция считывания данных из файла в список.
// Сохранение списка в файл.


using System;
using System.Collections.Generic;
using System.IO;

public class Node<T>
{
    public T Data;
    public Node<T> Next;
    public Node<T> Previous;

    public Node(T data)
    {
        Data = data;
    }
}

public class DoublyCircularLinkedList<T>
{
    private Node<T> head;

    // Операция дополнения списка
    public void Add(T data)
    {
        Node<T> newNode = new Node<T>(data);
        if (head == null)
        {
            head = newNode;
            head.Next = head;
            head.Previous = head;
        }
        else
        {
            Node<T> tail = head.Previous;
            tail.Next = newNode;
            newNode.Previous = tail;
            newNode.Next = head;
            head.Previous = newNode;
        }
    }

    // Операция просмотра списка
    public void Display()
    {
        if (head == null) return;
        Node<T> current = head;
        do
        {
            Console.Write(current.Data + " ");
            current = current.Next;
        } while (current != head);
        Console.WriteLine();
    }

    // Операция удаления элементов списка
    public void Delete(T data)
    {
        if (head == null) return;
        Node<T> current = head;
        do
        {
            if (current.Data.Equals(data))
            {
                if (current.Next == current)
                {
                    head = null;
                }
                else
                {
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;
                    if (current == head)
                    {
                        head = current.Next;
                    }
                }
                break;
            }
            current = current.Next;
        } while (current != head);
    }

    // Копирование элементов списка в массив
    public T[] ToArray()
    {
        List<T> list = new List<T>();
        if (head == null) return list.ToArray();
        Node<T> current = head;
        do
        {
            list.Add(current.Data);
            current = current.Next;
        } while (current != head);
        return list.ToArray();
    }

    // Сортировка методом Пузырька
    public void BubbleSort()
    {
        if (head == null) return;
        bool swapped;
        Node<T> current;
        do
        {
            swapped = false;
            current = head;
            do
            {
                Node<T> next = current.Next;
                if (Comparer<T>.Default.Compare(current.Data, next.Data) > 0)
                {
                    T temp = current.Data;
                    current.Data = next.Data;
                    next.Data = temp;
                    swapped = true;
                }
                current = current.Next;
            } while (current.Next != head);
        } while (swapped);
    }

    // Линейный поиск элемента в массиве
    public int LinearSearch(T data)
    {
        if (head == null) return -1;
        Node<T> current = head;
        int index = 0;
        do
        {
            if (current.Data.Equals(data))
            {
                return index;
            }
            current = current.Next;
            index++;
        } while (current != head);
        return -1;
    }

    // Операция считывания данных из файла в список
    public void ReadFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            if (typeof(T) == typeof(int))
            {
                Add((T)(object)int.Parse(line));
            }
            else if (typeof(T) == typeof(double))
            {
                Add((T)(object)double.Parse(line));
            }
            else if (typeof(T) == typeof(string))
            {
                Add((T)(object)line);
            }
        }
    }

    // Операция сохранения списка в файл
    public void WriteToFile(string filePath)
    {
        List<T> list = new List<T>();
        if (head != null)
        {
            Node<T> current = head;
            do
            {
                list.Add(current.Data);
                current = current.Next;
            } while (current != head);
        }
        File.WriteAllLines(filePath, list.ConvertAll(x => x.ToString()).ToArray());
    }
}

class Program
{
    static void Main()
    {
        DoublyCircularLinkedList<int> list = new DoublyCircularLinkedList<int>();

        // Операция дополнения списка
        list.Add(3);
        list.Add(1);
        list.Add(4);
        list.Add(2);

        // Операция просмотра списка
        Console.WriteLine("List contents:");
        list.Display();

        // Операция удаления элементов списка
        list.Delete(3);
        Console.WriteLine("After deletion:");
        list.Display();

        // Копирование элементов списка в массив
        int[] array = list.ToArray();
        Console.WriteLine("Array contents:");
        Console.WriteLine(string.Join(", ", array));

        // Сортировка методом Пузырька
        list.BubbleSort();
        Console.WriteLine("After sorting:");
        list.Display();

        // Линейный поиск элемента в массиве
        int index = list.LinearSearch(4);
        Console.WriteLine("Index of element '4': " + index);

        // Операция сохранения списка в файл
        list.WriteToFile("list.txt");

        // Операция считывания данных из файла в список
        DoublyCircularLinkedList<int> newList = new DoublyCircularLinkedList<int>();
        newList.ReadFromFile("list.txt");
        Console.WriteLine("List contents after reading from file:");
        newList.Display();
    }
}