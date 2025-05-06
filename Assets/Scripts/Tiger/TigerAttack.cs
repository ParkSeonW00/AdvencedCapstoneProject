using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerAttack : MonoBehaviour
{
    public Tiger GetTiger;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�÷��̾� ����");
        if (other.CompareTag("Player"))
        {
            // ���ο���
            // GameMgr.Instance.SlowMotion(0.5f);
            GetTiger.tigerState = Tiger.TState.Attack;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GetTiger.tigerState != Tiger.TState.Attack)
                GetTiger.tigerState = Tiger.TState.Attack;
            Debug.Log("�÷��̾� ������");
            GetTiger.playerT = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�÷��̾� ���� ����");
        GetTiger.tigerState = Tiger.TState.Idle;
    }
}
