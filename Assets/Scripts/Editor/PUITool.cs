using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Pico.PUI
{
    public class PUITool
    {
        private const string kUILayerName = "UI";

        public static Canvas FindCanvas()
        {
            Canvas[] canvases = GameObject.FindObjectsOfType<Canvas>();
            CreateEventSystem();
            if (canvases == null || canvases.Length == 0)
            {
                GameObject root = new GameObject("Canvas");
                root.layer = LayerMask.NameToLayer(kUILayerName);
                Canvas canvas = root.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.WorldSpace;
                canvas.gameObject.GetComponent<RectTransform>().localScale = new Vector3(0.001f, 0.001f, 0.001f);
                canvas.gameObject.GetComponent<RectTransform>().position = new Vector3(0, 0, 3.5f);
                canvas.sortingLayerName = "UI";
                root.AddComponent<CanvasScaler>();
                root.AddComponent<GraphicRaycaster>();
                Undo.RegisterCreatedObjectUndo(root, "Create " + root.name);
                return canvas;
            }
            else
            {
                return canvases[canvases.Length-1];
            }
        }
        private static void CreateEventSystem()
        {
            var eyes = GameObject.FindObjectOfType<EventSystem>();
            if (eyes == null)
            {
                var go = new GameObject("EventSystem");
                go.transform.position = Vector3.zero;
                go.transform.rotation = Quaternion.identity;
                go.transform.localScale = Vector3.one;
                go.AddComponent<EventSystem>();
                go.AddComponent<StandaloneInputModule>();
                Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
            }
        }

        /// <summary>
        /// 在Canvas下创建一个带有指定组件的对象
        /// </summary>
        /// <typeparam name="T">添加指定组件</typeparam>
        /// <param name="name">游戏对象名称</param>
        /// <returns></returns>
        public static T CreateGameObject<T>(string name) where T:UnityEngine.Component
        {
            T t=default(T);
            GameObject activeGo = Selection.activeGameObject;
            Transform parent = null;
            if (activeGo != null)
            {
                Transform temp = activeGo.transform;
                while (temp != null)
                {
                    if (temp.GetComponent<Canvas>() != null)
                    {
                        parent = activeGo.transform;
                        break;
                    }
                    temp = temp.transform.parent;
                }
                if (parent == null)
                {
                    Debug.LogError("the selected obj did not hava the canvas component");
                    return t;
                }
            }
            else
            {
                Canvas canvas = PUITool.FindCanvas();
                parent = canvas.transform;
            }

            GameObject go = new GameObject(name);
            t = go.AddComponent<T>();
            go.transform.parent = parent;
            go.transform.localPosition = Vector3.zero;
            go.transform.rotation = Quaternion.identity;
            go.transform.localScale = Vector3.one;

            Undo.RegisterCreatedObjectUndo(go, "Create Label");
            Selection.activeGameObject = go;

            return t;
        }
    }
}

