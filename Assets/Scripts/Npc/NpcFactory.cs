using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using UnityEngine;

public static class NpcFactory
{
    public static List<Type> NpcTypeList;

    public static XmlDocument xmlFile;
    static NpcFactory()
    {
        NpcTypeList=new List<Type>();
        xmlFile = new XmlDocument();

        string context = Resources.Load("NPCConfig").ToString();
 
        xmlFile.LoadXml(context);

    }

    public static T CreateNpc<T>() where T : Character, new()
    {


        T t = new T();
        string typeName = t.GetType().Name;
        var nodelist = xmlFile.SelectNodes("NpcFactory/Npc");
        XmlNode xn=null;
        foreach (XmlNode root in nodelist)
        {
            if (root.Attributes["category"].Value.Equals(typeName))
            {
                xn = root;
                break;
            }
        }
        

        if (xn == null)
        {
            Debug.LogError("没有找到xml类型" + typeName);
            return null;
        }

        FieldInfo[] fields = t.GetType().BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance );
        for (int i = 0; i < fields.Length; i++)
        {
            string fieldName = fields[i].Name;

            foreach (XmlNode itemNode in xn)
            {
                if (itemNode.Name.Equals(fieldName))
                { 
            
                    if (fields[i].FieldType==(typeof(float)))
                    {
                        fields[i].SetValue(t, Convert.ToSingle(itemNode.InnerText));
                        Debug.Log(string.Format("{0}字段赋值{1}", itemNode.Name, (itemNode.InnerText)));
                        break;
                    }

                    if (fields[i].FieldType == (typeof(int)))
                    {
                        fields[i].SetValue(t, Convert.ToInt32(itemNode.InnerText));
                        Debug.Log(string.Format("{0}字段赋值{1}", itemNode.Name, (itemNode.InnerText)));
                        break;
                    }

                    if (fields[i].FieldType == (typeof(string)))
                    {
                        fields[i].SetValue(t, Convert.ToString(itemNode.InnerText));
                        Debug.Log(string.Format("{0}字段赋值{1}", itemNode.Name, (itemNode.InnerText)));
                        break;
                    }

                }
            }
            
        }



        return t;
    }
}
