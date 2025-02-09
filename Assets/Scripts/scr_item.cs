using UnityEngine;
using UnityEngine.UI;

public class scr_item : MonoBehaviour
{
    public float range;
    public string imagename;

    void Start()
    {
        GameObject.Find(imagename).GetComponent<scr_billboard>().targetObject = this.transform;
    }

    private void Update()
    {
        float dis = Vector3.Distance(this.transform.position, GameObject.Find("obj_player").transform.position);
        if (dis < range)
        {
            GameObject.Find(imagename).GetComponent<Image>().enabled = true;
        }
        else
        {
            GameObject.Find(imagename).GetComponent<Image>().enabled = false;
        }
    }

    private void OnDestroy()
    {
        Destroy(GameObject.Find(imagename));
    }

}
