using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class TestPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public Transform cameraTransform;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public static bool isPlayerMove = true;
    public static bool isPlayerJump = true;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            isPlayerMove = false;
        }
        else if(Input.GetKeyUp(KeyCode.LeftAlt))
        {
            isPlayerMove = true;
        }
        Debug.Log(isPlayerMove);
        if (!isPlayerMove)
            return;
        // �ٴ� üũ
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        // �Է�
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // ī�޶� ���� ���� ���ϱ�
        Vector3 camForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = cameraTransform.right;

        // �̵� ���� ���
        Vector3 move = (camForward * vertical + camRight * horizontal).normalized;
        controller.Move(move * moveSpeed * Time.deltaTime);

        // ����
        if (isPlayerJump && Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // �߷� ����
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}