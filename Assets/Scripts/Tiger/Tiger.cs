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
    public static TState tigerState;
    [HideInInspector]
    public static Transform playerT;

    // �ӽ� ����
    public GameObject playerScan;
    
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
            case TState.Attack:
                Debug.Log("Attack");
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
}
