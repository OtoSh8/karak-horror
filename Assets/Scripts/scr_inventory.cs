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
    public Sprite itm_3;
    public Sprite itm_4;

    public GameObject par_held;
    public GameObject tip_vape;

    public GameObject quest;
    public GameObject par_bat;
    int bat = 0;
    public void OnPickupBattery()
    {
        bat = 4;
        for(int i = 0; i < par_bat.transform.childCount; i++)
        {
            if(i <= bat)
            {
                par_bat.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                par_bat.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public void OnUseVape()
    {
        bat--;

        for (int i = 0; i < par_bat.transform.childCount; i++)
        {
            if (i <= bat)
            {
                par_bat.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                par_bat.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
        HoldItem(1);
        this.transform.GetChild(0).GetComponent<Animator>().Play("drink");
        this.GetComponent<scr_react>().CalmDown();
    }
    public void PickupItem(int x)
    {
        if (inv_data.Contains(x)) return;
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
        
        HoldItem(sel_slot);



        for (var i = 0; i < inv_slots.Length; i++)
        {
            if (inv_slots[i] == sel_slot && inv_slots[i] != 0)
            {
                par_hotbar.transform.GetChild(i).GetComponent<Image>().color = Color.green;
            }
            else
            {
                par_hotbar.transform.GetChild(i).GetComponent<Image>().color = Color.white;
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
                par_inv.transform.GetChild(i).GetComponent<Image>().color = Color.white;
            }
        }

        }

        

    }

    public void HoldItem(int y)
    {
        for(int i = 0; i < par_held.transform.childCount; i++)
        {
            if(y-1 == i)
            {
                par_held.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                par_held.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            par_inv.transform.parent.gameObject.SetActive(!par_inv.transform.parent.gameObject.activeInHierarchy);

            if (par_inv.transform.parent.gameObject.activeInHierarchy)
            {
                GameObject.Find("FreeLook Camera").GetComponent<CinemachineInputAxisController>().enabled = false;
                Cursor.lockState = CursorLockMode.None;
            }
            else if(!par_inv.transform.parent.gameObject.activeInHierarchy)
            {
                Cursor.lockState = CursorLockMode.Locked;
                GameObject.Find("FreeLook Camera").GetComponent<CinemachineInputAxisController>().enabled = true;
            }
        }

        if (par_inv.transform.parent.gameObject.activeInHierarchy & Input.GetKeyDown(KeyCode.Escape))
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
        if(sel_slot == 1)
        {
            tip_vape.SetActive(true);
        }
        else
        {
            tip_vape.SetActive(false);
        }
        if (Input.GetMouseButtonDown(1) && this.gameObject.GetComponent<ThirdPersonController>().inCar == false && bat > 0)
        {
            OnUseVape();
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
            case 3:
                name = "files";
                break;
            case 4:
                name = "wallet";
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
            case 3:
                img = itm_3;
                break;
            case 4:
                img = itm_4;
                break;
        }

        return img;

    }

    public void SelectSlot(int x)
    {


        if (inv_data[x] != 0)
        {
            HoldItem(inv_data[x]);


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
                        par_hotbar.transform.GetChild(i).GetComponent<Image>().color = Color.white;
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
                        par_hotbar.transform.GetChild(i).GetComponent<Image>().color = Color.white;
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
                    par_inv.transform.GetChild(i).GetComponent<Image>().color = Color.white;
                }
            }
        }

        RefreshInv();
    }
}
