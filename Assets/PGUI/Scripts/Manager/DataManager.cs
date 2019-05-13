using System;
using System.Collections.Generic;
using UnityEngine;
public class DataManager : Singleton<DataManager>
{
    private string[] keys;
    private static Dictionary<string, Dictionary<string, string>> datas = new Dictionary<string, Dictionary<string, string>>();
    public DataManager()
    {
        TextAsset textAsset = ResManager.instance.GetText("Component_viewConfig");
        string allString = textAsset.text;
        ParseData(allString.TrimEnd('\n'));
    }

    public string GetDataByIDAndName(string id, string key)
    {
        return datas[id][key];
    }
    private void ParseData(string dataStr)
    {
        string[] lines = dataStr.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i].TrimEnd('\n');
            if (i == 0)
            {
                keys = line.Split(',');
            }
            else
            {
                string[] values = line.Split(',');
                datas[values[0]] = new Dictionary<string, string>();
                for (int m = 0; m < keys.Length; m++)
                {
                    datas[values[0]][keys[m].Trim()] = values[m].Trim();
                }
            }
        }
    }
}
