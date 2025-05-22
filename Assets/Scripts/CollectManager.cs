using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> paperList = new List<GameObject>();
    [SerializeField] private int paperLimit;
    [SerializeField] private GameObject paperPrefab;
    [SerializeField] private Transform collectPoint;

    [SerializeField] private float paperHeight = 0.02f;
    [SerializeField] private float paperDropHeight = 1.0f;
    [SerializeField] private float dropDuration = 0.3f;

    private Transform lastPaper;

    void OnEnable()
    {
        TriggerManager.OnPaperCollect += GetPaper;
    }
    void OnDisable()
    {
        TriggerManager.OnPaperCollect -= GetPaper;
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

    void GetPaper()
    {
        if (paperList.Count < paperLimit)
        {
            GameObject temp = Instantiate(paperPrefab, collectPoint);

            Vector3 localTargetPos = lastPaper ? lastPaper.localPosition + Vector3.up * paperHeight : Vector3.zero;
            Vector3 localStartPos = localTargetPos + Vector3.up * paperDropHeight;

            temp.transform.localPosition = localStartPos;

            temp.transform.DOLocalMove(localTargetPos, dropDuration).SetEase(Ease.OutQuad);

            lastPaper = temp.transform;
            paperList.Add(temp.gameObject);

            if (TriggerManager.printerManager != null)
            {
                TriggerManager.printerManager.RemoveLast();
            }
        }
    }

    void GivePaper()
    {

    }

}
