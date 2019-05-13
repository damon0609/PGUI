using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private const string MOUSE_X = "Mouse X";
    private const string MOUSE_Y = "Mouse Y";


    private static float mouseX = 0.0f;
    private static float mouseY = 0.0f;
    private static float mouseZ = 0.0f;


    public Vector3 headOffset;

    void Start () {
		
	}
	void Update () {

        bool roll = false;
        if (ChangeRoll())
        {
            //控制前后旋转
            roll = true;
            mouseZ += Input.GetAxis(MOUSE_X) * 5.0f;
            mouseZ = Mathf.Clamp(mouseZ, -85, 85);
        }
        else if (ChangeYawPitch())
        {
            //控制垂直方向旋转
            mouseX += Input.GetAxis(MOUSE_X)*5.0f;
            if (mouseX > 180)
                mouseX -= 360;
            else if(mouseX < -180)
                mouseX += 360;


            //控制水平方向旋转
            mouseY -= Input.GetAxis(MOUSE_Y) * 4.4f;
            mouseY = Mathf.Clamp(mouseY,-85,85);
        }
        if (!roll)
        {
            mouseZ = Mathf.Lerp(mouseZ,0,Time.deltaTime/(Time.deltaTime+0.1f));
        }


        IEnumerator<Camera> cams = validCamera();
        while (cams.MoveNext())
        {
            Camera cur = cams.Current;

            cur.transform.localPosition = (Quaternion.Euler(mouseY, mouseX, mouseZ)* headOffset -  Vector3.up) * cur.transform.lossyScale.y;
            cur.transform.localRotation= Quaternion.Euler(mouseY, mouseX, mouseZ);
        }
    }


    IEnumerator<Camera> validCamera()
    {
        Camera[] cams = Camera.allCameras;
        for (int i = 0; i < cams.Length; i++)
        {
            Camera cam = cams[i];
            if (cam.stereoTargetEye == StereoTargetEyeMask.None)
                continue;

            yield return cam;
        }
    }



    private bool ChangeRoll()
    {
        bool on = false;
        if (IsActiveCtrl() && !IsActiveShift() && !IsActiveAlt())
            on = true;
        else
            on = false;
        return on;
    }

    private bool ChangeYawPitch()
    {
        bool on = false;
        if (!IsActiveCtrl() && !IsActiveShift() && IsActiveAlt())
            on = true;
        else
            on = false;
        return on;
    }

    private bool IsActiveShift()
    {
        if (!Input.mousePresent)
            return false;

        return Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift);
    }

    private bool IsActiveCtrl()
    {
        if (!Input.mousePresent)
            return false;

        return Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
    }

    private bool IsActiveAlt()
    {
        if (!Input.mousePresent)
            return false;

        return Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt);
    }
}
