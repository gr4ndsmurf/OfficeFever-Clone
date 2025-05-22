using System.Collections;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public delegate void OnCollectArea();
    public static event OnCollectArea OnPaperCollect;
    public static PrinterManager printerManager;


    public delegate void OnDeskArea();
    public static event OnDeskArea OnPaperGive;

    bool isCollecting;
    bool isGiving;

    [SerializeField] private float CollectTime = 0.5f;

    void Start()
    {
        StartCoroutine(CollectEnum());
    }

    IEnumerator CollectEnum()
    {
        while (true)
        {
            if (isCollecting)
            {
                OnPaperCollect();
            }
            if (isGiving)
            {
                OnPaperGive();
            }
            yield return new WaitForSeconds(CollectTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            isCollecting = true;
            printerManager = other.gameObject.GetComponent<PrinterManager>();
        }

        if (other.gameObject.CompareTag("WorkArea"))
        {
            isGiving = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            isCollecting = false;
            printerManager = null;
        }

        if (other.gameObject.CompareTag("WorkArea"))
        {
            isGiving = false;
        }
    }
}
