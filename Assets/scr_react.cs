using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using URPGlitch;

public class scr_react : MonoBehaviour
{
    public int threshold = 50;
    private bool isNearby = false;

    private bool Triggered = false;

    [SerializeField] scr_bpm bpm;
    [SerializeField] Volume vol;
    AnalogGlitchVolume anavol;
    private void Start()
    {
        vol.profile.TryGet(out anavol);
    }
    // Update is called once per frame
    void Update()
    {
        if (IsNear() && isNearby == false)
        {
            //SPIRIT IS NEARBY
            if(Triggered == false)
            {
                StartCoroutine(Anxious());
                Triggered = true;
            }
            isNearby = true;
        }
        else if (isNearby == true)
        {
            //SPIRIT NOT NEAR ANYMORE
            Triggered = false;
        }
    }

    private bool IsNear()
    {
        
        GameObject[] allspirits = GameObject.FindGameObjectsWithTag("Ghost");
        foreach (GameObject obj in allspirits)
        {
            if (Vector3.Distance(obj.transform.position, this.transform.position) <= threshold && !isNearby)
            {
                return true;
            }
        }

        return false;
    }


    IEnumerator Anxious()
    {
        // SET BPM, START CAMERA GLITCH, START OVERLAY ANIMATION
        bpm.bpm = 90;
        
        anavol.scanLineJitter.Override(0.5f);
        anavol.colorDrift.Override(0.3f);

        yield return new WaitForSeconds(5f);

        CalmDown();
        yield return null;

    }

    private void CalmDown()
    {
        bpm.bpm = 60;
        anavol.scanLineJitter.Override(0f);
        anavol.colorDrift.Override(0f);

    }
}
