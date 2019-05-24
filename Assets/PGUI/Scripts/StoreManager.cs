using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour, ITogglePanel
{
    private CanvasGroup m_canvasGroup;

    public CanvasGroup list_canvasGroup;
    public CanvasGroup back_canvasGroup;
    public CanvasGroup main_canvasGroup;

    public GameObject upPage;
    public GameObject downPage;

    public GameObject backBtn;
    public Text expandAll;
    public GameObject group;

    public GameObject group01;
    public GameObject group02;

    public static StoreManager instance;

    private bool on = false;
    private int curPage=1;

    public void ExpanAllCallBack()
    {
        if (!on)
        {
            on = true;
            list_canvasGroup.DOFade(1.0f, 0.2f);
            back_canvasGroup.DOFade(1.0f, 0.2f);
            main_canvasGroup.DOFade(0.0f, 0.0f);
            expandAll.raycastTarget = false;
            back_canvasGroup.interactable = true;
            back_canvasGroup.blocksRaycasts = true;
            upPage.GetComponent<Image>().raycastTarget=true;
            downPage.GetComponent<Image>().raycastTarget = true;
        }
        else
        {
            on = false;
            expandAll.raycastTarget = true;
            back_canvasGroup.interactable = false;
            back_canvasGroup.blocksRaycasts = false;
            upPage.GetComponent<Image>().raycastTarget = false;
            downPage.GetComponent<Image>().raycastTarget = false;
            list_canvasGroup.DOFade(0.0f, 0.0f);
            back_canvasGroup.DOFade(0.0f, 0.0f);
            main_canvasGroup.DOFade(1.0f, 0.2f);
        }
    }

    public void Enter()
    {
        if (m_canvasGroup != null)
            m_canvasGroup.DOFade(1.0f, 0.2f);
        expandAll.raycastTarget = true;
        NearUIManager.instance.panelType = PanelType.Store;
    }
    public void Exit()
    {
        if (m_canvasGroup != null)
            m_canvasGroup.alpha = 0.0f;
        expandAll.raycastTarget = false;
        ExpanAllCallBack();
    }
    void Start () {
        instance = this;
        m_canvasGroup = transform.GetComponent<CanvasGroup>();
        UGUIEventListener.Get(backBtn).onClick += OnClick;
        UGUIEventListener.Get(backBtn).onHover += OnHover;
        UGUIEventListener.Get(backBtn).onPress += OnPress;

        UGUIEventListener.Get(upPage).onClick += UpPage_OnClick;
        UGUIEventListener.Get(upPage).onHover += UpPage_OnHover;
        UGUIEventListener.Get(upPage).onPress += UpPage_OnPress;

        UGUIEventListener.Get(downPage).onClick += DownPage_OnClick;
        UGUIEventListener.Get(downPage).onHover += DownPage_OnHover;
        UGUIEventListener.Get(downPage).onPress += DownPage_OnPress;
    }

    void UpPage_OnClick(GameObject go)
    {
        curPage--;
        if (curPage != 2)
            downPage.SetActive(true);
        else
            downPage.SetActive(false);
        if (curPage != 1)
            upPage.SetActive(true);
        else
            upPage.SetActive(false);
        if (curPage < 1)
        {
            return;
        }
        group.transform.DOLocalMoveX(0, 0.6f);
        group01.GetComponent<CanvasGroup>().DOFade(1.2f, 0.2f);
        group02.GetComponent<CanvasGroup>().DOFade(0.2f, 0.2f);
    }
    void UpPage_OnHover(GameObject go,bool isHover)
    {
        if (isHover)
        {
            upPage.transform.DOLocalMoveZ(-20, 0.2f);
        }
        else
        {
            upPage.transform.DOLocalMoveZ(0, 0.2f);
        }
    }
    void UpPage_OnPress(GameObject go,bool isPress)
    {
        if (isPress)
        {
            upPage.transform.DOLocalMoveZ(-10, 0.2f);
        }
        else
        {
            upPage.transform.DOLocalMoveZ(-20, 0.2f);
        }
    }
    void DownPage_OnClick(GameObject go)
    {
        curPage++;
        if (curPage != 2)
            downPage.SetActive(true);
        else
            downPage.SetActive(false);
        if (curPage != 1)
            upPage.SetActive(true);
        else
            upPage.SetActive(false);
        if (curPage > 2)
        {
            return;
        }
        group.transform.DOLocalMoveX(-790,0.6f);
        group01.GetComponent<CanvasGroup>().DOFade(0.2f,0.2f);
        group02.GetComponent<CanvasGroup>().DOFade(1.0f, 0.2f);
    }

    void DownPage_OnHover(GameObject go, bool isHover)
    {
        if (isHover)
        {
            downPage.transform.DOLocalMoveZ(-20, 0.2f);
        }
        else
        {
            downPage.transform.DOLocalMoveZ(0, 0.2f);
        }
    }
    void DownPage_OnPress(GameObject go, bool isPress)
    {
        if (isPress)
        {
            downPage.transform.DOLocalMoveZ(-10, 0.2f);
        }
        else
        {
            downPage.transform.DOLocalMoveZ(-20, 0.2f);
        }
    }
    void OnPress(GameObject go, bool isPress)
    {
        if (isPress)
        {
            backBtn.transform.DOLocalMoveZ(-10, 0.2f);
        }
        else
        {
            backBtn.transform.DOLocalMoveZ(-20, 0.2f);
        }
    }
    void OnClick(GameObject go)
    {
        ExpanAllCallBack();
        back_canvasGroup.interactable = false;
        back_canvasGroup.blocksRaycasts = false;
        backBtn.transform.DOLocalMoveZ(0, 0.0f);
    }
    void OnHover(GameObject go, bool isHover)
    {
        if (isHover)
        {
            backBtn.transform.DOLocalMoveZ(-20, 0.2f);
        }
        else
        {
            backBtn.transform.DOLocalMoveZ(0, 0.2f);
        }
    }
}
