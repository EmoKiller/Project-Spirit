using DG.Tweening;
using UnityEngine;

public class DetectedEnemy : MonoBehaviour
{
    private Enemy enemyTranform;
    Vector3 dir => enemyTranform.transform.position - transform.position;
    private float SpeedMove = 7;
    private float SpeedRotation = 2;
    [SerializeField] private bool startDetec = false;
    [SerializeField] private bool startMove = false;
    private Vector3 dirStart;
    public bool StartDetec
    {
        get { return startDetec; }
        set { startDetec = value; }
    }
    
    private void Update()
    {
        if (!startMove)
            return;
        if (!StartDetec)
        {
            transform.position += new Vector3(dirStart.x, 0, dirStart.z) * Time.deltaTime * SpeedMove;
            transform.rotation = Quaternion.LookRotation(new Vector3(dirStart.x,0, dirStart.z) * Time.deltaTime * SpeedRotation);
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
            {
                dirStart = (enemyTranform.transform.position - transform.position).normalized;
                StartDetec = true;
            }
        }
    }
    public void DirStarts(Vector3 dir)
    {
        dirStart = dir;
        transform.DOMove(transform.position + dirStart + new Vector3(0,1,0), 0.3f).OnComplete(() =>
        {
            startMove = true;
        });
    }
    private void OnEnable()
    {
        StartDetec = false;
        startMove = false;
    }
    private void OnDisable()
    {
        enemyTranform = null;
    }
}
