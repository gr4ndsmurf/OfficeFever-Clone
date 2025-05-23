using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PrinterManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> paperList = new List<GameObject>();
    [SerializeField] private int paperLimit = 30;
    [SerializeField] private GameObject paperPrefab;
    [SerializeField] private float paperHeight = 0.02f;
    [SerializeField] private float paperDropHeight = 1.0f;
    [SerializeField] private float dropDuration = 0.2f;
    [SerializeField] private float columnOffset = 0.5f; // İki sütun arası mesafe
    [SerializeField] private float paperPrintTime = 1f;
    [SerializeField] private Transform exitPoint;

    private Transform lastPaper;
    private bool isWorking;

    void Start()
    {
        StartCoroutine(PrintPaper());
    }

    public void RemoveLast()
    {
        if (paperList.Count > 0)
        {
            Transform target = paperList[paperList.Count - 1].transform;
            DOTween.Kill(target);
            Destroy(target.gameObject);
            paperList.RemoveAt(paperList.Count - 1);
        }
    }

    IEnumerator PrintPaper()
    {
        while (true)
        {
            if (isWorking)
            {
                int index = paperList.Count;
                int column = index / 10; // Kaçıncı sütunda
                int row = index % 10;    // Sütun içindeki sıra

                Vector3 basePosition = exitPoint.position + Vector3.right * column * columnOffset + Vector3.up * row * paperHeight;

                Vector3 spawnPosition = basePosition + Vector3.up * paperDropHeight;

                GameObject temp = Instantiate(paperPrefab, spawnPosition, Quaternion.identity);
                temp.transform.DOMove(basePosition, dropDuration).SetEase(Ease.OutBounce);

                paperList.Add(temp);

                if (paperList.Count >= paperLimit)
                {
                    isWorking = false;
                }
            }
            else if (paperList.Count < paperLimit)
            {
                isWorking = true;
            }

            yield return new WaitForSeconds(paperPrintTime);
        }
    }
}
