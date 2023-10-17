using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using UnityEngine;

public class TimeTableFetcher : MonoBehaviour
{
    [SerializeField] private int classId;
    private HtmlDocument htmlDoc;
    [SerializeField] private TMPro.TMP_Dropdown classesDropdown;
    private TimeTableCreator tableCreator;
    private void Awake()
    {
        tableCreator = GetComponent<TimeTableCreator>();
        htmlDoc = new HtmlDocument();
        var html = RetriveHtml($"https://www.zsl.gda.pl/assets/plan-lekcji/lista.html");
        htmlDoc.LoadHtml(html);
        var classList = htmlDoc.DocumentNode.Descendants("a");
        foreach (var VARIABLE in classList)
        {
            try
            {
                if (VARIABLE.ParentNode.ParentNode.Attributes["class"].Value == "blk")
                    AddClass(VARIABLE.InnerHtml);
            }catch{}
        }
    }
    public void Reload()
    {
        var classSchedule = RetriveHtml($"https://www.zsl.gda.pl/assets/plan-lekcji/plany/o{classesDropdown.value+1}.html");
        htmlDoc.LoadHtml(classSchedule);
        var stringTable = htmlDoc.DocumentNode.Descendants("table").Where(x => x.GetAttributeValue("class", "") == "tabela").First();
        List<List<string>> table = new();
        foreach (var columnName in stringTable.Descendants("th"))
        {
            table.Add(new List<string>{columnName.InnerText});
        }
        
        foreach (var row in stringTable.Descendants("tr"))
        {
            int i = 0;
            foreach (var column in row.Descendants("td"))
            {
                table[i].Add(column.InnerText);
                i++;
            }
        }

        for (int i = 0; i < table.Count; i++)
        {
            for (int j = 0; j < table[i].Count; j++)
            {
                tableCreator.SetColumn(i,j, table[i][j]);
            }
            tableCreator.DisableEmpty(table[i].Count);
        }
    }
    private void AddClass(string name)
    {
        var option = new TMPro.TMP_Dropdown.OptionData(name);
        classesDropdown.options.Add(option);
    }
    private string RetriveHtml(string url)
    {
        var htmlData = new WebClient().DownloadData(url);
        return Encoding.UTF8.GetString(htmlData);
    }
}
