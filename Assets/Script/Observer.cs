using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IOserver
{
    public void OnNotify();
}
public class Observer : IOserver
{
    public void OnNotify()
    {
        
    }

}
