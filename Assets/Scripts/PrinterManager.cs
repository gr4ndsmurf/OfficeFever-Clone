using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterManager : MonoBehaviour
{
    public List<GameObject> paperList = new List<GameObject>();
    public GameObject paperPrefab;
    public Transform exitPoint;

    void Start()
    {
        StartCoroutine(PrintPaper());
    }

    IEnumerator PrintPaper()
    {
        while (true)
        {
            float paperCount = paperList.Count;
            GameObject temp = Instantiate(paperPrefab);
            temp.transform.position = new Vector3(exitPoint.position.x, paperCount / 20, exitPoint.position.z);
            paperList.Add(temp);
            yield return new WaitForSeconds(1);
        }
    }
}
