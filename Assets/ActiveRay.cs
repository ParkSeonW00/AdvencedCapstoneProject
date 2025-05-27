using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit; // XR ���ͷ��� ��Ŷ�� ����մϴ�.
using UnityEngine.InputSystem; // �ۺ� ���� �Է��� ���� �Է°��� �ý����� ����մϴ�.

public class ActiveRay : MonoBehaviour
{
    public GameObject RightTP; // �� ������Ʈ�� �ۺ� ������ �Է¹޽��ϴ�.

    public InputActionProperty RightActivate; // ������ Ʈ���� ��ư �Է��� �ް� ���� ��� ���� ����

    // Update is called once per frame
    void Update()
    {
        RightTP.SetActive(RightActivate.action.ReadValue<float>() > 0.1f);
        // ������ Ʈ���� ��ư�� �Է°��� 0.1f���� ũ�ٸ� ������ �ڷ���Ʈ �� Ȱ��ȭ
    }
}

