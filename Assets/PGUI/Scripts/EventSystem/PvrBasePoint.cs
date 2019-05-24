using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;


[RequireComponent(typeof(LineRenderer))]
public class PvrBasePoint : MonoBehaviour
{
    [HideInInspector]
    public Camera Camera;
    public SpriteRenderer cursor;
    public static PvrBasePoint pointer;

    [Tooltip("射线的最大距离")]
    public float maxPointDistance = 10;
    public Ray ray;

    [HideInInspector]
    public Transform pointerTrans;

    [Tooltip("是否显示检测碰撞的射线")]
    public bool isShow = true;

    [Tooltip("碰撞射线的颜色")]
    public Color debugLineColor = Color.red;


    protected LineRenderer m_lineRenderer;
    protected Vector3 hitPoint;
    public bool isHit = false;
    private bool on = false;

    protected Vector3 originPos;
    protected Vector3 endPos;
    private Vector3 targetPos;


    protected bool m_IsActive;
    public bool isActive
    {
        get
        {
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
            m_lineRenderer.SetPosition(0, originPos);
            m_lineRenderer.SetPosition(1, endPos);
        }
    }
    protected virtual void OnUpdate()
    {

    }

    void Awake()
    {
        OnAwake();
        Camera = GetComponent<Camera>();
        cursor.transform.DOScale(Vector3.zero, 0.02f);
    }
    void Start()
    {
        OnStart();
    }

    public void OnUpDateRay()
    {
        ray.origin = pointerTrans.position;
        ray.direction = (pointerTrans.rotation * Vector3.forward).normalized;
    }
    public void SetEndPoint(Vector3 pos)
    {
        targetPos = pos;
        DOTween.To(() => endPos, x => endPos = x, pos, 0.002f);
    }
    void UpdateLineRender()
    {
#if UNITY_EDITOR
        if (!m_IsActiveLineRender) return;
#endif
        originPos = ray.origin;
        if (!isHit)
            endPos = ray.GetPoint(maxPointDistance);
        m_lineRenderer.SetPosition(0, originPos);
        m_lineRenderer.SetPosition(1, endPos);
    }


    void Update()
    {
        OnUpDateRay();
#if UNITY_EDITOR
        if (IsActiveLineRender() != m_IsActiveLineRender)
        {
            m_IsActiveLineRender = IsActiveLineRender();
            m_lineRenderer.enabled = m_IsActiveLineRender;
        }
#endif
        UpdateLineRender();
        if (cursor != null)
        {
            if (isHit)
            {
                if (!on)
                {
                    on = true;
                    Vector3 target = new Vector3(0.1f, 0.1f, 0.1f);
                    Vector3 curPos = cursor.transform.position;
                    if(targetPos.z<1.0f)
                        cursor.transform.localScale = 1.2f* target;
                    else
                        cursor.transform.localScale = targetPos.z * target;
                }
                cursor.transform.position = endPos;
            }
            else
            {
                on = false;
                cursor.transform.DOScale(Vector3.zero, 0.0002f);
            }
        }
        if (isShow)
        {
            Debug.DrawRay(ray.origin, ray.direction * maxPointDistance, debugLineColor);
        }
        OnUpdate();
    }

    private bool IsActiveLineRender()
    {
        return Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
    }
}
