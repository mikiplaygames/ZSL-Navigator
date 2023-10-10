using System.Collections.Generic;
using UnityEngine;

public class TimeTableCreator : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private GameObject rowPrefab;
    private List<TimeTableCollumn> rows = new();
    public void SetColumn(int verticalIndex, int horizontalIndex, string text)
    {
        if (verticalIndex >= rows.Count)  
            AddColumn(horizontalIndex, text);
        else
            rows[verticalIndex].SetRow(horizontalIndex, text);
    }

    private void AddColumn(int verticalIndex, string text)
    {
        var column = Instantiate(rowPrefab, container).GetComponent<TimeTableCollumn>();
        rows.Add(column);
        column.SetRow(verticalIndex, text);
    }
}