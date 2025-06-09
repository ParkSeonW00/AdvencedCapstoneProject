using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class EndingTiger : MonoBehaviour
{
    // �ʿ� ���� (ȣ���� �ӵ�, ������ ����, 
    private Vector3 spawnPos;

    [Header("TigerStateInfo")]
    public float tigerWalkSpeed;
    public float tigerRunSpeed;
    public float tigerAttackSpeed;
    public float attackDistance = 2f;
    public TState tigerState;

    private Animator animator;
    [SerializeField] private AnimatorController changeAni;

    private NavMeshAgent meshAgent;

    [HideInInspector]
    public Transform playerT;

    // ȣ���� �ڵ� ������ ����
    private float updateInterval = 10f;
    private float timeSinceLastUpdate;

    // ���� ��ĵ üũ ����
    public int checkRange = 2;
    private int updateCheckRange = 0;

    public enum TState
    {
        Idle,
        Run,
        Attack,
        Eat,
        RunBack
    }

    // Start is called before the first frame update
    void Start()
    {
        
        
        tigerState = TState.Run;
        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
       
    }

    // Update is called once per frame
    void Update()
    {

        // �÷��̾� ��ǥ�� ���� ���� �ӵ��� �޷�����. -> �÷��̾�� �������� ������ ��������� ���� ����
        // ������ ������ �� (����)

        // ���� �޾��� �� (����)
        float distance = Vector3.Distance(transform.position, playerT.position);

        // �ڵ� ���� ��ȯ (Ȥ�� �÷��̾ �ʹ� ��������µ� ���� ������ �� �Ǿ��� �� ���)
       
        switch (tigerState)
        {
            case TState.Idle:
                Debug.Log("Idle");
                //GetRandomMove();
                TigerMove();
                break;
            case TState.Run:
                Debug.Log("Run");
              
                TigerRun();
                break;
            case TState.Attack:
                Debug.Log("Attack");
                //ScanPlayer();
                TigerAttack();
                break;
        }
        // �̵� �׽�Ʈ

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "gotgam")
        {
            Debug.Log("������ �¾Ҵ�!");
            gameObject.GetComponent<Animator>().runtimeAnimatorController = changeAni;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "dduk")
        {
            Debug.Log("���̴�!");
        }
    }

    /*public void OnCheckScanRange()
    {
        var player = GameMgr.Instance.PlayerInit();
        if (updateCheckRange != checkRange)
        {
            switch (checkRange)
            {
                case 0:
                    break;
                case 1:
                    player.SetUpBoundaryLevel();
                    break;
                case 2:
                    player.SetDownBoundaryLevel();
                    break;
            }

            //Debug.Log("CheckRange : " + checkRange);
            updateCheckRange = checkRange;
        }


    }*/

    // 3�ʸ��� ���ο� ���� ��ġ�� ã�� �̵��ϴ� �Լ�
    /*public void GetRandomMove()
    {
        timeSinceLastUpdate += Time.deltaTime;
        OnCheckScanRange();
        if (timeSinceLastUpdate >= updateInterval)
        {
            if (GameMgr.Instance.PlayerInit().GetBoundaryLevel() <= 0)
            {
                Vector3 randPos = GetRandomPosition();
                meshAgent.SetDestination(randPos);
                timeSinceLastUpdate = 0f;
            }
            else
            {
                meshAgent.SetDestination(playerT.position);
                timeSinceLastUpdate = 0f;
            }
        }
    }
*/
    public Vector3 GetRandomPosition()
    {
        Vector3 randDirect = Random.insideUnitSphere * 20f;
        randDirect += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randDirect, out hit, 20f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return transform.position;
        }

    }

    // ���� ��ȭ �Լ�(����, ����, ����)
    public void TigerStateChanger()
    {
        int level = GameMgr.Instance.PlayerInit().GetBoundaryLevel();
        //Debug.Log("BoundaryLevel = " + level);
        switch (level)
        {
            case 0:
                tigerState = TState.Idle;
                break;
            case 1:
                tigerState = TState.Idle;
                break;
            case 2:
                tigerState = TState.Run;
                break;
            case 3:
                tigerState = TState.Attack;
                break;
            case 4:
                tigerState = TState.RunBack;
                break;

        }
        //GetRandomMove();
    }
    // ������ �Լ�
    private void TigerMove()
    {
        // �ȱ� �ִϸ��̼� ���

        //�ȱ� �ڵ�
        //float speed = tigerWalkSpeed * Time.deltaTime;
        //transform.Translate(Vector3.forward * speed);

        meshAgent.speed = tigerWalkSpeed;
        animator.SetBool("ScanTigerL", false);
        animator.SetBool("ScanTigerS", true);

    }
    private void TigerRun()
    {

        //�ٱ� �ڵ�
        //float speed = tigerRunSpeed * Time.deltaTime;
        //transform.Translate(Vector3.forward * speed);
        meshAgent.isStopped = false;
        meshAgent.SetDestination(playerT.position);
        meshAgent.speed = tigerRunSpeed;
        animator.SetBool("ScanTigerS", false);
        animator.SetBool("ScanTigerL", true);

    }

    private void TigerAttack()
    {
        meshAgent.velocity = Vector3.zero;
        animator.SetBool("ScanTigerS", false);
        animator.SetBool("AttackTiger", true);
    }
}
