using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ExpandAll : MonoBehaviour
{
    void Start()
    {
        UGUIEventListener.Get(gameObject).onClick += OnClick;
        UGUIEventListener.Get(gameObject).onHover += OnHover;
        UGUIEventListener.Get(gameObject).onPress += OnPress;
    }

    void OnPress(GameObject go, bool isPress)
    {
        if (isPress)
        {
            transform.DOLocalMoveZ(-10, 0.2f);
        }
        else
        {
            transform.DOLocalMoveZ(-20, 0.2f);
        }
    }
    void OnClick(GameObject go)
    {
        StoreManager.instance.ExpanAllCallBack();
    }
    void OnHover(GameObject go, bool isHover)
    {
        if (isHover)
        {
            transform.DOLocalMoveZ(-20,0.2f);
        }
        else
        {
            transform.DOLocalMoveZ(0, 0.2f);
        }
    }
}
