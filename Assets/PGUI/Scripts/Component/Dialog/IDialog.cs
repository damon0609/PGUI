using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



interface IDialog
{
    DialogType type { get; set; }
    void Open();
    void Close();
}
