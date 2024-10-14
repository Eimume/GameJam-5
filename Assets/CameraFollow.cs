using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // ลิงก์ไปยัง Transform ของผู้เล่น
    public Vector3 offset;    // ระยะห่างระหว่างกล้องกับผู้เล่น

    void Update()
    {
        // ให้ตำแหน่งกล้องตามผู้เล่นโดยเพิ่ม offset
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y, transform.position.z);
    }
}
