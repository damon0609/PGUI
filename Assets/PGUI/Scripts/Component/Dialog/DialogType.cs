using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public enum DialogType:int
{
    UnKnow=0,
    ControllerState,//手柄连接状态
    ControllerRecenter,//手柄连接后校准提示

    UpdateTip,//版本最新，无需更新

    Temperature,//温度过高提示
}
