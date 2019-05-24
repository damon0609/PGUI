using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UGUIEventListener : EventTrigger
{

    public System.Action<GameObject> onClick;
    public System.Action<GameObject, bool> onHover;
    public System.Action<GameObject, bool> onPress;

    public static UGUIEventListener Get(GameObject go)
    {
        if (go == null)
            return null;
        UGUIEventListener listener = go.GetComponent<UGUIEventListener>();
        if (listener == null)
            listener = go.AddComponent<UGUIEventListener>();
        return listener;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (onPress != null)
            onPress(eventData.pointerEnter, true);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        if (onPress != null)
            onPress(eventData.pointerEnter, false);
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (onClick != null)
            onClick(eventData.pointerEnter);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        if (onHover != null)
            onHover(eventData.pointerEnter, true);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        if (onHover != null)
            onHover(eventData.pointerEnter, false);
    }

}
