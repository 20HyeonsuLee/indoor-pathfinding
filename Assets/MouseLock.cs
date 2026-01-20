using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Player2 오브젝트를 연결할 곳
    float xRotation = 0f;

    void Start()
    {
        // 게임 시작 시 마우스 커서를 화면 중앙에 고정하고 숨깁니다.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // 마우스의 움직임(Delta) 값을 가져옵니다.
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseDelta.y * mouseSensitivity * Time.deltaTime;

        // 위아래 회전 (카메라만 회전)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 고개가 뒤로 꺾이지 않게 제한

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 좌우 회전 (플레이어 몸체 전체를 회전)
        playerBody.Rotate(Vector3.up * mouseX);
    }
}