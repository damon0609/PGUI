using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TestButtton : BaseToggle
{
    public Text titleText;
    public Sprite target;
    public GameObject panel;

    protected override void Start()
    {
        base.Start();
        if (panel != null)
            targetPanel = panel.GetComponent<ITogglePanel>();
    }
    public override void Init()
    {
        base.Init();
    }
    public override void ResetState()
    {
        base.ResetState();
        titleText.DOColor(new Color(166 / 255f, 166 / 255f, 166 / 255f, 1.0f), 0.2f);
    }
    protected override void OnClick(GameObject go)
    {
        base.OnClick(go);
        if (isSelected) return;
        isSelected = true;
        if (targetPanel != null)
            targetPanel.Enter();
        titleText.DOColor(new Color(121 / 255f, 97 / 255f, 1.0f, 1.0f), 0.2f);
        toggleGroup.SetSelected(this);

    }

    protected override void OnHover(GameObject go, bool isHover)
    {
        if (isSelected) return;
        if (isHover)
        {
            titleText.DOColor(new Color(121 / 255f, 97 / 255f, 1.0f, 1.0f), 0.2f);
        }
        else
        {
            titleText.DOColor(new Color(166 / 255f, 166 / 255f, 166 / 255f, 1.0f), 0.2f);
        }
    }
}
