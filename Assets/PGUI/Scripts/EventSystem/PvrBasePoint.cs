using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PvrBasePoint : MonoBehaviour {

    public static PvrBasePoint pointer;

    [Tooltip("射线的最大距离")]
    public float maxPointDistance = 20;

    public Ray ray;

    [HideInInspector]
    public Transform pointerTrans;

    [Tooltip("是否显示检测碰撞的射线")]
    public bool isShow = true;

    [Tooltip("碰撞射线的颜色")]
    public Color debugLineColor = Color.red;


    protected LineRenderer m_lineRenderer;
    protected Vector3 hitPoint;
    protected bool isHit = false;


    protected Vector3 originPos;
    protected Vector3 endPos;

    protected bool m_IsActive;
    public bool isActive
    {
        get {
            return m_IsActive;
        }
        set
        {
            m_IsActive = value;
        }
    }

    protected bool m_IsActiveLineRender = false;

    protected virtual void OnAwake()
    {

    }

    protected virtual void OnStart()
    {
        if (m_lineRenderer != null)
        {
            m_lineRenderer.startWidth = 0.01f;
            m_lineRenderer.endWidth = 0.01f;
            m_lineRenderer.positionCount = 2;
            m_lineRenderer.SetPosition(0,originPos);
            m_lineRenderer.SetPosition(1, endPos);
        }
    }

    protected virtual void OnUpdate()
    {

    }
    void Awake()
    {
        OnAwake();
    }


    public void OnUpDateRay()
    {
        ray.origin = pointerTrans.position;
        ray.direction = pointerTrans.rotation*Vector3.forward.normalized;
    }


    void UpdateLineRender()
    {

#if UNITY_EDITOR
        m_lineRenderer.enabled = m_IsActiveLineRender;
        if (!m_IsActiveLineRender)
            return;
#endif

        originPos = ray.origin;
        if (!isHit)
            endPos = ray.direction.normalized * maxPointDistance;

        m_lineRenderer.SetPosition(0, originPos);
        m_lineRenderer.SetPosition(1, endPos);
    }

    void Start () {
        OnStart();
    }

    void Update()
    {
        OnUpDateRay();
        OnUpdate();
        if (isShow)
        {
            Debug.DrawRay(ray.origin, ray.direction * maxPointDistance, debugLineColor);
        }

#if UNITY_EDITOR
        if (IsActiveLineRender()!=m_IsActiveLineRender)
            m_IsActiveLineRender = IsActiveLineRender();
#endif
        UpdateLineRender();
        
    }

    private bool IsActiveLineRender()
    {
        return Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift);
    }
}
