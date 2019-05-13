using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PvrRay : MonoBehaviour
{
    private Ray ray;

    public GameObject cursorGo;
    public Transform direction;
    public Transform origin;
    private LineRenderer lineRenderer;
    protected  void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
    }
    protected  void Start()
    {
        lineRenderer.startWidth = 0.01f;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, origin.position);
        lineRenderer.SetPosition(1, ray.GetPoint(0.4f));

    }

    protected  void Update()
    {
        //cursorGo.transform.position = ray.GetPoint(0.4f);
        lineRenderer.useWorldSpace = true;
        lineRenderer.SetPosition(0, ray.origin);
        lineRenderer.SetPosition(1,ray.GetPoint(0.4f));
    }

    public void OnDrawGizmos()
    {
        //Gizmos.DrawSphere(ray.direction.normalized*0.5f,0.1f);
    }
}
