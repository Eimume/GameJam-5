using UnityEngine;

[CreateAssetMenu(fileName = "ItemType", menuName = "Scriptable Objects/ItemType")]
public class ItemType : ScriptableObject
{
    public string itemName;    // ชื่อของไอเท็ม
    public int BuffEffect;     // ค่าบัฟที่ไอเท็มจะให้ เช่น HP เพิ่มขึ้นเท่าไหร่
    public Sprite itemIcon;    // ไอคอนของไอเท็มที่จะแสดงใน UI
    
}
