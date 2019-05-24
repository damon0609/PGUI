using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pvr_UnitySDKAPI;
using DG.Tweening;
public class HomeManager : MonoBehaviour, ITogglePanel
{
    public enum WinType
    {
        None,
        Dialog,
        Tips,
    }


    private CanvasGroup m_canvasGroup;
    public CanvasGroup dialog01;
    public CanvasGroup tips01;
    private bool on = false;
    private bool onTips = false;
    public WinType winType;

    public void Enter()
    {
        if (m_canvasGroup != null)
            m_canvasGroup.DOFade(1.0f, 0.6f);
        NearUIManager.instance.panelType = PanelType.Home;
    }
    public void Exit()
    {
        if (m_canvasGroup != null)
            m_canvasGroup.alpha = 0.0f;

        on = false;
        onTips = false;
        dialog01.DOFade(0.0f, 0.0f);
        tips01.DOFade(0.0f, 0.0f);
        winType = WinType.None;
    }

    void Start () {
        m_canvasGroup = transform.GetComponent<CanvasGroup>();
        winType = WinType.None;
    }
	
	void Update ()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.TRIGGER)&&NearUIManager.instance.panelType==PanelType.Home)
        {
            if (!on && winType == WinType.None)
            {
                on = true;
                dialog01.DOFade(1.0f, 0.6f);
                m_canvasGroup.DOFade(0.2f, 0.2f);
                winType = WinType.Dialog;
            }
            else
            {
                if (winType == WinType.Dialog)
                {
                    on = false;
                    dialog01.DOFade(0.0f, 0.6f);
                    m_canvasGroup.DOFade(1.0f, 0.2f);
                    winType = WinType.None;
                }
            }
        }
         if (Controller.UPvr_GetKeyDown(0, Pvr_KeyCode.APP)&&NearUIManager.instance.panelType==PanelType.Home)
        {
            if (!onTips&& winType == WinType.None)
            {
                onTips = true;
                tips01.DOFade(1.0f, 0.6f);
                m_canvasGroup.DOFade(0.2f, 0.2f);
                winType = WinType.Tips;
            }
            else
            {
                if (winType == WinType.Tips)
                {
                    onTips = false;
                    tips01.DOFade(0.0f, 0.6f);
                    m_canvasGroup.DOFade(1.0f, 0.2f);
                    winType = WinType.None;
                }
            }
        }

#elif UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.A)&&NearUIManager.instance.panelType==PanelType.Home)
        {
            if (!on&&winType==WinType.None)
            {
                on = true;
                dialog01.DOFade(1.0f, 0.6f);
                m_canvasGroup.DOFade(0.2f, 0.2f);
                winType = WinType.Dialog;
            }
            else
            {
                if (winType == WinType.Dialog)
                {
                    on = false;
                    dialog01.DOFade(0.0f, 0.6f);
                    m_canvasGroup.DOFade(1.0f, 0.2f);
                    winType = WinType.None;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.D)&&NearUIManager.instance.panelType == PanelType.Home )
        {
            if (!onTips && winType == WinType.None)
            {
                onTips = true;
                tips01.DOFade(1.0f, 0.6f);
                m_canvasGroup.DOFade(0.2f, 0.2f);
                winType = WinType.Tips;

            }
            else
            {
                if (winType == WinType.Tips)
                {
                    onTips = false;
                    tips01.DOFade(0.0f, 0.6f);
                    m_canvasGroup.DOFade(1.0f, 0.2f);
                    winType = WinType.None;
                }
            }
        }
#endif
    }
}
