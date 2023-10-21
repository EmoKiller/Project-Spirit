using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smooth;
    private Vector3 vecref = Vector3.zero;
    //[SerializeField] private float maxX;
    //[SerializeField] private float minX;
    //[SerializeField] private float maxZ;
    //[SerializeField] private float minZ;
    


    private void Update()
    {
        //targetpos.x = Mathf.Clamp(targetpos.x, minX, maxX);
        //targetpos.z = Mathf.Clamp(targetpos.z, minZ, maxZ);
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref vecref, smooth);
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
        //targetpos = target.transform.position;
        //targetpos.x = Mathf.Clamp(targetpos.x, minX, maxX);
        //targetpos.z = Mathf.Clamp(targetpos.z, minZ, maxZ);
        //transform.position = Vector3.SmoothDamp(transform.position, targetpos + offset, ref vecref, smooth);
    }

}
