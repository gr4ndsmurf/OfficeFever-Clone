using System.Collections.Generic;
using UnityEngine;

public class WorkerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> paperList = new List<GameObject>();
    [SerializeField] private Transform paperPoint;
    [SerializeField] private GameObject paperPrefab;
    [SerializeField] private float paperHeight = 0.02f;


    private Transform lastPaper;

    public void GetPaper()
    {
        GameObject temp = Instantiate(paperPrefab, paperPoint);
        temp.transform.position = lastPaper ? lastPaper.position + Vector3.up * paperHeight : paperPoint.position;
        lastPaper = temp.transform;

        paperList.Add(temp.gameObject);
    }
}
