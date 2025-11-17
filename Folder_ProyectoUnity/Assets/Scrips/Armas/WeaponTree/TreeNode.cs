
using System.Collections.Generic;
using UnityEngine;

public class TreeNode<T>
{
    private T value;
    private TreeNode<T> parent;
    private List<TreeNode<T>> children;

    public TreeNode(T _value)
    {
        value = _value;
        children = new();
    }
    #region CoreMethods
    public void AddChild(TreeNode<T> child)
    {
        if (children == null) return;
        child.parent = this;
        children.Add(child);
    }

    public bool RemoveChild(TreeNode<T> child)
    {
        if (children == null) return false;

        if (children.Remove(child))
        {
            child.parent = null;
            return true;
        }
        return false;
    }
    #endregion
    #region AuxMethods
    public bool IsRoot() => parent == null;//->ojo
    public bool IsBranch() => children.Count > 0 && parent != null;
    public bool IsLeaf() => children.Count == 0;
    public int GetChildrenCount() => children.Count;
    #endregion
    public T Value => value;

    public TreeNode<T> Parent => parent;
    public List<TreeNode<T>> Children => children;
}
