using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogManager:MonoSingleton<DialogManager>
{
    private GameObject uiRoot;
    protected override void OnAwake()
    {
        base.OnAwake();
        uiRoot = GameObject.Find("Canvas_Tip");
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    public void Open(DialogType type)
    {
        switch (type)
        {
            case DialogType.ControllerState:
                Debug.Log("open dialogManager");
                ResManager.instance.GetCloneObject(ResManager.ctrlState01,uiRoot);
                break;
            case DialogType.ControllerRecenter:
                break;
            case DialogType.UnKnow:
                break;
        }
    }

    public void Close()
    {

    }
}
