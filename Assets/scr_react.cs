using Unity.VisualScripting;
using UnityEngine;

public class scr_react : MonoBehaviour
{
    public int threshold = 50;
    private bool isNearby = false;
    // Update is called once per frame
    void Update()
    {
        if (IsNear() && isNearby == false)
        {
            //SPIRIT IS NEARBY
            Anxious();
            isNearby = true;
        }
        else if(isNearby == true)
        {
            //SPIRIT NOT NEAR ANYMORE
            CalmDown();
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

    private void Anxious()
    {

    }

    private void CalmDown()
    {

    }
}
