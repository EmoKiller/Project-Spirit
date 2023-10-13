using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject 
{
    

    public void AddObserver(IOserver observer)
    {
        
    }
    public void RemoveObserver(IOserver observer)
    {
        
    }
    protected void NotifyObserver()
    {
        //observers.ForEach((observer) =>
        //{
        //    observer.OnNotify();
        //});
    }
}
