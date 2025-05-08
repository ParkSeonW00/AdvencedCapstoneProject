using System.Collections;
using System.Collections.Generic;
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

    private NavMeshAgent meshAgent;

    [HideInInspector]
    public Transform playerT;

    // ȣ���� �ڵ� ������ ����
    public float updateInterval = 3f;
    private float timeSinceLastUpdate;

    public enum TState
    {
        Idle,
        Run,
        Attack,
        Eat,
        Die
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

        switch (tigerState)
        {
            case TState.Idle:
                Debug.Log("Idle");
                GetRandomMove();
                TigerMove();
                break;
            case TState.Run:
                Debug.Log("Run");
                GetRandomMove();
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
    // 3�ʸ��� ���ο� ���� ��ġ�� ã�� �̵��ϴ� �Լ�
    public void GetRandomMove()
    {
        timeSinceLastUpdate += Time.deltaTime;

        if (timeSinceLastUpdate >= updateInterval)
        {
            if(GameMgr.Instance.PlayerInit().GetBoundaryLevel() == 0)
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
        if(NavMesh.SamplePosition(randDirect, out hit, 20f, NavMesh.AllAreas))
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
        }
        GetRandomMove();
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
        meshAgent.speed = tigerRunSpeed;
        animator.SetBool("ScanTigerS", false);
        animator.SetBool("ScanTigerL", true);

    }

    private void TigerAttack()
    {
        animator.SetBool("ScanTigerS", false);
        animator.SetBool("AttackTiger", true);
    }

    // �÷��̾� ���� �Լ�
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
