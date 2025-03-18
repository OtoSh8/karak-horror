using UnityEngine;
using UnityEngine.UI;

public class scr_item : MonoBehaviour
{
    public float range;
    public bool isEnabled = false;
    public GameObject pref_bil;
    private GameObject bil;
    public Vector3 offset;
    void Start()
    {
        bil = Instantiate(pref_bil,GameObject.Find("par_billboard").transform);
        bil.GetComponent<scr_billboard>().targetObject = this.transform;
        bil.GetComponent<scr_billboard>().offset = offset;
    }

    private void Update()
    {
        if (isEnabled)
        {
            float dis = Vector3.Distance(this.transform.position, GameObject.Find("obj_player").transform.position);

            if (dis < range)
            {
                bil.GetComponent<Image>().enabled = true;
            }
            else
            {
                bil.GetComponent<Image>().enabled = false;
            }
        }
        else
        {
            bil.GetComponent<Image>().enabled = false;
        }
        
    }
    private void OnDisable()
    {
        if(bil.GetComponent<Image>() != null)
        {
            bil.GetComponent<Image>().enabled = false;

        }
    }

    private void OnDestroy()
    {
        if(bil != null)
        {
            Destroy(bil);

        }
    }

}
