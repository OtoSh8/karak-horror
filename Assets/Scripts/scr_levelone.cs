using System.Collections.Generic;
using UnityEngine;

public class scr_levelone : MonoBehaviour
{
    public bool isInteracted = false;
    public List<GameObject> interactables = new List<GameObject>();

    [SerializeField] GameObject placeholder;

    void Start()
    {
        if (GameObject.Find("model_player") != null) { 
        GameObject.Find("model_player").transform.position = new Vector3(placeholder.transform.position.x, GameObject.Find("model_player").transform.position.y, placeholder.transform.position.z);
            GameObject.Find("obj_controller").GetComponent<scr_controller>().crnt_level1_obj = this.gameObject;
        Destroy(placeholder);
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
