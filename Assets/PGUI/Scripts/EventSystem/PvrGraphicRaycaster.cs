using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class PvrGraphicRaycaster : BaseRaycaster
{
    public enum BlockingObjects
    {
        None = 0,
        TwoD = 1,
        ThreeD = 2,
        All = 3,
    }

    private const int NO_EVENT_MASK_SET = -1;

    public bool ignoreReversedGraphics = true;
    public BlockingObjects blockingObjects = BlockingObjects.None;
    public LayerMask blockingMask = NO_EVENT_MASK_SET;

    private static readonly List<Graphic> sortedGraphics = new List<Graphic>();
    private List<Graphic> raycastResults = new List<Graphic>();

    private Canvas m_Canvas;
    private Canvas canvas
    {
        get
        {
            if (m_Canvas == null)
            {
                m_Canvas = GetComponent<Canvas>();
                if (m_Canvas == null)
                    m_Canvas = gameObject.AddComponent<Canvas>();
            }
            return m_Canvas;
        }
    }
    public override Camera eventCamera
    {
        get
        {

            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                return PvrBasePoint.pointer.Camera;
                //return canvas.worldCamera == null ? Camera.main : canvas.worldCamera;
            }
            else
            {
                Debug.LogError("the renderMode of canvas must is worldSpace");
                return null;
            }
        }
    }


    public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        if (canvas == null||eventCamera==null) return;

        PvrBasePoint pointer = PvrLaserPointer.pointer;
        float hitDistance = float.MinValue;
        if (blockingObjects != BlockingObjects.None)
        {
            float dist = eventCamera.farClipPlane - eventCamera.nearClipPlane;
            if (blockingObjects == BlockingObjects.ThreeD || blockingObjects == BlockingObjects.All)
            {
                RaycastHit hit;
                if (Physics.Raycast(pointer.ray, out hit, dist, blockingMask))
                {
                    hitDistance = hit.distance;
                }
            }
        }
        pointer.isHit = false;
        raycastResults.Clear();
        PreformGraphics(pointer,raycastResults);
        for (int i = 0; i < raycastResults.Count; i++)
        {
            GameObject go = raycastResults[i].gameObject;
            Plane plane = new Plane(go.transform.forward, go.transform.position);
            float enter = 0.0f;
            if (plane.Raycast(pointer.ray, out enter))
            {
                pointer.isHit = true;
                pointer.SetEndPoint(pointer.ray.GetPoint(enter));
            }
        }
    }
    private void PreformGraphics(PvrBasePoint pointer,List<Graphic> results)
    {
        Vector2 screenPos = eventCamera.WorldToScreenPoint(pointer.ray.GetPoint(pointer.maxPointDistance));
        var graphics = GraphicRegistry.GetGraphicsForCanvas(canvas);
        for (int i = 0; i < graphics.Count; i++)
        {
            Graphic g = graphics[i];
            if (g.depth == -1 || !g.raycastTarget)
            {
                continue;
            }

            if (!RectTransformUtility.RectangleContainsScreenPoint(g.rectTransform, screenPos, eventCamera))
            {
                continue;
            }
            if (g.Raycast(screenPos, eventCamera))
            {
                sortedGraphics.Add(g);
            }
            sortedGraphics.Sort((g1,g2)=>g1.depth.CompareTo(g.depth));
            foreach (var s in sortedGraphics)
            {
                results.Add(s);
            }
            sortedGraphics.Clear();
        }
    }
}
