using UnityEngine;
using Unity.Cinemachine;
using Fungus;

public class scr_preg : MonoBehaviour
{
    public GameObject light;
    public Animator karak;
    public scr_item parang;
    public scr_item corpse;
    public scr_item kill;
    public scr_billb wife;
    public GameObject pref_particles;


    public void PregAppear()
    {
        /*karak.Play("lookaround");*/
        light.GetComponent<Light>().enabled = true;
    }

    public void InvokePregnant()
    {
        karak.Play("karak_appear");
    }

    public void LookAtGhost()
    {
        CinemachineCamera cam = FindFirstObjectByType<CinemachineCamera>();
        cam.LookAt = karak.transform;

        
    }
    public void scenestarted()
    {
        GameObject.FindFirstObjectByType<ThirdPersonController>().isEnabled = false;
    }
    public void scenefinished()
    {
        GameObject.FindFirstObjectByType<ThirdPersonController>().isEnabled = true;
    }
    public void DontLook()
    {
        CinemachineCamera cam = FindFirstObjectByType<CinemachineCamera>();
        cam.LookAt = GameObject.Find("obj_player").transform;
        parang.isEnabled = true;
        corpse.isEnabled = false;
        
    }

    public void OnKarakKill()
    {
        GameObject.Find("obj_player").transform.GetChild(0).GetComponent<Animator>().Play("slash");
        GameObject.Find("obj_player").GetComponent<scr_inventory>().HoldItem(3);

        light.SetActive(false);
        karak.Play("die");
        Instantiate(pref_particles,this.transform.GetChild(0).transform.position, Quaternion.Euler(-90, 0, 0));
        kill.isEnabled = false;
        wife.isEnabled = true;
        wife.GetComponent<CapsuleCollider>().enabled = true;
        wife.GetComponent<Flowchart>().SetBooleanVariable("isDone", true);
        GameObject.Find("obj_player").GetComponent<scr_inventory>().quest.GetComponent<scr_quest>().RemoveList("Help the pregnant lady \nfind her husband.");
    }
}
