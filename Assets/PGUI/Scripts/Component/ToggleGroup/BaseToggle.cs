using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseToggle : MonoBehaviour
{
    public bool isSelected = false;

    [HideInInspector]
    public ToggleGroup toggleGroup;

    protected virtual void Start()
    {
        UGUIEventListener.Get(gameObject).onClick += OnClick;
        UGUIEventListener.Get(gameObject).onHover += OnHover;
    }

    //初始化状态
    public virtual void Init()
    {
        toggleGroup.curToggle = this;
    }

    //重置状态
    public virtual void ResetState()
    {
        isSelected = false;
    }

    //点击状态
    protected virtual void OnClick(GameObject go)
    {
        isSelected = true;
    }

    //悬浮状态
    protected virtual void OnHover(GameObject go, bool isHover)
    {

    }
}
