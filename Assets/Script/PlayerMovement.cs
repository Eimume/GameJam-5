using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // ความเร็วในการเคลื่อนที่
    public Transform[] tiles; // ท่อต่างๆที่ตัวละครสามารถเดินไปได้
    private int currentTileIndex = 0; // ท่อตัวละครปัจจุบัน

    public GameObject leftAppearance; // การแสดงผลทิศทางซ้าย
    public GameObject rightAppearance; // การแสดงผลทิศทางขวา
    public GameObject upAppearance; // การแสดงผลทิศทางขึ้น
    public GameObject downAppearance; // การแสดงผลทิศทางลง

    public int HPplayer = 100; // ค่า HP ของผู้เล่น
    public Text Hp; // UI แสดงค่า HP
    public GameObject potioon; // Potion UI
    public GameObject Potoin; // ปุ่มใช้ Potion
    public int BuffPotion = 20; // ค่าเพิ่ม HP เมื่อใช้ Potion

    private Vector3 lastPosition; // ตำแหน่งล่าสุดของผู้เล่น
    private bool hasStoppedMoving = false; // เช็คว่าผู้เล่นหยุดเคลื่อนที่แล้วหรือไม่

    // ตัวแปรใหม่ที่จะใช้ในการตรวจสอบว่า Player อยู่ในบล็อกที่มี Tag "DamageBlock"
    private bool isInDamageBlock = false;

    private void Start()
    {
        lastPosition = transform.position; // ตั้งค่าตำแหน่งเริ่มต้น
    }

    private void UpdateAppearance(GameObject newAppearance)
    {
        // ปิดการแสดงผลทุกทิศทาง
        leftAppearance.SetActive(false);
        rightAppearance.SetActive(false);
        upAppearance.SetActive(false);
        downAppearance.SetActive(false);

        // เปิดการแสดงผลทิศทางใหม่
        newAppearance.SetActive(true);
    }

    public IEnumerator MovePlayer(int steps)
    {
        for (int i = 0; i < steps; i++)
        {
            if (currentTileIndex < tiles.Length - 1)
            {
                currentTileIndex++; // ไปที่ท่อถัดไป
                yield return StartCoroutine(MoveToTile(currentTileIndex)); // ย้ายตัวละครไปที่ท่อถัดไป
            }
            else
            {
                break; // หยุดหากไม่มีท่อให้เดิน
            }
        }
    }

    private IEnumerator MoveToTile(int tileIndex)
    {
        Vector3 startPosition = transform.position; // ตำแหน่งเริ่มต้น
        Vector3 targetPosition = tiles[tileIndex].position; // ตำแหน่งท่อที่ต้องการไป

        // ตรวจสอบทิศทางและอัพเดตการแสดงผล
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

        // เคลื่อนที่ตัวละครไปยังท่อที่กำหนด
        while (Vector3.Distance(transform.position, targetPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; // รอจนกว่าจะเสร็จ
        }

        // ปรับตำแหน่งตัวละครให้ตรงกับตำแหน่งท่อ
        transform.position = new Vector3(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y), transform.position.z);
    }

    private int RollDice()
    {
        return Random.Range(1, 7); // ทอยลูกเต๋า (1-6)
    }

    // ฟังก์ชันที่ลดพลังชีวิตของตัวละคร
    public void TakeDamage(int damage)
    {
        HPplayer -= damage;
        Debug.Log("Player's HP: " + HPplayer);

        if (HPplayer <= 0)
        {
            Debug.Log("Player is dead!");
            // คุณสามารถทำการปลดล็อกหรือให้ Game Over ที่นี่
        }
    }

    // ฟังก์ชันที่ตรวจสอบเมื่อ Player ชนกับบล็อกที่มี Tag เป็น "DamageBlock"
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DamageBlock")) // ตรวจสอบว่าเป็นบล็อกที่มี Tag "DamageBlock"
        {
            isInDamageBlock = true; // ผู้เล่นอยู่ในบล็อก
        }
    }

    // ฟังก์ชันที่ตรวจสอบเมื่อ Player ออกจากบล็อก
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DamageBlock")) // ตรวจสอบว่าเป็นบล็อกที่มี Tag "DamageBlock"
        {
            isInDamageBlock = false; // ผู้เล่นออกจากบล็อก
        }
    }

    // การอัพเดต UI ของ HP
    void Update()
    {
        Hp.text = $"HP: {HPplayer}";
        if (HPplayer > 100)
        {
            HPplayer = 100;
        }
        if (HPplayer < 0)
        {
            HPplayer = 0;
        }

        // เช็คว่าผู้เล่นหยุดเคลื่อนที่
        if (transform.position == lastPosition)
        {
            if (!hasStoppedMoving)
            {
                // ถ้าผู้เล่นหยุดเคลื่อนที่และยังไม่เคยลดเลือด
                if (isInDamageBlock)
                {
                    TakeDamage(10); // ลดเลือด 10 เมื่อหยุดที่บล็อก
                }
                hasStoppedMoving = true; // ตั้งค่าสถานะว่าเลือดลดแล้ว
            }
        }
        else
        {
            // ถ้าผู้เล่นกำลังเคลื่อนที่, อัพเดตตำแหน่งล่าสุดและรีเซ็ตการหยุดเคลื่อนที่
            lastPosition = transform.position;
            hasStoppedMoving = false; // รีเซ็ตสถานะเมื่อเริ่มเคลื่อนที่ใหม่
        }
    }

    public void UsePotion() // เมื่อกดใช้จะเพิ่มบัพและ UI ทั้งหมดจะหายไป
    {
        if (HPplayer > 0 && HPplayer < 100)
        {
            HPplayer += BuffPotion;
            Potoin.SetActive(false);
            potioon.SetActive(false);
        }
    }

    public void NoUse() // ปิดหน้าต่าง
    {
        potioon.SetActive(false);
    }

    public void OpenPotion()
    {
        potioon.SetActive(true);
    }
}