using UnityEngine;

public class BuyManager : MonoBehaviour
{
    public int MoneyCount = 0;

    void OnEnable()
    {
        TriggerManager.OnMoneyCollected += IncreaseMoney;
    }
    void OnDisable()
    {
        TriggerManager.OnMoneyCollected -= IncreaseMoney;
    }

    void IncreaseMoney()
    {
        MoneyCount += 50;
    }
}
