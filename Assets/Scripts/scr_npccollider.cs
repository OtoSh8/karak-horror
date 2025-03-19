using UnityEngine;

public class scr_npccollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") || other.CompareTag("Player"))
        {
            //Collided
            GameObject.FindFirstObjectByType<scr_death>().Death(2);
            GameObject.FindFirstObjectByType<scr_followroute>().gameObject.GetComponent<AudioSource>().Play();
            Destroy(this.transform.parent.gameObject);
        }
    }

}
