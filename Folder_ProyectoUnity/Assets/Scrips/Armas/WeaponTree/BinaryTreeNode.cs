using System.Collections.Generic;
using UnityEngine;

public class BinaryTreeNode<T>
{
    private T value;
    private BinaryTreeNode<T> parent;
    private BinaryTreeNode<T> left;
    private BinaryTreeNode<T> right;


    public BinaryTreeNode(T _value)
    {
        value = _value;
        left = null;
        right = null;

    }
    #region CoreMethods
    public void SetLeft(BinaryTreeNode<T> node)
    {
        if(node == null) return;

        node.parent = this;
        left = node;

    }
    public void SetRight(BinaryTreeNode<T> node)
    {
        if (node == null) return;

        node.parent = this;
        right = node;

    }
    public bool RemoveLeft()
    {
        if(left == null) return false;

        left.parent = null;
        left = null;
        return true;

    }
    public bool RemoveRight()
    {
        if (right == null) return false;

        right.parent = null;
        right = null;
        return true;

    }
    #endregion
    #region AuxMethods
    public bool IsRoot() => parent == null;//->ojo
    public bool IsBranch() => (left != null || right != null) && parent != null;
    public bool IsLeaf() => left == null && right == null;
    #endregion
    public T Value => value;
    public BinaryTreeNode<T> Parent => parent;
    public BinaryTreeNode<T> Right => right;
    public BinaryTreeNode<T> Left => left;
}
