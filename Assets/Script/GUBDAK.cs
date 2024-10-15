using UnityEngine;

public class GUBDAK : MonoBehaviour
{
   public PlayerHP playerHP;
   private void OnCollisionEnter2D(Collision2D collision)
    {
        // ตรวจสอบว่า GameObject ที่ชนมีแท็กที่กำหนด (เช่น "Enemy")
        if (collision.gameObject.CompareTag("GUBDAK"))
        {
            // เรียกใช้ฟังก์ชันทำดาเมจ
            TakeDamage(10);  // สมมติว่าได้รับดาเมจ 10 หน่วย
        }
    }

    // ฟังก์ชันทำดาเมจ
    void TakeDamage(int damage)
    {
        playerHP.HPplayer -= damage;
        Debug.Log("Health: " + playerHP.HPplayer);

        if (playerHP.HPplayer <= 0)
        {
            Die();
        }
    }

    // ฟังก์ชันที่เรียกเมื่อ HP เท่ากับ 0 (ตาย)
    void Die()
    {
        Debug.Log("Player is dead!");
        // คุณสามารถทำให้ตัวละครตาย หรือทำการลบ GameObject นี้
        Destroy(gameObject);
    }
    
}
