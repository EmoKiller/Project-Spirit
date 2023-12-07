using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using System;

public class TestScript : SerializedMonoBehaviour
{
    public UnityEvent events;
    public Action<string> action;
    //public UIButtonAction Gameobj;
    //public Dictionary<string, int> yeasdfdsaf;
    //[SerializeField, PreviewField(100)] private Sprite test;
    private void Awake()
    {
        events.AddListener(test);
    }
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            string abc = "testsssssss";
            action?.Invoke(abc);
        }
    }
    private void test()
    {

    }
    //[Button("test")]
    //public void testdfasdfdsa(int a)
    //{
    //    Debug.Log("sdafasdfsadfds" + a);
    //}

}
