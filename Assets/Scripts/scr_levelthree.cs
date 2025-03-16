using System.Collections.Generic;
using UnityEngine;

public class scr_levelthree : MonoBehaviour
{
    public List<GameObject> listitems = new List<GameObject>();
    public void activateitems()
    {
        foreach(GameObject item in listitems)
        {
            item.GetComponent<scr_item>().isEnabled = true;
        }
    }

    public void OnPreg()
    {
        GameObject.Find("obj_player").GetComponent<scr_inventory>().quest.GetComponent<scr_quest>().AddList("Help the pregnant lady \nfind her husband.");
    }

    public void OnFinishQuest()
    {
        GameObject.Find("obj_player").GetComponent<scr_inventory>().quest.GetComponent<scr_quest>().RemoveList("Help the pregnant lady \nfind her husband.");
    }
}
