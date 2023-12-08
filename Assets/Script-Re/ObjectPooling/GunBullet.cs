using UnityEngine;

public class GunBullet : MonoBehaviour, IPool
{
    public string objectName => gameObject.name;

    public void Hide()
    {
        // Push vật thể này vào pooling tương ứng
    }

    public void Show()
    {
        // Nổ vfx muzzle sau đó viên đạn bay luôn

    }
}
