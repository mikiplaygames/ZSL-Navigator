using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using HtmlAgilityPack;
using UnityEngine;
using UnityEngine.UI;

public class TimeTableFetcher : MonoBehaviour
{
    public static TimeTableFetcher Instance { get; private set; }
    private HtmlDocument htmlDoc;
    private TimeTableCreator tableCreator;
    private List<List<string>> table = new();
    [SerializeField] private TMPro.TMP_Dropdown classesDropdown;
    [SerializeField] private Button classesButton;
    [SerializeField] private TMPro.TextMeshProUGUI classText;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        tableCreator = GetComponent<TimeTableCreator>();
        htmlDoc = new HtmlDocument();
        RetriveClasses();
        classesButton.onClick.AddListener(() =>
        {
            //cla
        });
    }
    private void RetriveClasses()
    {
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
    public void Reload(int classId)
    {
        var classSchedule = RetriveHtml($"https://www.zsl.gda.pl/assets/plan-lekcji/plany/o{classId}.html");
        htmlDoc.LoadHtml(classSchedule);
        var stringTable = htmlDoc.DocumentNode.Descendants("table").First(x => x.GetAttributeValue("class", "") == "tabela");
        table.Clear();
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

    public void Hide()
    {
        for (int i = 0; i < table.Count; i++)
        {
            tableCreator.DisableEmpty(0);
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
