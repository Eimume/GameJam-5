using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
   public GameObject Item;
   public int price;
   public int money;
   public Text Currentmoney;
   public Text pricetext;
    void Start()
    {
        Item.SetActive(false);
    }

    // Update is called once per frame
    void Update() // เเสดงจำนวนเงินปัจจุบัน เเละราคาสินค้า
    {
        Currentmoney.text = $"Money : {money} $";
        pricetext.text = $"{price} $";
    }
    public void Buy() // เมื่อกดจะทำการหักเงินตามราคาสินค้า
    {
        if(money >= price)
        {
           money -= price;
        }
        
        
    }

    public void NoBuy() // ปิดหน้าต่าง
    {
        Item.SetActive(false);
    }
    public void OpenTobuy()
    {
        Item.SetActive(true);
    }
}
