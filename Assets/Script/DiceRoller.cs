using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DiceRoller : MonoBehaviour
{
    public Button rollDiceButton; // ปุ่มทอยลูกเต๋า
    public TextMeshProUGUI diceResultText; // ข้อความแสดงผลลูกเต๋า
    public PlayerMovement playerMovement; // สคริปต์การเคลื่อนที่ของผู้เล่น
    public float rollDuration = 1.0f; // ระยะเวลาในการหมุน
    public float rollSpeed = 0.05f; // ความเร็วในการเปลี่ยนตัวเลข

    private System.Random random = new System.Random();

    void Start()
    {
        // เพิ่มฟังก์ชันที่เรียกเมื่อปุ่มถูกคลิก
        rollDiceButton.onClick.AddListener(RollDice);
    }

    void RollDice()
    {
        int diceResult = random.Next(1, 7); // ทอยลูกเต๋าแบบ 6 หน้า
        StartCoroutine(RollDiceAnimation(diceResult)); // เรียกใช้งานอนิเมชั่นการหมุน
    }

    IEnumerator RollDiceAnimation(int finalResult)
    {
        float elapsedTime = 0f;

        // หมุนตัวเลขแบบวนไปเรื่อยๆ จนกว่าจะครบเวลาที่กำหนด
        while (elapsedTime < rollDuration)
        {
            // เปลี่ยนตัวเลข 1 ถึง 6 ไปเรื่อยๆ
            int currentNumber = (int)Mathf.Ceil(elapsedTime / rollSpeed) % 6 + 1;
            diceResultText.text = "Result: " + currentNumber.ToString(); // เพิ่มคำว่า "Result: " ระหว่างหมุน

            elapsedTime += rollSpeed;
            yield return new WaitForSeconds(rollSpeed);
        }

        // เมื่ออนิเมชั่นจบ ให้แสดงผลลัพธ์สุดท้าย
        diceResultText.text = "Result: " + finalResult.ToString();

        // ส่งค่าจำนวนก้าวที่ผู้เล่นต้องเดิน
        StartCoroutine(playerMovement.MovePlayer(finalResult)); // เรียกใช้งาน MovePlayer ผ่าน Coroutine
    }
}