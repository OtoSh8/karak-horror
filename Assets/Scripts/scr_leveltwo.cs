using System.Collections.Generic;
using UnityEngine;

public class scr_leveltwo : MonoBehaviour
{
    public List<GameObject> listitems = new List<GameObject>();
    public void activateitems()
    {
        foreach(GameObject item in listitems)
        {
            item.GetComponent<scr_item>().isEnabled = true;
        }
    }

}
