using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TMPro;
using UnityEngine;

public class TimeTableFetcher : MonoBehaviour
{
    public static TimeTableFetcher Instance { get; private set; }
    private HtmlDocument htmlDoc;
    private TimeTableCreator tableCreator;
    private List<List<string>> table = new();
    [SerializeField] private TMP_Dropdown classesDropdown;
    [SerializeField] private TMP_Dropdown lessonDropdown;
    public string SelectedLesson => lessonDropdown.options[lessonDropdown.value].text;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        tableCreator = GetComponent<TimeTableCreator>();
        htmlDoc = new HtmlDocument();
        RetriveClasses();
        classesDropdown.onValueChanged.AddListener((v) => FillUpLessonsDropdown(v+1));
    }
    private async void FillUpLessonsDropdown(int classId)
    {
        await LoadLessons(classId);
        lessonDropdown.ClearOptions();
        var lessons = new List<string>();
        for (int i = 2; i < table.Count; i++)
        {
            for (int j = 1; j < table[i].Count; j++)
            {
                if (table[i][j].Contains("&")) continue;
                lessons.Add(table[i][j]);
            }
        }
        lessons = lessons.Distinct().ToList();
        foreach (var VARIABLE in lessons)
        {
            var option = new TMP_Dropdown.OptionData(VARIABLE);
            lessonDropdown.options.Add(option);
        }
    }
    private async void RetriveClasses()
    {
        var html = await RetriveHtml($"https://www.zsl.gda.pl/assets/plan-lekcji/lista.html");
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
    private async Task LoadLessons(int classId)
    {
        if (classId == 0) return;
        var classSchedule = await RetriveHtml($"https://www.zsl.gda.pl/assets/plan-lekcji/plany/o{classId}.html");
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
    }
    public async void DisplayTimeTable(int classId)
    {
        await LoadLessons(classId);
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
        var option = new TMP_Dropdown.OptionData(name);
        classesDropdown.options.Add(option);
    }
    private async Task<string> RetriveHtml(string url)
    {
        Uri uri = new(url);
        var client = new WebClient();
        var htmlData = await client.DownloadDataTaskAsync(uri);
        return Encoding.UTF8.GetString(htmlData);
    }
}
