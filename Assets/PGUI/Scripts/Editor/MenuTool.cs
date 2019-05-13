using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


# if !UNITY_ANDROID
public class MenuTool
{

    [MenuItem("Tools/CreatePrefab")]
    public static void CreatePrefab()
    {
        GameObject[] gos = Selection.gameObjects;
        if (gos == null||gos.Length<=0) return;
        string path = EditorUtility.OpenFolderPanel("Selected Path", "Assets", "");
        path = path.Replace(Application.dataPath,"Assets");
        for (int i = 0; i < gos.Length; i++)
        {
            PrefabUtility.CreatePrefab(path + "/" + gos[i].name + ".prefab", gos[i]);
        }
    }
}
#endif