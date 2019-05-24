using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pvr_UnitySDKAPI;
using CurvedUI;
using UnityEngine.UI;
using DG.Tweening;

public class LayerManager : MonoBehaviour {

    public CurvedUISettings curvedUISettings;
    public GameObject controllerGo;
    public Text text;
    public GameObject menuList;

    private bool on = true;
    int angle = 0;

	void Start () {

        MeshRenderer[] renders = controllerGo.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer render in renders)
        {
            render.sortingLayerName = "top";
        }
        if (curvedUISettings != null)
        {
            angle = curvedUISettings.Angle;
            text.text = curvedUISettings.Angle.ToString();
        }
    }

    void Update()
    {
        if (curvedUISettings == null) return;

        SwipeDirection dir = Controller.UPvr_GetSwipeDirection(0);
        switch (dir)
        {
            case SwipeDirection.SwipeUp:
                angle -= 6;
                curvedUISettings.Angle = angle;
                text.text = curvedUISettings.Angle.ToString();
                break;
            case SwipeDirection.SwipeDown:
                angle += 6;
                curvedUISettings.Angle = angle;
                text.text = curvedUISettings.Angle.ToString();
                break;
        }

        if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.APP))
        {
            if (on)
            {
                on = false;
                menuList.GetComponent<CanvasGroup>().DOFade(0.0f,0.2f);
            }
            else
            {
                on = true;
                menuList.GetComponent<CanvasGroup>().DOFade(1.0f, 0.2f);
            }
        }
    }
}
