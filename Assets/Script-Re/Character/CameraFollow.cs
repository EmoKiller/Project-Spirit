using UnityEngine;
using UnityEngine.Video;

public class CameraFollow : MonoBehaviour
{
    public enum Eventss
    {
        TargetPlayer,
        ChangeTarget,
        ChangeColorBackGround,
        ReturnTargetPlayer,
        CameraDefault,
        CameraFocus
    }
    [SerializeField] private Transform target;
    [SerializeField] private Transform player;
    [SerializeField] private float smooth;
    [SerializeField] private Vector3 offset;
    
    Camera _camera => GetComponent<Camera>();
    private Vector3 vecref = Vector3.zero;
    private void Awake()
    {
        
        //EventDispatcher.Addlistener<Transform>(ListScript.CameraFollow, Eventss.TargetPlayer, TargetPlayer);
        //EventDispatcher.Addlistener<Transform>(ListScript.CameraFollow, Eventss.ChangeTarget, ChangeTarget);
        EventDispatcher.Addlistener<Color32>(ListScript.CameraFollow, Eventss.ChangeColorBackGround, ChangeColorBackGround);
        
        EventDispatcher.Addlistener(ListScript.CameraFollow, Eventss.ReturnTargetPlayer, ReturnTargetPlayer);
        EventDispatcher.Addlistener(ListScript.CameraFollow,Eventss.CameraDefault, CameraDefault);
        EventDispatcher.Addlistener(ListScript.CameraFollow, Eventss.CameraFocus, CameraFocus);
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            target = (Transform)EventDispatcher.Call(ListScript.CameraFollow, Events.test22);
        }
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref vecref, smooth);
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
    private void CameraDefault()
    {
        offset = new Vector3(0,22,-28);
        transform.eulerAngles = new Vector3(37,0,0);
        SetSmooth(0.4f);
    }
    private void CameraFocus()
    {
        SetSmooth(0.6f);
        offset = new Vector3(0, 6, -8.5f);
        transform.eulerAngles = new Vector3(30, 0, 0);
    }
    private void ReturnTargetPlayer()
    {
        target = player;
    }
    private void TargetPlayer(Transform target)
    {
        player = target;
    }
    private void ChangeTarget(Transform target)
    {
        this.target = target;
    }
    private void ChangeColorBackGround(Color32 color)
    {
        _camera.backgroundColor = color;
    }
    private void SetSmooth(float value)
    {
        smooth = value;
    }

}
