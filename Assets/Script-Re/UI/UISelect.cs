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
        Left();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Left();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Right();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            EventDispatcher.Publish(IntroGame.Script.IntroGame, Events.PlayTalkScript3);
            Destroy(gameObject);
        }
    }
    private void Left()
    {
        Rotation(new Vector3(0, 0, 89.9f));
        yes.color = new Color32(252, 240, 211, 255);
        absolutely.color = Color.white;
    }
    private void Right()
    {
        Rotation(new Vector3(0, 0, -89.9f));
        yes.color = Color.white;
        absolutely.color = new Color32(252, 240, 211, 255);
    }
    private void Rotation(Vector3 value)
    {
        arrow.DORotate(value,0.2f,RotateMode.Fast);
    }
}
