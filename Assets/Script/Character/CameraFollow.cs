using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smooth;
    public Vector3 offset;
    private Vector3 vecref = Vector3.zero;
    
    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref vecref, smooth);
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
        //targetpos = target.transform.position;
        //targetpos.x = Mathf.Clamp(targetpos.x, minX, maxX);
        //targetpos.z = Mathf.Clamp(targetpos.z, minZ, maxZ);
        //transform.position = Vector3.SmoothDamp(transform.position, targetpos + offset, ref vecref, smooth);
    }


}
