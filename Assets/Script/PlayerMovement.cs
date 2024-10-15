using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] tiles; // Array ของตำแหน่งของแต่ละช่องในเกม
    public int currentTileIndex = 0; // ตำแหน่งเริ่มต้นของผู้เล่น
    public float moveSpeed = 2f; // ความเร็วในการเคลื่อนที่ของผู้เล่น

    public GameObject upModel; // โมเดลสำหรับการเคลื่อนที่ขึ้น
    public GameObject downModel; // โมเดลสำหรับการเคลื่อนที่ลง
    public GameObject leftModel; // โมเดลสำหรับการเคลื่อนที่ซ้าย
    public GameObject rightModel; // โมเดลสำหรับการเคลื่อนที่ขวา

    private bool isMoving = false;

    void Start()
    {
        // ซ่อนโมเดลทั้งหมดตั้งแต่เริ่มต้น
        downModel.SetActive(false);
        leftModel.SetActive(false);
        rightModel.SetActive(false);
        // โดยให้โมเดลขึ้นเป็นโมเดลเริ่มต้นหรือสามารถปรับตามที่ต้องการได้
        upModel.SetActive(true);
    }

    public IEnumerator MovePlayer(int steps)
    {
        isMoving = true;

        // เดินไปตามจำนวนที่ได้จากการทอยเต๋า
        while (steps > 0)
        {
            Vector3 targetPosition = tiles[currentTileIndex + 1].position;

            // เปลี่ยนโมเดลตามทิศทางการเคลื่อนที่ก่อนเริ่มเดิน
            ChangeModel();

            // เคลื่อนที่ไปที่ช่องถัดไปทีละนิด
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            currentTileIndex++; // เพิ่มตำแหน่งทีละ 1

            if (currentTileIndex >= tiles.Length - 1)
            {
                currentTileIndex = tiles.Length - 1; // ป้องกันไม่ให้เดินเกิน
                break;
            }

            steps--;
            yield return new WaitForSeconds(0.2f); // หน่วงเวลาเล็กน้อยเพื่อให้ดูการเคลื่อนที่ชัดเจน
        }

        // หลังจากเดินเสร็จแล้ว ตรวจสอบว่าช่องปัจจุบันเป็นช่องพิเศษหรือไม่
        SpecialTile specialTile = tiles[currentTileIndex].GetComponent<SpecialTile>();
        if (specialTile != null && specialTile.isMoveBackwardTile)
        {
            StartCoroutine(MoveBackward(specialTile.moveBackwardSteps)); // ถอยกลับตามจำนวนที่กำหนด
        }
        else
        {
            isMoving = false;
        }
    }

    public IEnumerator MoveBackward(int steps)
    {
        while (steps > 0)
        {
            Vector3 targetPosition = tiles[currentTileIndex - 1].position;

            // เปลี่ยนโมเดลตามทิศทางการเคลื่อนที่ก่อนเริ่มเดิน
            ChangeModel();

            // เคลื่อนที่กลับไปที่ช่องถัดไปทีละนิด
            while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }

            currentTileIndex--; // ลดตำแหน่งทีละ 1
            if (currentTileIndex < 0)
            {
                currentTileIndex = 0; // ป้องกันไม่ให้ถอยกลับไปน้อยกว่า 0
                break;
            }

            steps--;
            yield return new WaitForSeconds(0.2f); // หน่วงเวลาเล็กน้อยเพื่อให้ดูการถอยกลับชัดเจน
        }

        isMoving = false;
    }

    private void ChangeModel()
    {
        Vector3 movementDirection = tiles[currentTileIndex + 1].position - (currentTileIndex > 0 ? tiles[currentTileIndex].position : transform.position);

        // ปิดโมเดลทั้งหมดก่อน
        upModel.SetActive(false);
        downModel.SetActive(false);
        leftModel.SetActive(false);
        rightModel.SetActive(false);

        // ตรวจสอบทิศทางการเคลื่อนที่
        if (movementDirection.y > 0)
        {
            upModel.SetActive(true); // เคลื่อนที่ขึ้น
        }
        else if (movementDirection.y < 0)
        {
            downModel.SetActive(true); // เคลื่อนที่ลง
        }
        else if (movementDirection.x > 0)
        {
            rightModel.SetActive(true); // เคลื่อนที่ขวา
        }
        else if (movementDirection.x < 0)
        {
            leftModel.SetActive(true); // เคลื่อนที่ซ้าย
        }
    }
}
