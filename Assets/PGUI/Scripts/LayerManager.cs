using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerManager : MonoBehaviour {

    public GameObject controllerGo;

	void Start () {

        MeshRenderer[] renders = controllerGo.GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer render in renders)
        {
            render.sortingLayerName = "top";
        }
	}
	
}
