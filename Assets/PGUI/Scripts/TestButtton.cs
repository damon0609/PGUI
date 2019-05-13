using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TestButtton : BaseToggle
{
    public Text titleText;
    public Image panel;
    public Sprite target;

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
        isSelected = true;
        Debug.Log("damon--" + go.name);
        if (target != null)
            panel.sprite = target;
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
