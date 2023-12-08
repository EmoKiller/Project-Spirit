using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPooling : MonoBehaviour
{
    
    public List<GunBullet> gunBulletsPool = new List<GunBullet>();
    public static UnityEvent<IPool> OnObjectPooled = new UnityEvent<IPool>();



    private void Awake()
    {
        GunBullet bullet = PopFromPool<GunBullet>("Bullet1", gunBulletsPool);
        bullet.Show();
        bullet.Hide();
    }

    public GunBullet PopGunBullet(string bulletName)
    {
        return PopFromPool(bulletName, gunBulletsPool);
    }


    public T PopFromPool<T>(string objectName, List<T> pool) where T : MonoBehaviour, IPool, new()
    {
        // Logic để lấy 1 vật thể từ pool ra

        T objectToPop = new T();

        OnObjectPooled?.Invoke(objectToPop);
        return objectToPop;
    } 
    
    public void PushToPool<T>(T objectToPush, List<T> pool) where T : MonoBehaviour, IPool, new()
    {

    }
}
