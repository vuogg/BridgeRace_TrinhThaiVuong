using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : ColorObject
{
    private BoxCollider boxCollider;
    private MeshRenderer meshRenderer;  // Thêm MeshRenderer để quản lý hình ảnh

    private void Start()
    {
        // Khởi tạo boxCollider bằng cách lấy BoxCollider từ GameObject này
        boxCollider = GetComponent<BoxCollider>();

        // Khởi tạo MeshRenderer để quản lý hình ảnh của Gate
        meshRenderer = GetComponent<MeshRenderer>();

        // Bật hình ảnh của cổng (nếu cần thiết)
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;  // Hiển thị cổng
        }
        else
        {
            Debug.LogError("MeshRenderer is not found.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (boxCollider != null)
        {
            Character character = Cache.GetCharacter(other);
            if (character != null) // Kiểm tra nếu other là một Character
            {
                StartCoroutine(DelayCoroutine());
            }
            else
            {
                Debug.LogError("Other object does not have a Character component.");
            }
        }
        else
        {
            Debug.LogError("boxCollider is not initialized.");
        }
    }

    IEnumerator DelayCoroutine()
    {
        yield return new WaitForSeconds(1);

        // Sau khi chờ xong, tắt chế độ trigger của BoxCollider
        boxCollider.isTrigger = false;

        // Tắt hình ảnh của Gate (ẩn cổng sau khi trigger)
        if (meshRenderer != null)
        {
            meshRenderer.enabled = false;  // Ẩn cổng sau 3 giây
        }
    }
}
