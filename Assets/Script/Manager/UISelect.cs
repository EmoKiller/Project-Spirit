using DG.Tweening;
using TMPro;
using UnityEngine;

public class UISelect : MonoBehaviour
{
    [SerializeField] RectTransform arrow;
    [SerializeField] TMP_Text yes;
    [SerializeField] TMP_Text absolutely;
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Rotation(new Vector3(0,0,89.9f));
            yes.color = new Color32(252,240,211,255);
            absolutely.color = Color.white;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Rotation(new Vector3(0, 0, -89.9f));
            yes.color = Color.white;
            absolutely.color = new Color32(252, 240, 211, 255);
        }
    }
    private void Rotation(Vector3 value)
    {
        arrow.DORotate(value,0.5f,RotateMode.Fast);
    }
}
