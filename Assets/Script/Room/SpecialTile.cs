using UnityEngine;

public class SpecialTile : MonoBehaviour
{
    public bool isMoveBackwardTile = false; // กำหนดว่าช่องนี้เป็นช่องถอยหลังหรือไม่
    public int moveBackwardSteps = 3; // จำนวนช่องที่ถอยกลับ (ในกรณีที่เป็นช่องพิเศษ)
}
