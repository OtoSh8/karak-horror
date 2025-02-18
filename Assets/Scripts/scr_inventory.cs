using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Unity.Cinemachine;
using TMPro;
public class scr_inventory : MonoBehaviour
{
    int[] inv_data = { 0, 0, 0, 0 , 0, 0, 0, 0 };
    int[] inv_slots = { 0,0,0,0};
    private int inv_size = 8;

    private int sel_slot = 0;

    [Header("Camera Settings")]
    public GameObject par_hotbar;
    public GameObject par_inv;


    [Header("Item Data")]
    public TextMeshProUGUI txtitem;
    public Sprite itm_1;
    public Sprite itm_2;

    public void PickupItem(int x)
    {
        int empty = 999;

        for (var i = 0; i < inv_size; i++)
        {
            if (inv_data[i] == 0)
            {
                empty = i;
                Debug.Log("slot found: " + i);
                inv_data[i] = x;
                RefreshInv();
                Debug.Log(x);
                return;
            }
        }

        RefreshInv();
    }

    public void RefreshInv()
    {

            for (var i = 0; i < inv_size; i++)
            {
            if(inv_data[i] != 0)
            {
                /*Debug.Log(inv_data[i] +" "+ i);*/
                par_inv.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = GetItemImg(inv_data[i]);
                par_inv.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                par_inv.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = null;
                par_inv.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
            }

        for (var i = 0; i < inv_slots.Length; i++)
        {
            if (inv_slots[i] != 0)
            {
                par_hotbar.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = GetItemImg(inv_slots[i]);
                par_hotbar.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                par_hotbar.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = null;
                par_hotbar.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }


        }
        txtitem.text = GetItem(sel_slot);
    }

    private void OnScroll(bool up)
    {
        int crntslot = sel_slot;
        for (var i = 0; i < inv_slots.Length; i++)
        {
            if (inv_slots[i] == sel_slot)
            {
                crntslot = i;
            }
        }

        if(sel_slot != 0)
        {
            if (up && crntslot < 3 && inv_slots[crntslot + 1] != 0)
            {
                sel_slot = inv_slots[crntslot + 1];
            }
            else if (!up && crntslot > 0 && inv_slots[crntslot - 1] != 0)
            {
                sel_slot = inv_slots[crntslot - 1];
            }
        
        



        for (var i = 0; i < inv_slots.Length; i++)
        {
            if (inv_slots[i] == sel_slot && inv_slots[i] != 0)
            {
                par_hotbar.transform.GetChild(i).GetComponent<Image>().color = Color.green;
            }
            else
            {
                par_hotbar.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
            }
        }

        for (var i = 0; i < inv_size; i++)
        {
            if (inv_data[i] == sel_slot && inv_data[i] != 0)
            {
                par_inv.transform.GetChild(i).GetComponent<Image>().color = Color.green;
            }
            else if (inv_data[i] != sel_slot && inv_data[i] != 0)
            {
                par_inv.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
            }
        }

        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            par_inv.SetActive(!par_inv.activeInHierarchy);
            if (par_inv.activeInHierarchy)
            {
                GameObject.Find("FreeLook Camera").GetComponent<CinemachineInputAxisController>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
            else if(!par_inv.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.Locked;
                GameObject.Find("FreeLook Camera").GetComponent<CinemachineInputAxisController>().enabled = true;
            }
        }

        if (par_inv.activeInHierarchy & Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            GameObject.Find("FreeLook Camera").GetComponent<CinemachineInputAxisController>().enabled = false;
        }


        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            OnScroll(false);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            OnScroll(true);
        }
    }
    private void Start()
    {
/*        Cursor.lockState = CursorLockMode.Locked;
*/
        for (var i = 0; i < 4; i++)
        {
            inv_slots[i] = 0;
        }
    }

    private string GetItem(int x)
    {
        string name = "";


        switch (x)
        {
            case 0:
                name = "";
                break;
            case 1:
                name = "vape";
                break;
            case 2:
                name = "ring";
                break;
        }

        return name;

    }

    private Sprite GetItemImg(int x)
    {
        Sprite img = null;

        switch (x)
        {
            case 0:
                break;
            case 1:
                img = itm_1;
                break;
            case 2:
                img = itm_2;
                break;
        }

        return img;

    }

    public void SelectSlot(int x)
    {


        if (inv_data[x] != 0)
        {
            


            //already exists in hotbar
            if (inv_slots.Contains(inv_data[x]))
            {
                sel_slot = inv_data[x];


                for (var i = 0; i < inv_slots.Length; i++)
                {
                    if (inv_slots[i] == inv_data[x])
                    {
                        par_hotbar.transform.GetChild(i).GetComponent<Image>().color = Color.green;
                    }
                    else
                    {
                        par_hotbar.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
                    }
                }

            }
            else if (!inv_slots.Contains(inv_data[x]))
            {
                Debug.Log(inv_data[x]);
                sel_slot = inv_data[x];

                inv_slots[3] = inv_slots[2];
                inv_slots[2] = inv_slots[1];
                inv_slots[1] = inv_slots[0];
                inv_slots[0] = inv_data[x];

                for (var i = 0; i < inv_slots.Length; i++)
                {
                    if (inv_slots[i] == sel_slot)
                    {
                        par_hotbar.transform.GetChild(i).GetComponent<Image>().color = Color.green;
                    }
                    else
                    {
                        par_hotbar.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
                    }
                }
            }



            for (var i = 0; i < inv_size; i++)
            {
                if (inv_data[i] == sel_slot)
                {
                    par_inv.transform.GetChild(i).GetComponent<Image>().color = Color.green;
                }
                else if (inv_data[i] != sel_slot)
                {
                    par_inv.transform.GetChild(i).GetComponent<Image>().color = Color.gray;
                }
            }
        }

        RefreshInv();
    }
}
