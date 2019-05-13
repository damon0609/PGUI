/*
*author:
*data:
*description:
*/
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Pico.PUI
{
    public class LayoutNodeManager : EditorWindow
    {
        public static LayoutNodeManager instance;
        public LayoutNodeManager()
        {
            instance = this;
        }

        private string layoutName = "LayoutNode";
        private Vector2 pos;
        private Vector2 size = Vector2.one * 100;
        private Vector3 rotation;
        private bool isShow = true;
        private LayoutNode.FillType fillType = LayoutNode.FillType.Wrap;

        private Vector2 m_LayoutNodePos;
        private Rect m_layoutNodeViewRect;
        private float m_LayoutNodeHeight;
        private Texture m_bg;
        private string path ;

        public bool on;
        private bool m_On;
        LayoutNode[] layoutNodes;
        private void Init()
        {
            layoutNodes = GameObject.FindObjectsOfType<LayoutNode>();
            foreach (LayoutNode node in layoutNodes)
            {
                node.isShow = on;
            }
        }
        /// <summary>
        /// 创建节点区域
        /// </summary>
        void CreateLayoutNode()
        {
            GUILayout.Space(5);
            EditorGUILayout.LabelField("Create LayoutNode", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical("box");

            layoutName = EditorGUILayout.TextField("LayoutName", layoutName);
            isShow = EditorGUILayout.Toggle("isShow", !isShow);
            fillType = (LayoutNode.FillType)EditorGUILayout.EnumPopup("FillType", fillType);

            if (fillType == LayoutNode.FillType.FillParent)
                GUI.enabled = false;

            EditorGUILayout.BeginHorizontal();
            pos = EditorGUILayout.Vector2Field("Pos", pos);
            size = EditorGUILayout.Vector2Field("Size", size);
            rotation = EditorGUILayout.Vector3Field("Rotation", rotation);
            EditorGUILayout.EndHorizontal();
            GUI.enabled = true;
            GUILayout.Space(2);
            if (GUILayout.Button("Create LayoutNode"))
            {
                //EditorApplication.ExecuteMenuItem("PUI/Layout/LayoutNode");
                LayoutNode layoutNode = PUITool.CreateGameObject<LayoutNode>(layoutName);
                layoutNode.rectTransform = layoutNode.gameObject.GetComponent<RectTransform>();
                layoutNode.isShow = isShow;
                layoutNode.fillType = fillType;
                layoutNode.OnSetRectTransform(pos, size, rotation, fillType);
                layoutName = "LayoutNode";
            }
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// 获取节点
        /// </summary>
        void GetLayoutNode()
        {
            EditorGUILayout.LabelField("LayoutNode List", EditorStyles.boldLabel);
            Rect rect = position;
            rect.y = EditorGUIUtility.singleLineHeight * 10;
            rect.x = 5;
            rect.width = position.width - 10;
            rect.height = position.height - EditorGUIUtility.singleLineHeight * 10 - 5;
            GUI.Box(rect, "");

            Rect layoutRect = rect;
            layoutRect.width= rect.width / 2 - 180;
            layoutRect.height = rect.height;

            m_layoutNodeViewRect.x = layoutRect.x;
            m_layoutNodeViewRect.y = layoutRect.y;
            m_layoutNodeViewRect.width = layoutRect.width;
            if (m_LayoutNodeHeight < layoutRect.height)
                m_LayoutNodeHeight = layoutRect.height;
            m_layoutNodeViewRect.height = m_LayoutNodeHeight;
            if (m_bg!= null)
            {
                Rect temp = m_layoutNodeViewRect;
                temp.width = temp.x - 2;
                temp.height = temp.y - 2;
                GUI.DrawTexture(m_layoutNodeViewRect, m_bg, ScaleMode.StretchToFill);
            }
            m_LayoutNodePos = GUI.BeginScrollView(layoutRect, m_LayoutNodePos, m_layoutNodeViewRect,true,true);
            for (int i = 0; i < 5; i++)
            {
                Rect temp = m_layoutNodeViewRect;
                temp.y += i * 18;
                GUI.Label(temp,i+"----");
            }
            GUI.EndScrollView();
            SettingLayoutNode(layoutRect);
        }

        void SettingLayoutNode(Rect rect)
        {
            Rect temp = new Rect(rect.x+rect.width+5,rect.y,position.width-rect.width,rect.height);
            GUI.Label(temp,"LayoutNode Setting",EditorStyles.boldLabel);
            temp.x += 140;
            on = GUI.Toggle(temp,on,"isShow");
            if (m_On != on)
            {
                Init();
                m_On = on;
                EditorPrefs.SetBool("toggle",on);
            } 
        }
        private void OnEnable()
        {
            instance = this;
            on = EditorPrefs.GetBool("toggle",true);
            m_On = on;
            path = "Assets/PGUI/Texture/Icon/bg_01.png";
            m_bg = GameObject.Instantiate(AssetDatabase.LoadAssetAtPath<Texture>(path));
            Init();
            EditorApplication.hierarchyWindowChanged += Init;
        }
        private void OnGUI()
        {
            CreateLayoutNode();
            GetLayoutNode();
        }
    }
}
