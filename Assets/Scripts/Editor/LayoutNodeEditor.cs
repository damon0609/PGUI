/*
*author:
*data:
*description:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Pico.PUI;

[CustomEditor(typeof(LayoutNode))]
public class LayoutNodeEditor : Editor
{
    private SerializedProperty m_Scripts;
    private SerializedProperty isShow;
    private SerializedProperty fillType;
    private SerializedProperty rectTransform;
    private RectTransform m_RectTransform;
    private LayoutNode.FillType lastFillType;
    private bool on;
    private Vector3 rotate;
    private void OnEnable()
    {
        m_Scripts = serializedObject.FindProperty("m_Script");
        isShow = serializedObject.FindProperty("isShow");
        fillType = serializedObject.FindProperty("fillType");
        rectTransform = serializedObject.FindProperty("rectTransform");
        m_RectTransform = rectTransform.objectReferenceValue as RectTransform;
        lastFillType = (LayoutNode.FillType)fillType.enumValueIndex;
        isShow.boolValue = LayoutNodeManager.instance.on;
        on = isShow.boolValue;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GUI.enabled = false;
        EditorGUILayout.PropertyField(m_Scripts);
        GUI.enabled = true;

        EditorGUILayout.LabelField("LayoutNodeName:", m_RectTransform.name, EditorStyles.boldLabel);
        m_RectTransform.anchoredPosition = EditorGUILayout.Vector2Field("pos", m_RectTransform.anchoredPosition);
        m_RectTransform.sizeDelta = EditorGUILayout.Vector2Field("size", m_RectTransform.sizeDelta);
        m_RectTransform.eulerAngles = EditorGUILayout.Vector3Field("Rotate", m_RectTransform.eulerAngles);

        isShow.boolValue = EditorGUILayout.Toggle("isShow", on);
        on = isShow.boolValue;
        EditorGUILayout.PropertyField(fillType);
        if (GUI.changed)
        {
            LayoutNode.FillType m_FillType = (LayoutNode.FillType)fillType.enumValueIndex;
            if (lastFillType == m_FillType)
                return;
            else
            {
                switch (m_FillType)
                {
                    case LayoutNode.FillType.FillParent:
                        m_RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
                        m_RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
                        m_RectTransform.anchorMin = Vector2.zero;
                        m_RectTransform.anchorMax = Vector2.one;
                        m_RectTransform.pivot = new Vector2(0.5f, 0.5f);
                        break;
                    case LayoutNode.FillType.Wrap:
                        m_RectTransform.sizeDelta = new Vector2(2800, 1600);
                        m_RectTransform.pivot = new Vector2(0, 1);
                        m_RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, m_RectTransform.sizeDelta.x);
                        m_RectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, m_RectTransform.sizeDelta.y);
                        break;
                    default:
                        Debug.LogError("the fillType is not exist");
                        break;
                }
                lastFillType = m_FillType;
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}
