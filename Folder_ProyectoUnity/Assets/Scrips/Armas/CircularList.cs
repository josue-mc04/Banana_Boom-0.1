using UnityEngine;

public class CircularList<T>
{
    private NodeWeapon<T> head = null;
    private NodeWeapon<T> current = null;

    //agregar un elemento
    public void Add(T value)
    {
        NodeWeapon<T> newNode = new NodeWeapon<T>(value);

        if (head == null)
        {
            //primer nodo
            head = newNode;
            head.SetNext(head); //circular, aqui se apunta asi mismo
            head.SetPrev(head);
            current = head;
        }
        else
        {
            //el headPrev es el ultimo
            //busca el ultimo
            NodeWeapon<T> tail = head.Prev;

            //insertar entre tail y head
            tail.SetNext(newNode);
            newNode.SetPrev(tail);

            newNode.SetNext(head);
            head.SetPrev(newNode);
        }
    }

    //obtener valor actual
    public T GetCurrent()
    {
        if (current == null)
        {
            return default;
        }

        return current.Value;
    }

    //avanza de forma circular
    public void Next()
    {
        if (current != null)
        {
            current = current.Next;
        }
    }

    //retrocede
    public void Prev()
    {
        if (current != null)
        { 
            current = current.Prev; 
        }
    }
}
