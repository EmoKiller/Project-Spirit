using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Player target => GameManager.Instance.player ;
    //[SerializeField] private PlayerControllerPC player;
    [SerializeField] private Vector3 offset = new Vector3(0, 9.7f, -13.3f);
    Vector3 targetpos;
    [SerializeField] private float maxX;
    [SerializeField] private float minX;
    [SerializeField] private float maxZ;
    [SerializeField] private float minZ;

    public float smooth;

    Transform max1;

    private Vector3 vecref = Vector3.zero;
    
    private void Awake()
    {
        
        transform.eulerAngles = new Vector3(37, 0, 0);
    }
    private void FixedUpdate()
    {

        //if (player.isRolling || player.isJump)
        //{
        //    smooth += Time.deltaTime * 0.4f;
        //}
        //else
        //{
        //    smooth -= Time.deltaTime * 0.16f;
        //    if (smooth <= 0)
        //    {
        //        smooth = 0;
        //    }
        //}
        targetpos = target.transform.position;
        targetpos.x = Mathf.Clamp(targetpos.x, minX, maxX);
        targetpos.z = Mathf.Clamp(targetpos.z, minZ, maxZ);
        transform.position = Vector3.SmoothDamp(transform.position, targetpos + offset, ref vecref, smooth);
    }
}
