using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scr_quest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtquest;
    List<string> Quests = new List<string> {};
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddList(string q)
    {
        Quests.Add(q);
        UpdateList();
    }

    void UpdateList()
    {
        foreach(string q in Quests)
        {
            txtquest.text += "- " + q + "\n";
        }
    }

    public void RemoveList(string q)
    {
        Quests.Remove(q);
        UpdateList();
    }
}
