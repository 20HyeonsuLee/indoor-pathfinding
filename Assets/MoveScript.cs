using UnityEngine;
using UnityEngine.InputSystem; // 최신 입력 시스템을 위해 필요합니다.

public class SimplePlayerMove : MonoBehaviour
{
    public float speed = 3.0f;
    private CharacterController controller;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        Vector2 inputVector = Vector2.zero;

        // 키보드 입력 체크 (New Input System 방식)
        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) inputVector.y += 1;
            if (Keyboard.current.sKey.isPressed) inputVector.y -= 1;
            if (Keyboard.current.aKey.isPressed) inputVector.x -= 1;
            if (Keyboard.current.dKey.isPressed) inputVector.x += 1;
        }

        // 이동 계산
        Vector3 move = transform.right * inputVector.x + transform.forward * inputVector.y;
        controller.Move(move.normalized * speed * Time.deltaTime);

        // 중력 적용 (바닥에 붙어 있게 함)
        controller.Move(Vector3.down * 9.81f * Time.deltaTime);
    }
}