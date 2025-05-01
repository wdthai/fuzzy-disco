using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsoleManager : MonoBehaviour
{
    public static ConsoleManager Instance;
    public GameObject fullPanel; 
    public Queue<string> logQueue = new Queue<string>();
    public int maxEntries = 10;
    public TextMeshProUGUI latestText;

    void Awake()
    {
        Instance = this;
        // fullPanel.SetActive(false);
    }

    public void AddEntry(string entry)
    {
        logQueue.Enqueue(entry);
        if (logQueue.Count > maxEntries)
            logQueue.Dequeue();

        latestText.text = entry;
        ConsolePanel.Instance.Refresh();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("K pressed");
            AddEntry("Test Entry K");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("P pressed");
            AddEntry("Test Entry P");
        }
    }
}
