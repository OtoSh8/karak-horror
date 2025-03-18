using System.Collections.Generic;
using UnityEngine;

public class scr_levelthree : MonoBehaviour
{
    public List<GameObject> listitems = new List<GameObject>();
    public scr_item corpse;
    public scr_item parang;

    public scr_billb wife;
    public scr_preg karak;

    public GameObject spawn;
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
        corpse.isEnabled = true;
        wife.isEnabled = false;
        wife.gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

    public void OnFinishQuest()
    {
        GameObject.Find("obj_player").GetComponent<scr_inventory>().quest.GetComponent<scr_quest>().RemoveList("Help the pregnant lady \nfind her husband.");
    }

    public void ResetLevel()
    {
        GameObject.Find("obj_player").GetComponent<scr_inventory>().quest.GetComponent<scr_quest>().RemoveList("Help the pregnant lady \nfind her husband.");

        wife.isEnabled = true;
        wife.gameObject.GetComponent<CapsuleCollider>().enabled = true;
        corpse.isEnabled = false;
        corpse.GetComponent<CapsuleCollider>().enabled = true;
        karak.transform.GetChild(0).GetComponent<Animator>().Play("State");
        karak.light.GetComponent<Light>().enabled = false;
        parang.gameObject.SetActive(true);
        parang.isEnabled = false;
    }

    public void OnPregSpeakDone()
    {
        wife.isEnabled = false;
        wife.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        GameObject.Find("obj_player").GetComponent<ThirdPersonController>().gamefinished = true;
    }
}
