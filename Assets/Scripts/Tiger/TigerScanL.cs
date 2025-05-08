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
            GetTiger.tigerState = Tiger.TState.Run;

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
