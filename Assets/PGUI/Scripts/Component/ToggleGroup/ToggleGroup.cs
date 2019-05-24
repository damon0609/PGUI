using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ToggleGroup : MonoBehaviour {

    public BaseToggle[] toggles;

    [HideInInspector]
    public BaseToggle curToggle;

    [HideInInspector]
    public BaseToggle lastToggle;




	void Start () {

        toggles = GetComponentsInChildren<BaseToggle>();
        if (toggles.Length <= 0 || toggles == null)
            return;

        for (int i = 0; i < toggles.Length; i++)
        {
            BaseToggle temp = toggles[i];
            temp.toggleGroup = this;
            if (!temp.isSelected)
                continue;
            else
                temp.Init();
        }
    }

    public void SetSelected(BaseToggle toggle)
    {
        lastToggle = curToggle;
        lastToggle.ResetState();
        curToggle = toggle;
    }
}


