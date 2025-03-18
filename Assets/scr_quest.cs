using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class scr_quest : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtquest;
    public List<string> Quests = new List<string> {};
    [SerializeField] GameObject txtquests;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void AddList(string q)
    {
        Quests.Add(q);
        UpdateList();
        txtquests.GetComponent<TextMeshProUGUI>().text = "New Quest:\n" + q;
        txtquests.transform.parent.GetComponent<Animator>().Play("txtquest");
    }

    void UpdateList()
    {
        txtquest.text = "";
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
