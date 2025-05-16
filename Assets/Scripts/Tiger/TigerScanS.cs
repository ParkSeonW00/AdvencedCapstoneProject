using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TigerScanS : MonoBehaviour
{
    public Tiger GetTiger;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�÷��̾� ����");
        if (other.CompareTag("Player"))
        {
            GetTiger.checkRange = 2;
            GetTiger.TigerStateChanger();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ������");
            GetTiger.playerT = other.transform;
            GetTiger.TigerStateChanger();
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�÷��̾� ���� ����");
        GetTiger.checkRange = 1;
        //GameMgr.Instance.PlayerInit().SetUpBoundaryLevel();
        GetTiger.tigerState = Tiger.TState.Idle;
    }

}
