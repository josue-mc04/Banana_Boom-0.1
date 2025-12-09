using UnityEngine;

public class QueuePersecution<T>
{
    private class Node
    {
        public T data;
        public Node next;

        public Node(T d)
        {
            data = d;
            next = null;
        }
    }

    private Node front; 
    private Node back;  
    private int count; 

    public int Count()
    {
        return count;
    }

    //agregar elemento al final
    public void Enqueue(T element)
    {
        Node newNode = new Node(element);

        if (front == null) //si la cola esta vacia
        {
            front = newNode;
            back = newNode;
        }
        else
        {
            back.next = newNode; //agregar al final
            back = newNode;
        }

        count++;

        Debug.Log("Enqueue: " + element);
    }

    //remueve elemento q esta al frente
    public T Dequeue()
    {
        if (front == null) //si no hay elementos
        {
            Debug.Log("Cola vaca");
            return default(T);
        }

        T value = front.data;
        front = front.next;

        if (front == null) //ahora esta vacia
        {
            back = null;
        }

        count--;

        Debug.Log("Dequeue: " + value);
        return value;
    }

    //ver el frente sin removerlo
    public T Peek()
    {
        if (front == null)
        {
            Debug.Log("Cola vacia");
            return default(T);
        }

        return front.data;
    }

    //verifica si esta vacia
    public bool IsEmpty()
    {
        if (front == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //limpia toda la cola
    public void Clear()
    {
        front = null;
        back = null;
        count = 0;
        Debug.Log("Cola limpiada");
    }
}
