using UnityEngine;
using Unity.Cinemachine;
public class scr_preg : MonoBehaviour
{
    public GameObject light;
    public Animator karak;
    public scr_item parang;

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

    public void DontLook()
    {
        CinemachineCamera cam = FindFirstObjectByType<CinemachineCamera>();
        cam.LookAt = GameObject.Find("obj_player").transform;
        parang.isEnabled = true;
    }
}
