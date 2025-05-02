using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tiger : MonoBehaviour
{
    // �ʿ� ���� (ȣ���� �ӵ�, ������ ����, 
    private Vector3 spawnPos;
    public float tigerSpeed;
    public float tigerAttackSpeed;
    private TState tigerState;
    private Transform playerT;

    // �ӽ� ����
    public GameObject playerScan;
    
    enum TState
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
        tigerState = TState.Idle;
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
                TigerMove();
                break;
            case TState.Attack:
                ScanPlayer();
                break;
        }
        // �̵� �׽�Ʈ
        
    }

    // ������ �Լ�
    private void TigerMove()
    {
        // �ȱ� �ִϸ��̼� ���

        //�ȱ� �ڵ�
        float speed = tigerSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * speed);
        
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
    // ���� ��ȭ �Լ�(����, ����, ����)
    public void TigerStateChange()
    {

    }
    // 
    
    private void OnTriggerEnter(Collider other)
    {
        tigerState = TState.Attack;
        //�÷��̾ ���� ���� �ȿ� ������ ��
        if (other.gameObject.tag == playerScan.tag)
        {
            playerT = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        tigerState = TState.Idle;
    }
}
