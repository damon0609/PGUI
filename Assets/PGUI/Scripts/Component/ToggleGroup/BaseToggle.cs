using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface ITogglePanel
{
    void Enter();
    void Exit();
}

public class BaseToggle : MonoBehaviour
{
    protected ITogglePanel targetPanel;

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
        if (targetPanel != null)
            targetPanel.Exit();
    }

    //点击状态
    protected virtual void OnClick(GameObject go)
    {
    }

    //悬浮状态
    protected virtual void OnHover(GameObject go, bool isHover)
    {

    }
}
