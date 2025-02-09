using UnityEngine;

public class scr_levelone : MonoBehaviour
{
    void Start()
    {
        if (GameObject.Find("model_player") != null) { 
        GameObject.Find("model_player").transform.position = new Vector3(this.transform.GetChild(0).transform.position.x, GameObject.Find("model_player").transform.position.y, this.transform.GetChild(0).transform.position.z);
            GameObject.Find("obj_controller").GetComponent<scr_controller>().crnt_level1_obj = this.gameObject;
        Destroy(this.transform.GetChild(0).gameObject);
    }
    }

}
