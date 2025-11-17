using System;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree<T>
{
    public BinaryTreeNode<T> Root;

    public BinaryTree(T rootValue)
    {
        Root = new BinaryTreeNode<T>(rootValue);
    }
    public bool AddChild(T parentValue , T childValue , bool isLeft = true)//-> Hacer que detecte izquierda o derecha automaticamente
    {
        BinaryTreeNode<T> parentNode = Find(Root, parentValue);

        if (parentNode == null)
        {
            Debug.Log("Padre no encontrado");
            return false;
        }

        BinaryTreeNode<T> childNode = new BinaryTreeNode<T>(childValue);

        if(isLeft)
        {
            if (parentNode.Left != null) return false;

            parentNode.SetLeft(childNode);
        }
        else
        {
            if (parentNode.Right != null) return false;

            parentNode.SetRight(childNode);

        }

        return true;
    }

    public BinaryTreeNode<T> Find(BinaryTreeNode<T> node, T value)
    {
        if (node == null) return null;

        //-> comparacion
        if (EqualityComparer<T>.Default.Equals(node.Value, value))
            return node;

        //->Si fallo realizco la busqueda
        BinaryTreeNode<T> leftResult = Find(node.Left, value);
        if(leftResult != null) return leftResult;

        BinaryTreeNode<T> rightResult = Find(node.Right, value);
        return rightResult;
    }
    public void TraverseInOrder(BinaryTreeNode<T> node, Action<T> action)
    {
        if(node == null) return;

        //-> izquierda
        TraverseInOrder(node.Left, action);
        //-> al medio
        action(node.Value);
        //-> derecha
        TraverseInOrder(node.Right, action);
    }


    /*public void TraversePreOrder(BinaryTreeNode<T> node, Action<T> action)
    {
        if (node == null) return;
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

            foreach (var child in current.Children)
            {
                queue.Enqueue(child);
            }

        }
    }*/
}
