using GLTFast.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TigerAttack : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("�÷��̾� ����");
        if (other.CompareTag("Player") && !transform.parent.GetComponent<Animator>().GetBool("IsRunBack"))
        {
            Debug.Log("���ݰ���");
            // ���ο���
            // GameMgr.Instance.SlowMotion(0.5f);
            //GameMgr.Instance.PlayerInit().SetBoundaryLevel(3);
            transform.parent.GetComponent<Tiger>().isAttack = true;
            transform.parent.GetComponent<Tiger>().tigerState = Tiger.TState.Attack;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        //Debug.Log("�÷��̾� ���� ����");
        transform.parent.GetComponent<Tiger>().tigerState = Tiger.TState.Idle;
    }
}
