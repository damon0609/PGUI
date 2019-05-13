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
            if (!eventSystem.isFocused&&!Pvr_ControllerManager.controllerlink.controller0Connected) return;
            PrcessControllerEvent();
        }
    }

    private void PrcessControllerEvent()
    {
        PvrBasePoint pointer = PvrLaserPointer.pointer;
        ControllerTouchPadEvent();
    }

    //检测手柄touchPad事件
    private void ControllerTouchPadEvent()
    {
        if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TOUCHPAD))
        {
            ExecuteEvents.Execute<IPointerClickHandler>(null,null,ExecuteEvents.pointerClickHandler);
        }
    }




}
