using System;
using System.Collections.Generic;
using UnityEngine;


public enum ResType
{
    ControllerState,
}

public enum DataType
{
    Data,
    Component,
    Texture,
}


/// <summary>
/// 直接从Resources文件中加载资源
/// 
/// </summary>
public class ResManager:Singleton<ResManager>
{
    //资源名称
    public const string ctrlState01 = "ControllerState_01";
    public const string ctrlState02 = "";
    public const string ctrlState03 = "";
    public const string ctrlState04 = "";
    public const string ctrlState05 = "";


    //不同的数据类型
    private readonly string dataPath = "DataTable/";
    private readonly string componentPath= "Component/";
    private readonly string texturePath = "Texture/";


    public TextAsset GetText(string assetName)
    {
        return Resources.Load<TextAsset>(dataPath+assetName);
    }


    public T GetObject<T>(string name) where T : UnityEngine.Object
    {
        return Resources.Load<T>("Texture/" + name);
    }



    //同步加载游戏对象引用
    public GameObject GetObject(string objName)
    {
        string path = componentPath + objName;
        GameObject go = null;
        go = (GameObject) Resources.Load(path);
        return go;
    }

    //同步加载游戏对象的实例
    public GameObject GetCloneObject(string objName)
    {
        GameObject go = null;
        go = GameObject.Instantiate<GameObject>(GetObject(objName));
        return go;
    }

    public GameObject GetCloneObject(string objName, GameObject parent)
    {
        GameObject go = GetCloneObject(objName);
        go.transform.parent = parent.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        return go;
    }
}
