using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPreview(typeof(PUIComponent))]
public class PUIComponentPreView : ObjectPreview
{
    PUIComponent temp;
    private Texture2D m_Preview;
    public override string GetInfoString()
    {
        m_Preview = temp.preview;
        return m_Preview!=null?m_Preview.width+"*"+m_Preview.height:"priview is null";
    }
    public override bool HasPreviewGUI()
    {
        return true;
    }
    public override GUIContent GetPreviewTitle()
    {
        return new GUIContent(temp!=null?temp.componentName : "Null");
    }
    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        temp = target as PUIComponent;
        if (temp != null && temp.preview != null)
        {
            Rect cenetrRect = new Rect(r.x+r.width/2-temp.preview.width/2,r.y+r.height/2-temp.preview.height/2,temp.preview.width,temp.preview.height);
            GUI.DrawTexture(cenetrRect, temp.preview);
        }
    }
}
