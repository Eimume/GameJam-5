using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    public bool isMoveBackwardTile = false; // กำหนดว่าช่องนี้เป็นช่องถอยหลังหรือไม่
    public int moveBackwardSteps = 3; // จำนวนช่องที่ถอยกลับ (ในกรณีที่เป็นช่องพิเศษ)

    public bool isDamageTile = false; // กำหนดว่าช่องนี้เป็นช่องลดเลือดหรือไม่
    public int damageAmount = 10; // จำนวนเลือดที่ลดลงเมื่อหยุดอยู่ที่ช่องนี้

    public bool isShopTile = false; // กำหนดว่าช่องนี้เป็นช่องร้านค้าหรือไม่
}
