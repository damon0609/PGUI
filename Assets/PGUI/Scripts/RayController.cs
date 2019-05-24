using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RayController : MonoBehaviour
{

    public Transform uiPanel;
    public Transform start;
    private Transform m_Tran;
    private Ray ray;
    private Plane plane;
    Vector3[] posList = new Vector3[4];
    private RectTransform rectTransform;
    void Start()
    {
        m_Tran = transform;
        ray = new Ray { origin = start.position, direction = (m_Tran.position - start.position).normalized };
        plane = new Plane(-uiPanel.forward, uiPanel.position);
        rectTransform = uiPanel as RectTransform;
        rectTransform.GetWorldCorners(posList);
    }

    void UpdateRay()
    {
        ray.origin = start.position;
        ray.direction = (m_Tran.position - start.position).normalized;
    }
    void Update()
    {
        UpdateRay();
        Debug.DrawLine(ray.origin, ray.direction * 100 + ray.origin, Color.red);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 pos = ray.origin + ray.direction * distance;
            if (Mathf.Abs(pos.x) < Mathf.Abs(posList[0].x) && pos.y > posList[0].y && pos.y < posList[1].y)
            {
                m_Tran.position = pos;
                m_Tran.DOScale(new Vector3(120f,0.24f,0.0f),0.2f);
            }
            else
            {
                m_Tran.localPosition = new Vector3(0, 3.7f, 0);
                m_Tran.DOScale(new Vector3(58.0f, 0.1f, 0.0f),0.2f);
            }
        }
        else
        {
            m_Tran.localPosition = new Vector3(0, 3.7f, 0);
            m_Tran.DOScale(new Vector3(58.0f, 0.1f, 0.0f), 0.2f);
        }
    }


}
