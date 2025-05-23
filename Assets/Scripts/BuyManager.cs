using UnityEngine;
using TMPro;

public class BuyManager : MonoBehaviour
{
    public int MoneyCount = 0;

    [SerializeField] private TextMeshProUGUI moneyText;

    void Start()
    {
        moneyText.text = MoneyCount.ToString();
    }

    void OnEnable()
    {
        TriggerManager.OnMoneyCollected += IncreaseMoney;
        TriggerManager.OnBuyingDesk += BuyArea;
    }
    void OnDisable()
    {
        TriggerManager.OnMoneyCollected -= IncreaseMoney;
        TriggerManager.OnBuyingDesk -= BuyArea;
    }

    void IncreaseMoney()
    {
        MoneyCount += 50;
        moneyText.text = MoneyCount.ToString();
    }

    void BuyArea()
    {
        if (TriggerManager.areaToBuy != null)
        {
            if (MoneyCount >= 1)
            {
                TriggerManager.areaToBuy.Buy(1);
                MoneyCount -= 1;
                moneyText.text = MoneyCount.ToString();
            }
        }
    }
}
