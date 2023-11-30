using UnityEngine;
using UnityEngine.Video;

public class CameraFollow : MonoBehaviour
{
    public enum Script
    {
        CameraFollow
    }
    [SerializeField] private Transform target;
    [SerializeField] private float smooth;
    [SerializeField] private Vector3 offset;
    
    Camera _camera => GetComponent<Camera>();
    private Vector3 vecref = Vector3.zero;
    private void Awake()
    {
        EventDispatcher.Addlistener<Color32>(Script.CameraFollow, Events.CameraChangeColorBackGround, ChangeColorBackGround);
        EventDispatcher.Addlistener<Transform>(Script.CameraFollow, Events.CameraChangeTarget, CameraChangeTarget);
        EventDispatcher.Addlistener(Script.CameraFollow, Events.CameraTargetPlayer, TargetPlayer);
        EventDispatcher.Addlistener(Script.CameraFollow, Events.CameraDefault, CameraDefault);
        EventDispatcher.Addlistener(Script.CameraFollow, Events.CameraFocus, CameraFocus);
    }
    private void Start()
    {
        TargetPlayer();
    }
    private void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref vecref, smooth);
    }
    private void CameraDefault()
    {
        offset = new Vector3(0,22,-28);
        transform.eulerAngles = new Vector3(38,0,0);
        SetSmooth(0.4f);
    }
    private void CameraFocus()
    {
        SetSmooth(0.6f);
        offset = new Vector3(0, 6, -8.5f);
        transform.eulerAngles = new Vector3(30, 0, 0);
    }
    private void CameraChangeTarget(Transform target)
    {
        this.target = target;
    }
    private void TargetPlayer()
    {
        target = (Transform)EventDispatcher.Call(Player.Script.Player, Events.PlayerDirection);
    }
    private void ChangeColorBackGround(Color32 color)
    {
        _camera.backgroundColor = color;
    }
    private void SetSmooth(float value)
    {
        smooth = value;
    }
    private void FixedUpdate()
    {
        //transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref vecref, smooth);
        //if (player.isRolling || player.isJump)
        //{
        //    smooth += Time.deltaTime * 0.4f;
        //}
        //else    Rotation -    30
        //{       ofset 0,10,-15
        //    smooth -= Time.deltaTime * 0.16f;
        //    if (smooth <= 0)
        //    {
        //        smooth = 0;
        //    }
        //}new Color32(242, 236, 222, 255);
        //targetpos = target.transform.position;
        //targetpos.x = Mathf.Clamp(targetpos.x, minX, maxX);
        //targetpos.z = Mathf.Clamp(targetpos.z, minZ, maxZ);
        //transform.position = Vector3.SmoothDamp(transform.position, targetpos + offset, ref vecref, smooth);
    }

}
