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

    // อ้างอิงไปยังกล้องต่างๆ
    public Camera gameCamera; // กล้องเกมหลัก
    public Camera shopCamera; // กล้องร้านค้า

    // อ้างอิงไปยัง UI ร้านค้า
    public GameObject shopUI;

    void Start()
    {
        // ซ่อนโมเดลทั้งหมดตั้งแต่เริ่มต้น
        downModel.SetActive(false);
        leftModel.SetActive(false);
        rightModel.SetActive(false);
        upModel.SetActive(true);

        // ตรวจสอบว่ากล้องและ UI ถูกตั้งค่าไว้
        if (gameCamera != null)
            gameCamera.gameObject.SetActive(true);
        if (shopCamera != null)
            shopCamera.gameObject.SetActive(false);
        if (shopUI != null)
            shopUI.SetActive(false);
    }

    public IEnumerator MovePlayer(int steps)
    {
        isMoving = true;

        while (steps > 0)
        {
            if (currentTileIndex + 1 >= tiles.Length)
            {
                // ป้องกันไม่ให้เดินเกินขอบเขตของ tiles
                break;
            }

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

            steps--;
            yield return new WaitForSeconds(0.2f); // หน่วงเวลาเล็กน้อยเพื่อให้ดูการเคลื่อนที่ชัดเจน
        }

        // ตรวจสอบช่องพิเศษหลังจากการเคลื่อนที่เสร็จสิ้น
        CheckSpecialTile();

        isMoving = false;
    }

    public IEnumerator MoveBackward(int steps)
    {
        while (steps > 0)
        {
            if (currentTileIndex - 1 < 0)
            {
                // ป้องกันไม่ให้ถอยกลับไปน้อยกว่า 0
                break;
            }

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
            steps--;
            yield return new WaitForSeconds(0.2f); // หน่วงเวลาเล็กน้อยเพื่อให้ดูการถอยกลับชัดเจน
        }

        // ตรวจสอบช่องพิเศษหลังจากการถอยกลับเสร็จสิ้น
        CheckSpecialTile();

        isMoving = false;
    }

    private void ChangeModel()
    {
        Vector3 movementDirection = Vector3.zero;
        if (currentTileIndex + 1 < tiles.Length)
        {
            movementDirection = tiles[currentTileIndex + 1].position - (currentTileIndex > 0 ? tiles[currentTileIndex].position : transform.position);
        }

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

    private void CheckSpecialTile()
    {
        SpecialTile specialTile = tiles[currentTileIndex].GetComponent<SpecialTile>();
        if (specialTile != null)
        {
            if (specialTile.isMoveBackwardTile)
            {
                StartCoroutine(MoveBackward(specialTile.moveBackwardSteps));
            }
            else if (specialTile.isDamageTile)
            {
                // ลดเลือดผู้เล่น
                PlayerHealth playerHealth = GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(specialTile.damageAmount);
                }
            }
            else if (specialTile.isShopTile)
            {
                // เปิดร้านค้า
                OpenShop();
            }
        }
    }

    private void OpenShop()
    {
        if (gameCamera != null && shopCamera != null)
        {
            gameCamera.gameObject.SetActive(false);
            shopCamera.gameObject.SetActive(true);
        }

        if (shopUI != null)
        {
            shopUI.SetActive(true);
        }

        // คุณสามารถเพิ่มฟังก์ชันอื่น ๆ ที่เกี่ยวข้องกับร้านค้าได้ที่นี่ เช่น การหยุดการเคลื่อนที่ หรือการแสดงข้อความ
        Debug.Log("เปิดร้านค้า");
    }

    public void CloseShop()
    {
        if (gameCamera != null && shopCamera != null)
        {
            shopCamera.gameObject.SetActive(false);
            gameCamera.gameObject.SetActive(true);
        }

        if (shopUI != null)
        {
            shopUI.SetActive(false);
        }

        Debug.Log("ปิดร้านค้า");
    }
}
