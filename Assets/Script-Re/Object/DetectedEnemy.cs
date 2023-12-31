using DG.Tweening;
using UnityEngine;

public class DetectedEnemy : MonoBehaviour
{
    private Enemy enemyTranform;
    Vector3 dir => enemyTranform.transform.position - transform.position;
    private float SpeedMove = 7;
    private float SpeedRotation = 2;
    private bool startDetec = false;
    private bool startMove = false;
    private Vector3 dirStart;
    public bool StartDetec
    {
        get { return startDetec; }
        set { startDetec = value; }
    }
    private void Start()
    {
        startDetec = false;
        startMove = false;
        transform.DOMove(transform.position + (dirStart * 3), 0.3f).OnComplete(() =>
        {
            startMove = true;
        });
    }
    private void Update()
    {
        if (!startMove)
            return;
        if (!StartDetec)
        {
            transform.position += dirStart * Time.deltaTime * SpeedMove;
            return;
        }
        transform.position = Vector3.MoveTowards(transform.position, enemyTranform.transform.position, SpeedMove * Time.deltaTime);
        transform.rotation = Quaternion.LookRotation(dir * Time.deltaTime * SpeedRotation);
        if (!enemyTranform.Alive && enemyTranform != null)
        {
            enemyTranform = null;
            StartDetec = false;
        }
    }
    private void LateUpdate()
    {
        if (!startMove)
            return;
        if (enemyTranform == null)
        {
            enemyTranform = GameLevelManager.Instance.listEnemys.Find(e => Vector3.Distance(e.transform.position, transform.position) < 10f);
            if (enemyTranform != null)
                StartDetec = true;
        }
    }
    public void DirStarts(Vector3 dir)
    {
        dirStart = dir;
    }
    private void OnDisable()
    {
        StartDetec = false;
        startMove = false;
    }
}
