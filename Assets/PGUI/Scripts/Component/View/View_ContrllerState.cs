using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class View_ContrllerState : BaseView
{
    public Image bg;
    public Text text;

    private Container2 override_Container2;

    public override void InitView()
    {
        base.InitView();
        if (override_Container2 != null)
        {
            bg.sprite = override_Container2.bg;
            text.text = override_Container2.text;
        }
    }

    public override void UpdateView()
    {
        base.UpdateView();
    }

    public override void DestroyView()
    {
        base.DestroyView();

    }

    protected override void OnAwake()
    {
        base.OnAwake();
    }

    protected override void OnStart()
    {
        base.OnStart();
        _resType = ResType.ControllerState;
        if (isOverride)
        {
            override_Container2 = new Container2();
            string value = DataManager.instance.GetDataByIDAndName("Tip_1000","value");
            string[] list = value.Split('/');
            Sprite sprite = ResManager.instance.GetObject<Sprite>(list[0]);
            override_Container2.bg = sprite;
            override_Container2.text = list[1];
            InitView();
        }
    }
}
