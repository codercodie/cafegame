using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public List<GameObject> items;
    public GameObject currentItem;
    public TextMeshProUGUI prompt;
    public bool holdingPlate;
    public bool canHoldMore;
    public bool holdingMochaPot, holdingTeaPot, holdingEmptyCup;
    public GameObject MochaPot, TeaPot, emptyCup, teaCup, coffeeCup, cupboardInteraction;
    public bool emptyCupOnCounter;

    // Start is called before the first frame update
    void Start()
    {
        // Set all to inactive as player is not holding any items yet
        prompt.gameObject.SetActive(false);
        HoldingNone();
        emptyCupOnCounter = false;
        holdingEmptyCup = false;
        holdingMochaPot = false;
        holdingTeaPot = false;
        emptyCup.SetActive(false);
        teaCup.SetActive(false);
        coffeeCup.SetActive(false);
    }

    public void HoldingNone()
    {
        foreach (var item in items)
        {
            item.SetActive(false);
            holdingPlate = false;
            holdingMochaPot = false;
            holdingTeaPot = false;
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

    public void EmptyCupPlaced()
    {
        emptyCup.SetActive(true);
        emptyCupOnCounter = true;
        holdingEmptyCup = false;
        cupboardInteraction.SetActive(false);
        HoldingNone();
    }

    public void HoldItem(string itemName)
    {
        Debug.Log("Attempting to hold: " + itemName);
        if (itemName == "CupboardInteraction" && holdingEmptyCup)
        {
            EmptyCupPlaced();

            return;
        }

        GameObject item = GetItemByName(itemName);

        if (item == null)
        {
            Debug.LogWarning("Item not found: " + itemName);
            return;
        }

        if (itemName == "MochaPot")
        {
            if (emptyCupOnCounter && canHoldMore)
            {
                holdingMochaPot = true;
                canHoldMore = false;
                MochaPot.SetActive(false);
                Debug.Log("Mocha:" + holdingMochaPot);
                Debug.Log("EmptyCup: " + emptyCupOnCounter);
            }
            else
            {
                prompt.gameObject.SetActive(true);
                prompt.text = "I should place an empty cup on the counter first.";
                return;
            }
        }

        if (itemName == "TeaPot")
        {
            if (emptyCupOnCounter && canHoldMore)
            {
                holdingTeaPot = true;
                canHoldMore = false;
                TeaPot.SetActive(false);
            }
            else
            {
                prompt.gameObject.SetActive(true);
                prompt.text = "I should place an empty cup on the counter first.";
                return;
            }
        }

        if (itemName == "CupEmpty" && canHoldMore)
        {
            Debug.Log("CanHoldMore:" + canHoldMore);
            holdingEmptyCup = true;
            Debug.Log("Holding Empty Cup: " + holdingEmptyCup);
            canHoldMore = false;
        }

        if (itemName == "CupEmpty" && currentItem.name == "CupEmpty")
        {
            HoldingNone();
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
        prompt.gameObject.SetActive(true);
        prompt.text = "I should probbaly get a plate before handling food";
        yield return new WaitForSeconds(2.5f);
        prompt.gameObject.SetActive(false);
    }
}
