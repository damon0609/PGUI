using Pvr_UnitySDKAPI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PvrPointerInputModule : BaseInputModule
{

    public override void Process()
    {
        if (PvrLaserPointer.pointer.isActive)
        {

#if !UNITY_EDITOR && UNITY_ANDROID
            if (!eventSystem.isFocused || !Pvr_ControllerManager.controllerlink.controller0Connected) return;
#endif
            PrcessControllerEvent();
        }
    }

    private void PrcessControllerEvent()
    {
        //监听touchPad事件
        PointerEventData pointerEventData = null;
        if (pointerEventData == null)
        {
            pointerEventData = new PointerEventData(eventSystem);
        }

        if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TOUCHPAD))
        {
            ControllerTouchPadEvent();
        }


        m_RaycastResultCache.Clear();
        eventSystem.RaycastAll(pointerEventData, m_RaycastResultCache);
    }

    //检测手柄touchPad事件
    private void ControllerTouchPadEvent()
    {
        //Debug.Log("damon:touchPad");
    }




}
