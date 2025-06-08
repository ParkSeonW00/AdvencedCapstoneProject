using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerAttack : MonoBehaviour
{
    public Tiger GetTiger;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("�÷��̾� ����");
        if (other.CompareTag("Player"))
        {
            // ���ο���
            // GameMgr.Instance.SlowMotion(0.5f);
            //GameMgr.Instance.PlayerInit().SetBoundaryLevel(3);
            GetTiger.tigerState = Tiger.TState.Attack;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("�÷��̾� ���� ����");
        GetTiger.tigerState = Tiger.TState.Idle;
    }
}
