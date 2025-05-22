using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleManager : MonoBehaviour
{
    public static ConsoleManager Instance;
    public GameObject fullPanel; 
    public Queue<string> logQueue = new Queue<string>();
    public int maxEntries = 50;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI latestText;


    void Awake()
    {
        Instance = this;
    }

    public void AddEntry(string entry)
    {
        logQueue.Enqueue(entry);
        if (logQueue.Count > maxEntries)
            logQueue.Dequeue();

        latestText.text = $"{entry}";
        ConsolePanel.Instance.Refresh();
    }
}
