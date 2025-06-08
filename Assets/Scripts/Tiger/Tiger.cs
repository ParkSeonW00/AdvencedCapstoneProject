using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Tiger : MonoBehaviour
{
    // �ʿ� ���� (ȣ���� �ӵ�, ������ ����, 
    private Vector3 spawnPos;

    [Header("TigerStateInfo")]
    public float tigerWalkSpeed;
    public float tigerRunSpeed;
    public float tigerAttackSpeed;
    public TState tigerState;

    private Animator animator;
    [SerializeField] private AnimatorController changeAni;

    private NavMeshAgent meshAgent;

    public Transform playerT;

    // ȣ���� �ڵ� ������ ����
    private float updateInterval = 10f;
    private float timeSinceLastUpdate;

    // ���� ��ĵ üũ ����
    public float runTime = 5f;
    private float checkRunTime;

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
        // ù ������ �������� ������ ���� ��.(��ġ �صΰ� ���丮 ���� ����)
        playerT = transform;
        tigerState = TState.Idle;
        animator = GetComponent<Animator>();
        meshAgent = GetComponent<NavMeshAgent>();
        timeSinceLastUpdate = updateInterval;
    }

    // Update is called once per frame
    void Update()
    {

        // �÷��̾� ��ǥ�� ���� ���� �ӵ��� �޷�����. -> �÷��̾�� �������� ������ ��������� ���� ����
        // ������ ������ �� (����)

        // ���� �޾��� �� (����)
        TigerStateChanger();
        switch (tigerState)
        {
            case TState.Idle:
                //Debug.Log("Idle");
                GetRandomMove();
                TigerWalk();
                break;
            case TState.Run:
                //Debug.Log("Run");
                GetRandomMove();
                TigerRun();
                break;
            case TState.Attack:
                //Debug.Log("Attack");
                //ScanPlayer();
                TigerAttack();
                break;
            case TState.Eat:
                TigerEat();
                break;
            case TState.RunBack:
                TigerRunBack();
                break;
        }
        // �̵� �׽�Ʈ

    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "gotgam")
        {
            Debug.Log("������ �¾Ҵ�!");
            SoundManager.Instance.Play("SadTiger");
            gameObject.GetComponent<Animator>().runtimeAnimatorController = changeAni;
            tigerState = TState.RunBack;
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "dduk")
        {
            Debug.Log("���̴�!");
        }
    }


    //public void OnCheckScanRange()
    //{
    //    var player = GameMgr.Instance.PlayerInit();
    //    if (updateCheckRange != checkRange)
    //    {
    //        switch (checkRange)
    //        {
    //            case 0:
    //                break;
    //            case 1:
    //                player.SetUpBoundaryLevel();
    //                break;
    //            case 2:
    //                player.SetDownBoundaryLevel();
    //                break;
    //        }

    //        //Debug.Log("CheckRange : " + checkRange);
    //        updateCheckRange = checkRange;
    //    }


    //}

    // 3�ʸ��� ���ο� ���� ��ġ�� ã�� �̵��ϴ� �Լ�
    public void GetRandomMove()
    {
        timeSinceLastUpdate += Time.deltaTime;
        // OnCheckScanRange();
        if (timeSinceLastUpdate >= updateInterval)
        {
            if (VRPlayer.instance.nowState == VRPlayer.PlayerState.HIDE)
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
    //public void TigerStateChanger()
    //{
    //    Debug.Log("���º�ȭ");
    //    int level = GameMgr.Instance.PlayerInit().GetBoundaryLevel();
    //    //Debug.Log("BoundaryLevel = " + level);
    //    switch (level)
    //    {
    //        case 0:
    //            tigerState = TState.Idle;
    //            break;
    //        case 1:
    //            tigerState = TState.Idle;
    //            break;
    //        case 2:
    //            tigerState = TState.Run;
    //            break;
    //        case 3:
    //            tigerState = TState.Attack;
    //            break;
    //        case 4:
    //            tigerState = TState.RunBack;
    //            break;

    //    }
    //    GetRandomMove();
    //}
    public void TigerStateChanger()
    {
        if (VRPlayer.instance.nowState != VRPlayer.instance.changeState)
            //Debug.Log("BoundaryLevel = " + level);
            switch (VRPlayer.instance.nowState)
            {
                case VRPlayer.PlayerState.WALK:
                    tigerState = TState.Idle;
                    break;
                case VRPlayer.PlayerState.RUN:
                    tigerState = TState.Run;
                    break;
                case VRPlayer.PlayerState.JUMP:
                    tigerState = TState.Run;
                    break;
                case VRPlayer.PlayerState.HIDE:
                    tigerState = TState.Idle;
                    break;
            }
        VRPlayer.instance.nowState = VRPlayer.instance.changeState;
    }
    // ������ �Լ�
    private void TigerWalk()
    {
        // �ȱ� �ִϸ��̼� ���

        //�ȱ� �ڵ�
        //float speed = tigerWalkSpeed * Time.deltaTime;
        //transform.Translate(Vector3.forward * speed);
        if (VRPlayer.instance.transform == null) 
            Debug.Log("����?");
        playerT = VRPlayer.instance.transform;
        meshAgent.speed = tigerWalkSpeed;
        animator.SetBool("IsWalk", true);
        animator.SetBool("IsRun", false);
        animator.SetBool("AttackTiger", false);
        animator.SetBool("IsEating", false);
        animator.SetBool("IsRunBack", false);

    }
    private void TigerRun()
    {

        //�ٱ� �ڵ�
        //float speed = tigerRunSpeed * Time.deltaTime;
        //transform.Translate(Vector3.forward * speed);
        meshAgent.speed = tigerRunSpeed;
        animator.SetBool("IsRun", true);
        animator.SetBool("IsWalk", false);
        animator.SetBool("AttackTiger", false);
        animator.SetBool("IsEating", false);
        animator.SetBool("IsRunBack", false);

    }

    private void TigerAttack()
    {
        meshAgent.velocity = Vector3.zero;
        animator.SetBool("AttackTiger", true);
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsRun", false);
        animator.SetBool("IsEating", false);
        animator.SetBool("IsRunBack", false);
    }

    private void TigerEat()
    {
        meshAgent.velocity = Vector3.zero;
        animator.SetBool("IsEating", true);
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsRun", false);
        animator.SetBool("AttackTiger", false);
        animator.SetBool("IsRunBack", false);
    }

    private void TigerRunBack()
    {
        animator.SetBool("IsRunBack", true);
        animator.SetBool("IsWalk", false);
        animator.SetBool("IsRun", false);
        animator.SetBool("AttackTiger", false);
        animator.SetBool("IsEating", false);
        TigerTurnBack();
    }

    private void TigerTurnBack()
    {
        checkRunTime += Time.deltaTime;
        if(checkRunTime >= runTime)
        {
            checkRunTime = 0;
            animator.SetBool("IsWalk", true);
        }
        else if(checkRunTime < runTime)
        {
            meshAgent.speed = tigerRunSpeed;
            meshAgent.SetDestination(-playerT.position);
        }
        animator.SetBool("IsTurnBack", true);
    }

    // �÷��̾� ���� �Լ�(��� ����)
    private void ScanPlayer()
    {
        // �浹 ���� -> �±� �÷��̾�
        // if((transform.position)
        // �÷��̾� ���� �ٶ󺸱�

        // ��ġ �̵�
        //vector3 dir = playert.position - transform.position;
        //dir.y = 0f;
        //quaternion rot = quaternion.lookrotation(dir.normalized);
        //transform.rotation = rot;

        // navMesh ��� �̵�
        meshAgent.SetDestination(playerT.position);

        // �÷��̾�� �޷����� ���⸸ �ٶ󺸰� �ִϸ��̼����� �޸���.

        //transform.position = Vector3.MoveTowards(transform.position, playerT.position, tigerAttackSpeed * Time.deltaTime);
    }

}
