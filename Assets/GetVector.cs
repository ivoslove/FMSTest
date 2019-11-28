using UnityEngine;
using System.Collections;
using UnityEditor;

public class GetVector : MonoBehaviour
{

    [ContextMenu("GetLocalUp")]
    public void GetLocalUp()
    {
        Debug.Log(name + "的模型空间y轴向量为： " + transform.up.ToString("0.00"));
    }
}
