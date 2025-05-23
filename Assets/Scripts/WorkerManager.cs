using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class WorkerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> paperList = new List<GameObject>();
    [SerializeField] private List<GameObject> moneyList = new List<GameObject>();
    [SerializeField] private Transform paperPoint, moneyPoint;
    [SerializeField] private GameObject paperPrefab, moneyPrefab;
    [SerializeField] private float paperHeight, moneyHeight = 0.02f;
    [SerializeField] private float paperDropHeight, moneyDropHeight = 1.0f;
    [SerializeField] private float dropDuration = 0.3f;
    [SerializeField] private float generateDuration = 1f;

    private Transform lastPaper, lastMoney;

    void Start()
    {
        StartCoroutine(GenerateMoney());
    }

    IEnumerator GenerateMoney()
    {
        while (true)
        {
            if (paperList.Count > 0)
            {
                Vector3 targetPos = lastMoney ? lastMoney.position + Vector3.up * moneyHeight : moneyPoint.position;
                Vector3 startPos = targetPos + Vector3.up * moneyDropHeight;

                GameObject temp = Instantiate(moneyPrefab, startPos, quaternion.identity);
                temp.transform.DOMove(targetPos, dropDuration).SetEase(Ease.OutQuad);

                lastMoney = temp.transform;
                moneyList.Add(temp);
                RemoveLast();
            }

            yield return new WaitForSeconds(generateDuration);
        }
    }

    public void GetPaper()
    {
        GameObject temp = Instantiate(paperPrefab, paperPoint);

        Vector3 targetPos = lastPaper ? lastPaper.position + Vector3.up * paperHeight : paperPoint.position;
        Vector3 startPos = targetPos + Vector3.up * paperDropHeight;

        temp.transform.position = startPos;
        temp.transform.DOMove(targetPos, dropDuration).SetEase(Ease.OutQuad);

        lastPaper = temp.transform;
        paperList.Add(temp);
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
}
