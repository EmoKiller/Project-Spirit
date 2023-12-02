using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TestScript : SerializedMonoBehaviour
{
    //public UIButtonAction Gameobj;
    //public Dictionary<string, int> yeasdfdsaf;
    //[SerializeField, PreviewField(100)] private Sprite test;    
    private void Awake()
    {
        EventDispatcher.Addlistener(ListScripTs.TestScript,Events.DebugAction, Testss);
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            
        //}
    }
    private void Testss()
    {
        Debug.Log("ACtion");
    }
    //[Button("test")]
    //public void testdfasdfdsa(int a)
    //{
    //    Debug.Log("sdafasdfsadfds" + a);
    //}

}
