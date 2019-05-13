using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IView
{
    ResType resType { get; }
    bool isOverride {get;}

    void InitView();
    void UpdateView();
    void DestroyView();
}

public class BaseView : MonoBehaviour, IView
{
    protected ResType _resType;
    public ResType resType
    {
        get
        {
            return _resType;
        }
    }
    public bool _isOverride;
    public bool isOverride
    {
        get
        {
            return _isOverride;
        }
    }

    public virtual void DestroyView()
    {
    }

    public virtual void InitView()
    {
    }

    public virtual void UpdateView()
    {
    }

    protected virtual void OnAwake()
    {
    }
    protected virtual void OnStart()
    {
    }

    private void Start()
    {
        OnStart();
    }
    private void Awake()
    {
        OnAwake();
    }
}


public abstract class Container
{

}

[System.Serializable]
public class Container2:Container
{
    public Sprite bg;
    public string text;
}