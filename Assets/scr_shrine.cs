using UnityEngine;

public class scr_shrine : MonoBehaviour
{
    [SerializeField] GameObject particle_free;

    public void OnShrineFinish()
    {
        Instantiate(particle_free, this.transform.position,Quaternion.Euler(-90,0,0));
        GameObject.Find("obj_controller").GetComponent<scr_controller>().isEscaped = true;
        GameObject.Find("obj_player").GetComponent<scr_inventory>().quest.GetComponent<scr_quest>().RemoveList("Set the beetle car driver free.");
    }

}
