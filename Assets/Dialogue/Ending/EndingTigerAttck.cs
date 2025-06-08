using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingTigerAttck : MonoBehaviour
{
    public EndingTiger GetTiger;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�÷��̾� ����");
        if (other.CompareTag("Player"))
        {
            // ���ο���
            // GameMgr.Instance.SlowMotion(0.5f);
            GameMgr.Instance.PlayerInit().SetBoundaryLevel(3);
            GetTiger.TigerStateChanger();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("�÷��̾� ������");
            GetTiger.TigerStateChanger();

            GetTiger.playerT = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("�÷��̾� ���� ����");
        GetTiger.tigerState = EndingTiger.TState.Idle;
    }
}
