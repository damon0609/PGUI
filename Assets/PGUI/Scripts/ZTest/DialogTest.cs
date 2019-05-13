using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class DialogTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //DialogManager.instance.Open(DialogType.ControllerState);

        //通过StartInfo属性调用进程
        /*
        Process process = new Process();
        process.StartInfo.FileName = "iexplore.exe";
        process.StartInfo.Arguments = "http://www.baidu.com";
        process.Start();
        */

        //通过进程信息封装起来通过进程的静态方法调用
        /*
        ProcessStartInfo processStartInfo = new ProcessStartInfo();
        processStartInfo.FileName = "explorer.exe";
        processStartInfo.Arguments = @"D:\";
        Process.Start(processStartInfo);
        */

        //string path = @"C:\Program Files (x86)\Tencent\TIM\Bin\TIM.exe";
        //Process.Start(path);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
