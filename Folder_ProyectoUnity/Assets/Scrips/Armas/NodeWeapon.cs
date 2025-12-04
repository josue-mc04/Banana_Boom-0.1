using UnityEngine;

public class NodeWeapon<T>
{
    #region Properties
    private T value;
    private NodeWeapon<T> next;
    private NodeWeapon<T> prev;
    #endregion

    #region Constructors
    public NodeWeapon(T value)
    {
        this.value = value;
        next = null;
        prev = null;
    }
    public void SetNext(NodeWeapon<T> next)
    {
        this.next = next;
    }
    public void SetPrev(NodeWeapon<T> prev)
    {
        this.prev = prev;
    }
    public void Clear()
    {
        value = default;
        next = null;
        prev = null;
    }
    #endregion


    #region Getters
    public T Value => value;
    public NodeWeapon<T> Next => next;
    public NodeWeapon<T> Prev => prev;
    #endregion
}