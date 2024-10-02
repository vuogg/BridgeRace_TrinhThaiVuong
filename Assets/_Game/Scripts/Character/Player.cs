using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed = 5;
    public Joystick Joystick;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsState(GameState.Gameplay))
        {
            Move();
        }
    }

    public void Move()
    {
        // Lấy đầu vào từ joystick
        float horizontal = Joystick.Horizontal;
        float vertical = Joystick.Vertical;

        // Chuyển đổi đầu vào thành vector3
        Vector3 moveDirection = new Vector3(horizontal, 0, vertical);

        // Nếu có đầu vào, xoay và di chuyển nhân vật
        if (moveDirection != Vector3.zero)
        {
            // Xoay nhân vật theo hướng di chuyển
            Quaternion lookRotation = Quaternion.LookRotation(moveDirection);
            TF.rotation = Quaternion.Slerp(TF.rotation, lookRotation, Time.deltaTime * 10f);

            // Tính toán vị trí tiếp theo
            Vector3 nextPoint = TF.position + moveDirection.normalized * speed * Time.deltaTime;
            ChangeAnim(Constants.ANIM_RUN);
            //transform.position = nextPoint;
            // Kiểm tra xem nhân vật có thể di chuyển tới vị trí mới không
            if (CanMove(nextPoint))
            {
                if (CheckGround(nextPoint))
                {
                    TF.position = nextPoint;

                }
            }
        }
        else
        {
            ChangeAnim(Constants.ANIM_IDLE);
        }
    }
}
