using UnityEngine;

public class CannonBullet : MonoBehaviour, IPool
{
    public string objectName => gameObject.name;

    public void Hide()
    {
        // chạy vfx explosion
        // Đưa vật thể này vào pool tương ứng
    }

    public void Show()
    {
        // logic show viên đạn của cannon
    }

}
