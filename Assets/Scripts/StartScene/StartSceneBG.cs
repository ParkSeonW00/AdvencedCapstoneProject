using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneBG : MonoBehaviour
{
    public float parallaxIntensity = 30f; // �ȼ� ���� ������
    public Vector2 maxOffset = new Vector2(50f, 30f); // �ִ� �̵� ���� (�ȼ�)

    private RectTransform rectTransform;
    private Vector2 initialAnchoredPosition;

    private Vector3 initialPosition;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialAnchoredPosition = rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBGMouse();
    }

    public void MoveBGMouse()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 screenSize = new Vector2(Screen.width, Screen.height);

        // ����Ʈ ��ǥ ���� (0~1), �߽��� (0.5, 0.5)
        Vector2 normalized = new Vector2(mousePos.x / screenSize.x - 0.5f, mousePos.y / screenSize.y - 0.5f);

        // �̵� ���� ���
        Vector2 offset = normalized * parallaxIntensity;

        // �̵� ���� ����
        offset.x = Mathf.Clamp(offset.x, -maxOffset.x, maxOffset.x);
        offset.y = Mathf.Clamp(offset.y, -maxOffset.y, maxOffset.y);

        rectTransform.anchoredPosition = initialAnchoredPosition + offset;
    }
}
