using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerScanL : MonoBehaviour
{
    public Tiger GetTiger;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�÷��̾� ����");
        if (other.CompareTag("Player"))
        {
            if(GameMgr.Instance.PlayerInit().GetBoundaryLevel() >= 2)
            {
                GetTiger.tigerState = Tiger.TState.Run;
            }
            else
            {
                GetTiger.tigerState = Tiger.TState.Idle;
            }

        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
