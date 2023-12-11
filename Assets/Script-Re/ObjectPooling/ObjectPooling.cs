using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectPooling : MonoBehaviour
{
    
    public List<Enemy> ListEnemy = new List<Enemy>();
    public static UnityEvent<IPool> OnObjectPooled = new UnityEvent<IPool>();



    private void Awake()
    {
        //GunBullet bullet = PopFromPool<GunBullet>("Bullet1", ListEnemy);
        //bullet.Show();
        //bullet.Hide();
    }

    public Enemy PopEnemy(string EnemyName)
    {
        return PopFromPool(EnemyName, ListEnemy);
    }


    public T PopFromPool<T>(string objectName, List<T> pool) where T : MonoBehaviour, IPool, new()
    {
        // Logic để lấy 1 vật thể từ pool ra
        T obj = pool.Find(e => e.name.Equals(objectName));
        if (obj == null)
        {
            Instantiate(obj);
            return obj;
        }
        return obj;
    }

    public void PushToPool<T>(T objectToPush, List<T> pool) where T : MonoBehaviour, IPool, new()
    {

    }
}
