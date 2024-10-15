using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public int HPplayer = 100;
    public int BuffPotion;
    public Text Hp;
    public GameObject potioon;
    public GameObject Potoin;
    
        void Start() 
    {
        potioon.SetActive(false);
    }

    
    void Update() //เเสดงค่า HP เเละกำหนดให้ HP ไม่มากกว่า 100
    {
        
        Hp.text = $"HP: {HPplayer}";
        if(HPplayer > 100)
        {
            HPplayer = 100;
        }
    }

    public void UsePotion() //เมื่อกดใช้จะเพิ่มบัพเเละ UI ทั้งหมดจะหายไป
    {
        if(HPplayer > 0 && HPplayer < 100)
        {
            HPplayer += BuffPotion;
            Potoin.SetActive(false);
            potioon.SetActive(false);
        }
    }
    public void NoUse() // ปิดหน่าตา่าง
    {
        potioon.SetActive(false);
    }
    public void OpenPotion()
    {
        potioon.SetActive(true);
    }
}
