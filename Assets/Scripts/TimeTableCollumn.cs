using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeTableCollumn : MonoBehaviour
{
    [SerializeField] private GameObject tablet;
    private List<TMP_Text> rows = new();
    
    public void SetRow(int horizontalIndex, string text)
    {
        int index = text.IndexOf("&");
        text = (index < 0)
            ? text
            : text.Remove(index, 6);

        if (horizontalIndex >= rows.Count)
            AddRow(text);
        else
        {
            rows[horizontalIndex].transform.parent.gameObject.SetActive(true);
            rows[horizontalIndex].text = text;
        }
    }
    public void DisableEmpty(int count)
    {
        int howMany = rows.Count - count;
        for (int i = 1; i <= howMany; i++)
        {
            rows[rows.Count-i].transform.parent.gameObject.SetActive(false);
        }
    }
    private void AddRow(string text)
    {
        var row = Instantiate(tablet, transform).GetComponentInChildren<TMP_Text>();
        row.text = text;
        rows.Add(row);
    }
}
