using UnityEngine;
using UnityEngine.UI;

public class BuyArea : MonoBehaviour
{
    [SerializeField] private Image progressImage;
    [SerializeField] private GameObject deskGameObject, buyGameObject;
    [SerializeField] private float cost = 100;

    private float currentMoney;


    public void Buy(int amount)
    {
        if (amount <= 0 || cost <= 0)
            return;

        currentMoney += amount;
        float progress = Mathf.Clamp01((float)currentMoney / cost);
        progressImage.fillAmount = progress;

        if (currentMoney >= cost)
        {
            buyGameObject.SetActive(false);
            deskGameObject.SetActive(true);
            enabled = false; // Bu script artık işini tamamladıysa, devre dışı kalabilir
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
