using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // ลิงก์ไปยัง Transform ของผู้เล่น
    public Vector3 offset;    // ระยะห่างระหว่างกล้องกับผู้เล่น
    private Camera cameraComponent;

    void Start()
    {
        // บังคับใช้กล้องนี้เป็นกล้องหลักเมื่อเริ่มเกม
        cameraComponent = GetComponent<Camera>();

        if (cameraComponent != null)
        {
            // ปิดการใช้งานกล้องอื่น ๆ ที่เป็น MainCamera
            Camera[] allCameras = Camera.allCameras;
            foreach (Camera cam in allCameras)
            {
                if (cam != cameraComponent && cam.CompareTag("MainCamera"))
                {
                    cam.gameObject.SetActive(false);
                }
            }

            // ตั้งกล้องนี้เป็น Main Camera
            cameraComponent.tag = "MainCamera";
        }
    }

    void Update()
    {
        // ให้ตำแหน่งกล้องตามผู้เล่นโดยเพิ่ม offset
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }
}
