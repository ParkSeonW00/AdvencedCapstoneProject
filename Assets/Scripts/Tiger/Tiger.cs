using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    [HideInInspector]
    public Transform playerT;

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
                ScanPlayer();
                TigerMove();
                break;
            case TState.Run:
                Debug.Log("Run");
                ScanPlayer();
                TigerRun();
                break;
            case TState.Attack:
                Debug.Log("Attack");
                ScanPlayer();
                TigerAttack();
                break;
        }
        // �̵� �׽�Ʈ
        
    }
    // ���� ��ȭ �Լ�(����, ����, ����)
    public void TigerStateChange(TState state)
    {
    }
    // ������ �Լ�
    private void TigerMove()
    {
        // �ȱ� �ִϸ��̼� ���

        //�ȱ� �ڵ�
        float speed = tigerWalkSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * speed);
        animator.SetBool("ScanTigerL", false);
        animator.SetBool("ScanTigerS", true);

    }
    private void TigerRun()
    {
        
        //�ٱ� �ڵ�
        float speed = tigerRunSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * speed);
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
        Vector3 dir = playerT.position - transform.position;
        dir.y = 0f;
        Quaternion rot = Quaternion.LookRotation(dir.normalized);
        transform.rotation = rot;

        // �÷��̾�� �޷����� ���⸸ �ٶ󺸰� �ִϸ��̼����� �޸���.

        //transform.position = Vector3.MoveTowards(transform.position, playerT.position, tigerAttackSpeed * Time.deltaTime);
    }
    
}
