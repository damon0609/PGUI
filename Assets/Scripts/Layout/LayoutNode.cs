/*
*author:
*data:
*description:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class LayoutNode : MonoBehaviour
{
    [HideInInspector]
    public RectTransform rectTransform;
    public bool isShow;

    private Vector3[] m_worldCorners = new Vector3[4];

    public enum FillType
    {
        FillParent,
        Wrap,
    }
    public FillType fillType = FillType.Wrap;

    public void OnSetRectTransform(Vector2 pos,Vector2 size,Vector3 rotation,FillType type)
    {
        if (rectTransform != null)
        {
            switch (type)
            {
                case LayoutNode.FillType.FillParent:
                    rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
                    rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
                    rectTransform.anchorMin = Vector2.zero;
                    rectTransform.anchorMax = Vector2.one;
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);
                    break;
                case LayoutNode.FillType.Wrap:
                    rectTransform.sizeDelta = new Vector2(size.x, size.y);
                    rectTransform.pivot = new Vector2(0, 1);
                    rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, size.x);
                    rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, size.y);
                    rectTransform.anchoredPosition = pos;
                    rectTransform.sizeDelta = size;
                    rectTransform.eulerAngles = rotation;
                    break;
                default:
                    Debug.LogError("the fillType is not exist");
                    break;
            }
            rectTransform.GetWorldCorners(m_worldCorners);
        }
    }
    private void OnDrawGizmos()
    {
        if (isShow)
        {
            rectTransform.GetWorldCorners(m_worldCorners);
            foreach (Vector3 pos in m_worldCorners)
            {
                Gizmos.DrawSphere(pos, 0.01f);
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(m_worldCorners[0],m_worldCorners[1]);
            Gizmos.DrawLine(m_worldCorners[1], m_worldCorners[2]);
            Gizmos.DrawLine(m_worldCorners[2], m_worldCorners[3]);
            Gizmos.DrawLine(m_worldCorners[0], m_worldCorners[3]);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (isShow)
        {
            Gizmos.color = Color.yellow;
            rectTransform.GetWorldCorners(m_worldCorners);
            foreach (Vector3 pos in m_worldCorners)
                Gizmos.DrawSphere(pos, 0.01f);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(m_worldCorners[0], m_worldCorners[1]);
            Gizmos.DrawLine(m_worldCorners[1], m_worldCorners[2]);
            Gizmos.DrawLine(m_worldCorners[2], m_worldCorners[3]);
            Gizmos.DrawLine(m_worldCorners[0], m_worldCorners[3]);
        }
    }

    #region
    void Awake()
    {

    }


    void Start()
    {

    }


    void Update()
    {

    }
    #endregion
}
