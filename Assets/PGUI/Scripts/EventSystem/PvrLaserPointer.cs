using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PvrLaserPointer : PvrBasePoint
{
    protected override void OnAwake()
    {
        base.OnAwake();
        pointer = this;
        pointerTrans = transform;
        ray = new Ray(pointerTrans.position, pointerTrans.forward);
        m_IsActive = true;
        m_lineRenderer = GetComponent<LineRenderer>();
        originPos = ray.origin;
        if (!isHit)
            endPos = ray.direction.normalized * maxPointDistance;

#if UNITY_EDITOR
        m_lineRenderer.enabled = m_IsActiveLineRender;
#elif !UNITY_EDITOR&&UNITY_ANDROID
        m_lineRenderer.enabled=true;
#endif
    }
}
