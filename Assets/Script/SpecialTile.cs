using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    public int stepsToMoveBack = 1; // จำนวนก้าวที่ต้องถอยหลังเมื่อยืนอยู่บนช่องพิเศษ

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered special tile"); // เพิ่ม Debug.Log เพื่อดูการตรวจจับ
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
            if (playerMovement != null)
            {
                playerMovement.MovePlayer(-stepsToMoveBack);
            }
        }
    }

}
