using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TimeTableFetcher : MonoBehaviour
{
    [SerializeField] private int classId;
    List<List<string>> table = new List<List<string>>();
    string dupa;
    private void Awake() {
        dupa = new WebClient().DownloadString($"https://www.zsl.gda.pl/assets/plan-lekcji/plany/o{classId}.html");
        string cut = "class=\"tabela\">";
        int index = dupa.IndexOf(cut);
        dupa = dupa.Remove(0, index + cut.Length);
        cut = "</table>";
        index = dupa.IndexOf(cut);
        dupa = dupa.Remove(index, dupa.Length - index);
        dupa = dupa.Replace("</tr>", "");
        List<string> collumns = dupa.Split(new string[] { "<tr>" }, System.StringSplitOptions.RemoveEmptyEntries).ToList();
        collumns.RemoveAt(0);
        for (int i = 0; i < collumns.Count; i++)
        {
            List<string> rows = new();
            if (collumns[i].Contains("<td"))
                rows = collumns[i].Split(new string[] { "<td" }, System.StringSplitOptions.RemoveEmptyEntries).ToList();
            else if (collumns[i].Contains("<th"))
                rows = collumns[i].Split(new string[] { "<th" }, System.StringSplitOptions.RemoveEmptyEntries).ToList();

            rows.RemoveAt(0);
            int index1 = 0;
            int index2 = 0;
            for (int g = 0; g < rows.Count; g++)
            {
                index1 = 0;
                index2 = 0;
                int first = rows[g].IndexOf('>');
                rows[g] = rows[g].Remove(0, first + 1);
                while (rows[g].Contains('<') || rows[g].Contains('>'))
                {
                    index1 = rows[g].IndexOf('<');
                    index2 = rows[g].IndexOf('>');
                    rows[g] = rows[g].Remove(index1, index2 - index1 + 1);
                }
            }
            table.Add(rows);
        }

        for (int i = 0; i < table.Count; i++)
        {
            for (int j = 0; j < table[i].Count; j++)
            {
                var text = new GameObject("Text", typeof(RectTransform), typeof(Text));
                text.transform.SetParent(transform);
                text.GetComponent<Text>().text = table[i][j];
                text.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "LegacyRuntime.ttf") as Font;

                var rect = text.GetComponent<RectTransform>();
                rect.anchorMin = new Vector2(0, 1);
                rect.anchorMax = new Vector2(0, 1);
                rect.pivot = new Vector2(0, 1);
                rect.anchoredPosition = new Vector2(j * 200, -i * 100);
                rect.sizeDelta = new Vector2(200, 100);

                text.GetComponent<Text>().alignment = TextAnchor.MiddleCenter;

                text.GetComponent<Text>().color = Color.black;

                text.GetComponent<Text>().fontSize = 24;
                text.GetComponent<Text>().maskable = false;

            }
        }
    }
}
