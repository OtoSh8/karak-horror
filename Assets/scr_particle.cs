using UnityEngine;

public class scr_particle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject,this.GetComponent<ParticleSystem>().main.duration + this.GetComponent<ParticleSystem>().startLifetime);
    }

}
