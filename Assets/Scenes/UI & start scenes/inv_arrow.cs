using UnityEngine;
using UnityEngine.UI;

public class InventoryPageSwitcher : MonoBehaviour
{
    public GameObject obj_inv;  // Assign Inventory Panel
    public GameObject quest;      // Assign Quest Panel
    public Button btn_arrow;         // Assign Arrow Button
    private bool isOnQuestPage = false;

    void Start()
    {
        // Ensure inventory starts visible, quest hidden
        obj_inv.SetActive(true);
        quest.SetActive(false);

        // Add listener to button
        btn_arrow.onClick.AddListener(TogglePage);
    }

    void TogglePage()
    {
        isOnQuestPage = !isOnQuestPage;

        // Toggle visibility of the panels
        obj_inv.SetActive(!isOnQuestPage);
        quest.SetActive(isOnQuestPage);

        // Move the button to the new position
        btn_arrow.transform.localPosition = isOnQuestPage ? new Vector3(-987, -84, 0) : new Vector3(24, -84, 0);


        // Optionally, flip the arrow direction
        btn_arrow.transform.Rotate(0f, 0f, 180f);
    }
}

