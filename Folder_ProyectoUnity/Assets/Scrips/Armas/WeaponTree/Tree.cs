using System;
using System.Collections.Generic;
using UnityEngine;

public class Tree<T>
{
    public TreeNode<T> Root;

    public Tree(T rootValue)
    {
        Root = new TreeNode<T>(rootValue);
    }

    public virtual TreeNode<T> Find(TreeNode<T> node , T value)
    {
        if(node == null) return null;

        //-> comparacion
        if(EqualityComparer<T>.Default.Equals(node.Value ,value))
            return node;

        //->Si fallo realizco la busqueda
        foreach(TreeNode<T> child in node.Children)
        {
            TreeNode<T> result = Find(child, value);
            if (result != null) return result;  
        }
        return null;    
    }

    //->No debetrian por definicion tener un metodo addChild

    public virtual bool AddChild(T parentValue , T ChildValue)
    {
        TreeNode<T> parentNode = Find(Root, parentValue);

        if (parentNode == null)
        {
            Debug.Log("El nodo que buscas nos e encuentra en el arbol");
            return false;
        }

        TreeNode<T> childNode = new(ChildValue);

        parentNode.AddChild(childNode);
        return true;
    }

    public void TraversePreOrder(TreeNode<T> node , Action<T> action)
    {
        if(node == null) return;
        action(node.Value);

        foreach (var child in node.Children)
        {
            TraversePreOrder(child, action);
        }
    }

    public void TraversePostOrder(TreeNode<T> node, Action<T> action)
    {
        if (node == null) return;

        foreach (var child in node.Children)//->añadimos a sus hijos
        {
            TraversePostOrder(child, action);
        }

        action(node.Value); //-> Registramos el nodo
    }
    public void TraverseLevelOrder(TreeNode<T> node, Action<T> action)
    {
        if (node == null) return;


        Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
        queue.Enqueue(node);

        while (queue.Count > 0)
        {
            TreeNode<T> current = queue.Dequeue(); 
            action(current.Value);  

            foreach(var child in current.Children)
            {
                queue.Enqueue(child);
            }

        }
    }
}
