using UnityEngine;
using UnityEngine.UI;

public class scr_billb : MonoBehaviour
{
    public bool isEnabled = false;
    public float range;
    public Vector3 offs;

    private GameObject billboard;
    [SerializeField] GameObject prefbill;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        billboard = Instantiate(prefbill,GameObject.Find("par_billboard").transform);
        billboard.GetComponent<scr_billboard>().targetObject = this.transform;
        billboard.GetComponent<scr_billboard>().offset = offs;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            float dis = Vector3.Distance(this.transform.position, GameObject.Find("obj_player").transform.position);

            if (dis < range)
            {
                billboard.SetActive(true);
            }
            else
            {
                billboard.SetActive(false);
            }
        }
        else
        {
            billboard.SetActive(false);
        }
    }
}
