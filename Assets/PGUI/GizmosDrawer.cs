using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

[ExecuteInEditMode]
public class GizmosDrawer : MonoBehaviour {

    public bool isShow = true;
    public Color color=Color.white;
    RectTransform rectTransform;
    Vector3[] worldCorners = new Vector3[4];
    Vector3[] temp;
    void OnDrawGizmos()
    {
        if (!isShow) return;
        rectTransform.GetWorldCorners(worldCorners);
        Gizmos.color = color;
        Gizmos.DrawLine(worldCorners[0],worldCorners[1]);
        Gizmos.DrawLine(worldCorners[1], worldCorners[2]);
        Gizmos.DrawLine(worldCorners[2], worldCorners[3]);
        Gizmos.DrawLine(worldCorners[3], worldCorners[0]);
    }

    void OnDrawGizmosSelected()
    {
        if (!isShow) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(worldCorners[0], worldCorners[1]);
        Gizmos.DrawLine(worldCorners[1], worldCorners[2]);
        Gizmos.DrawLine(worldCorners[2], worldCorners[3]);
        Gizmos.DrawLine(worldCorners[3], worldCorners[0]);
    }

    void Start () {

        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
            rectTransform.GetWorldCorners(worldCorners);
        }
        temp = new Vector3[worldCorners.Length];
        temp = worldCorners;

#if UNITY_EDITOR
        EditorApplication.update += () =>
        {
           
        };
#endif
    }
}
