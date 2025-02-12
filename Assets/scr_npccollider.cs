using UnityEngine;

public class scr_npccollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Car")  || other.CompareTag("Player"))
        {
            this.GetComponentInParent<scr_followroute>().OnColl();
            Debug.Log("ENTEREDl");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car") || other.CompareTag("Player"))
        {
            this.GetComponentInParent<scr_followroute>().UnColl();
            Destroy(this.transform.parent.gameObject);
        }
    }
}
