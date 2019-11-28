using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInject : MonoBehaviour
{

    public TestComponet AComponet;
    // Start is called before the first frame update
    void Start()
    {
   
        AComponet =new TestComponet();
        if (AComponet.componet == null)
        {
            Debug.LogError("没有注入成功");
        }
        else
        {
            Debug.Log("注入成功");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

    /// <summary>
    /// 组件对象自动注入标签
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class InjectAttribute : Attribute
    {
        /// <summary>
        /// 组件标签
        /// </summary>
        public string ComponentTag { get; set; }

        public InjectAttribute(string componentTag = null)
        {
            ComponentTag = componentTag;
        }
    }


public class TestComponet
{
    [Inject]
    public AAA componet;


}

public class AAA
{
    private string _componentTag;
    public string ComponentTag
    {
        get => _componentTag;
        set => _componentTag = value;
    }

    public AAA()
    {
        
    }
}
