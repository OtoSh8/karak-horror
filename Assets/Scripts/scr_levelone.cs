using System.Collections.Generic;
using UnityEngine;

public class scr_levelone : MonoBehaviour
{
    public bool isInteracted = false;
    public List<GameObject> interactables = new List<GameObject>();

    void Start()
    {
        if (GameObject.Find("model_player") != null) { 
        GameObject.Find("model_player").transform.position = new Vector3(this.transform.GetChild(0).transform.position.x, GameObject.Find("model_player").transform.position.y, this.transform.GetChild(0).transform.position.z);
            GameObject.Find("obj_controller").GetComponent<scr_controller>().crnt_level1_obj = this.gameObject;
        Destroy(this.transform.GetChild(0).gameObject);
    }


        

    }
    public void Interacted()
    {
        foreach (GameObject interactable in interactables)
        {
            interactable.GetComponent<scr_item>().isEnabled = true;
        }
    }

}
