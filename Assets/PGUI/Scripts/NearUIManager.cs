using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PanelType
{
    Home,
    Store,
    FileManager,
    Other,
}

public class NearUIManager : MonoBehaviour {

    public PanelType panelType;
    public static NearUIManager instance;
    void Awake()
    {
        instance = this;
    }

	void Start () {
		
	}
	
	void Update () {

		
	}
}
