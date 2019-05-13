using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputControllerMode:int
{
    None=0,
    Head=1,
    Controller,
}

public enum ControllerConnectState:int
{
    Connect=0,
    DisConnect,
    Unknow,
}


public class InputControllerManager : MonoBehaviour
{
    [Tooltip("输入设备的模式")]
    public InputControllerMode controllerMode=InputControllerMode.None;

    public PvrBaseInput m_Input;

    public static InputControllerManager instance;

    public Transform m_Controller;
    public Transform m_Head;

    public GameObject controllerModel;

    public bool isShow = true;

    public System.Action<InputControllerMode> controllerModeChanage;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {

#if UNITY_ANDROID && !UNITY_EDITOR
        //Pvr_ControllerManager.PvrServiceStartSuccessEvent += () => { Debug.Log("damon:PvrServiceStartSuccessEvent"); };
        //Pvr_ControllerManager.ControllerStatusChangeEvent += str =>
        //{
        //    if (Pvr_ControllerManager.controllerlink.controller0Connected)
        //    {
        //        controllerMode = InputControllerMode.Controller;
        //        m_Input = new InputController(m_Controller.position, m_Controller.forward);
        //        m_Input.pointerTran = m_Controller;
        //    }
        //    else
        //    {
        //        controllerMode = InputControllerMode.Head;
        //        m_Input = new HeadController(m_Head.position, m_Head.forward);
        //        m_Input.pointerTran = m_Head;
        //    }
        //};
#else
        //if (controllerMode == InputControllerMode.Controller)
        //{
        //    m_Input = new InputController(m_Controller.position, m_Controller.forward, controllerModel);
        //    m_Input.pointerTran = m_Controller;
        //    if (controllerModeChanage != null)
        //        controllerModeChanage(controllerMode);
        //}
        //else if (controllerMode == InputControllerMode.Head)
        //{
        //    m_Input = new HeadController(m_Head.position, m_Head.forward);
        //    m_Input.pointerTran = m_Head;
        //    if (controllerModeChanage != null)
        //        controllerModeChanage(controllerMode);
        //}
#endif
    }
    void Update()
    {
        if (isShow&&m_Input!=null)
        {
            //Debug.DrawRay(m_Input.ray.origin, m_Input.ray.direction.normalized,Color.red);
        }
    }
}


[System.Serializable]
public class PvrBaseInput
{
    protected int m_MaxDistance = 3;
    public int maxDistance
    {
        get { return m_MaxDistance; }
    }

    protected Ray m_ray;
    public Ray ray
    {
        get { return m_ray; }
    }

    protected GameObject visualControllerGo;


    [System.NonSerialized]
    public Transform pointerTran;

    //指示的光标
    public Sprite cursorNormal;



    //连接状态
    protected ControllerConnectState m_ControllerConnectState;
    public ControllerConnectState controllerConnectState
    {
        get { return m_ControllerConnectState; }
    }

    public PvrBaseInput(Vector3 start, Vector3 direction)
    {
        m_ray = new Ray();
        m_ray.origin = start;
        m_ray.direction = direction;

        InputControllerManager.instance.controllerModeChanage += ControllerModeChangeCallBack;
    }

    protected bool m_IsActive = true;
    public bool IsActive
    {
        get { return m_IsActive; }
    }

    protected virtual void ControllerModeChangeCallBack(InputControllerMode mode)
    {

    }
    public void UpdateRay()
    {
        m_ray.origin = pointerTran.position;
        m_ray.direction = pointerTran.rotation*Vector3.forward;
    }
}

public class InputController : PvrBaseInput
{
    public InputController(Vector3 start, Vector3 direction,GameObject model) : base(start, direction)
    {
        this.visualControllerGo = model;
    }

    protected override void ControllerModeChangeCallBack(InputControllerMode mode)
    {
        base.ControllerModeChangeCallBack(mode);
        if (mode == InputControllerMode.Controller)
        {
            visualControllerGo.SetActive(true);
            m_IsActive = true;
        }


    }
}
public class HeadController : PvrBaseInput
{
    public HeadController(Vector3 start, Vector3 direction) : base(start, direction)
    {

    }
}