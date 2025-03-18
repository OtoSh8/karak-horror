using UnityEngine;

public class scr_levelthreetrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car")
        {
            GameObject.FindFirstObjectByType<ThirdPersonController>().img_.enabled = false;
            GameObject.FindFirstObjectByType<ThirdPersonController>().getout = true;
            this.GetComponent<InteractableFungusCharacter>().Interact();
        }
    }
}
