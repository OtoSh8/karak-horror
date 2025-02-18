using UnityEngine;

public class scr_npccollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Car") || other.CompareTag("Player"))
        {
            Destroy(this.transform.parent.gameObject);
            //Collided
        }
    }

}
