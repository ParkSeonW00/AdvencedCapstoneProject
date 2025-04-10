using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tiger : MonoBehaviour
{
    // �ʿ� ���� (ȣ���� �ӵ�, ������ ����, 
    private Vector3 spawnPos;
    public float tigerSpeed;

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
    }

    // Update is called once per frame
    void Update()
    {
        // �÷��̾� ��ǥ�� ���� ���� �ӵ��� �޷�����. -> �÷��̾�� �������� ������ ��������� ���� ����
        // ������ ������ �� (����)

        // ���� �޾��� �� (����)

        // �̵� �׽�Ʈ
        TigerMove();
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

    }
    // ���� ��ȭ �Լ�(����, ����, ����)
    public void TigerStateChange()
    {

    }
    // 

    private void OnCollisionEnter(Collision collision)
    {
        //�÷��̾ ���� ���� �ȿ� ������ ��
        if(collision.gameObject.tag == playerScan.tag)
        {
            transform.position = collision.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //�÷��̾ ���� ���� �ȿ� ������ ��
        if (other.gameObject.tag == playerScan.tag)
        {
            transform.position = other.transform.position;
        }
    }
}
