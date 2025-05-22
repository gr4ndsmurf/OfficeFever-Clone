using System.Collections;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public delegate void OnCollectArea();
    public static event OnCollectArea OnPaperCollect;
    public static PrinterManager PrinterManager;


    public delegate void OnDeskArea();
    public static event OnDeskArea OnPaperGive;
    public static WorkerManager WorkerManager;

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
            PrinterManager = other.gameObject.GetComponent<PrinterManager>();
        }

        if (other.gameObject.CompareTag("WorkArea"))
        {
            isGiving = true;
            WorkerManager = other.gameObject.GetComponent<WorkerManager>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CollectArea"))
        {
            isCollecting = false;
            PrinterManager = null;
        }

        if (other.gameObject.CompareTag("WorkArea"))
        {
            isGiving = false;
            WorkerManager = null;
        }
    }
}
