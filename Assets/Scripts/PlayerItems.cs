using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public List<GameObject> items;
    public GameObject currentItem;
    public TextMeshProUGUI platePrompt;
    public bool holdingPlate;
    public bool canHoldMore;

    // Start is called before the first frame update
    void Start()
    {
        // Set all to inactive as player is not holding any items yet
        platePrompt.gameObject.SetActive(false);
        HoldingNone();
    }

    public void HoldingNone()
    {
        foreach (var item in items)
        {
            item.SetActive(false);
            holdingPlate = false;
            canHoldMore = true;
        }
    }

    public GameObject GetItemByName(string name)
    {
        foreach (var item in items)
        {
            if (item.name == name)
            {
                return item;
            }
        }
        Debug.Log("Item " + name + " is not found");
        return null;
    }

    public void HoldItem(string itemName)
    {
        Debug.Log("Attempting to hold: " + itemName);
        GameObject item = GetItemByName(itemName);

        if (item == null)
        {
            Debug.LogWarning("Item not found: " + itemName);
            return;
        }

        // Handle plate logic
        if (itemName == "Plate")
        {
            holdingPlate = true;
            Debug.Log("Plate picked up.");
        }

        // Check if the item is food
        if (item.CompareTag("Food"))
        {
            if (!holdingPlate)
            {
                Debug.Log("Cannot pick up food without a plate.");
                StartCoroutine(ShowPlatePrompt());
                return;
            }
            if (canHoldMore)
            {
                Debug.Log("Picking up a Food item with plate: " + itemName);
                canHoldMore = false;
            }

            // For Croissant: deactivate the plate, it has it's own
            if (itemName == "Croissant" && canHoldMore)
            {
                GameObject plate = GetItemByName("Plate");
                if (plate != null)
                {
                    plate.SetActive(false);
                    holdingPlate = false;
                    canHoldMore = false;
                }
            }
        }

        // Activate and set the item as the current held item
        if (!item.activeSelf)
        {
            item.SetActive(true);
        }
        currentItem = item;
    }

    IEnumerator ShowPlatePrompt()
    {
        platePrompt.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        platePrompt.gameObject.SetActive(false);
    }
}
