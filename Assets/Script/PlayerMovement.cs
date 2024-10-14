using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ความเร็วในการเคลื่อนที่
    public Transform[] tiles; // ช่องต่าง ๆ ที่ผู้เล่นสามารถเดินไปได้
    private int currentTileIndex = 0; // ช่องปัจจุบันที่ผู้เล่นอยู่

    public GameObject leftAppearance; // รูปร่างเมื่อเคลื่อนที่ไปทางซ้าย
    public GameObject rightAppearance; // รูปร่างเมื่อเคลื่อนที่ไปทางขวา
    public GameObject upAppearance; // รูปร่างเมื่อเคลื่อนที่ขึ้น
    public GameObject downAppearance; // รูปร่างเมื่อเคลื่อนที่ลง

    private void UpdateAppearance(GameObject newAppearance)
    {
        // ปิดการแสดงผลของรูปร่างทั้งหมด
        leftAppearance.SetActive(false);
        rightAppearance.SetActive(false);
        upAppearance.SetActive(false);
        downAppearance.SetActive(false);

        // เปิดการแสดงผลของรูปร่างใหม่
        newAppearance.SetActive(true);
    }

    public IEnumerator MovePlayer(int steps)
    {
        // ตรวจสอบว่าต้องเดินถอยหลังหรือไม่
        if (steps < 0)
        {
            steps = Mathf.Abs(steps); // เปลี่ยนให้เป็นบวก
            for (int i = 0; i < steps; i++)
            {
                if (currentTileIndex > 0) // ตรวจสอบว่าไม่ออกนอกขอบเขต
                {
                    currentTileIndex--; // ถอยหลัง
                    yield return StartCoroutine(MoveToTile(currentTileIndex)); // เคลื่อนที่ไปยังช่องนั้น
                }
                else
                {
                    break; // หยุดถ้าถึงช่องแรก
                }
            }
        }
        else
        {
            for (int i = 0; i < steps; i++)
            {
                if (currentTileIndex < tiles.Length - 1)
                {
                    currentTileIndex++; // ไปยังช่องถัดไป
                    yield return StartCoroutine(MoveToTile(currentTileIndex)); // เคลื่อนที่ไปยังช่องนั้น
                }
                else
                {
                    break; // หยุดถ้าถึงช่องสุดท้าย
                }
            }
        }
    }



    private IEnumerator MoveToTile(int tileIndex)
    {
        Vector3 startPosition = transform.position; // ตำแหน่งเริ่มต้นก่อนเคลื่อนที่
        Vector3 targetPosition = tiles[tileIndex].position; // ตำแหน่งเป้าหมาย

        // ตรวจสอบทิศทางและเปลี่ยนรูปร่างของผู้เล่น
        if (targetPosition.x > startPosition.x)
        {
            Debug.Log("Moving along +X axis");
            UpdateAppearance(rightAppearance);
        }
        else if (targetPosition.x < startPosition.x)
        {
            Debug.Log("Moving along -X axis");
            UpdateAppearance(leftAppearance);
        }

        if (targetPosition.y > startPosition.y)
        {
            Debug.Log("Moving along +Y axis");
            UpdateAppearance(upAppearance);
        }
        else if (targetPosition.y < startPosition.y)
        {
            Debug.Log("Moving along -Y axis");
            UpdateAppearance(downAppearance);
        }

        // การเคลื่อนที่
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // รอเฟรมถัดไป
        }

        // ปรับตำแหน่งให้เป็นจำนวนเต็ม
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }

    private int RollDice()
    {
        return Random.Range(1, 7); // ทอยลูกเต๋าและคืนค่าผลลัพธ์ (1-6)
    }
}
