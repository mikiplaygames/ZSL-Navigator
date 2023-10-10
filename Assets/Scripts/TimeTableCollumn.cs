using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeTableCollumn : MonoBehaviour
{
    [SerializeField] private GameObject tablet;
    private List<TMP_Text> rows = new();
    
    public void SetRow(int horizontalIndex, string text)
    {
        if (horizontalIndex >= rows.Count)
            AddRow(horizontalIndex, text);
        else
            rows[horizontalIndex].text = text;
    }
    private void AddRow(int horizontalIndex, string text)
    {
        var row = Instantiate(tablet, transform).GetComponentInChildren<TMP_Text>();
        row.text = text;
        rows.Add(row);
    }
}
