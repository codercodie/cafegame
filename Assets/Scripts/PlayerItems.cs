using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    public List<GameObject> items;
    GameObject currentItem;
    public TextMeshProUGUI platePrompt;
    public bool holdingPlate;

    // Start is called before the first frame update
    void Start()
    {
        // Set all to inactive as player is not holding any items yet
        foreach (var item in items)
        {
            item.SetActive(false);
            holdingPlate = false;
            platePrompt.gameObject.SetActive(false);
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

    public void holdItem(string itemName)
    {
        GameObject item = GetItemByName(itemName);
        if (itemName == "Plate")
        {
            holdingPlate = true;
        }
        if (item.tag == "Food")
        {
            if (!holdingPlate)
            {
                showPlatePrompt();
            }
        }
        if (itemName == "Crossiant" &&  holdingPlate)
        {
            GetItemByName("Plate").SetActive(false);
        }
        item.SetActive(true);
        currentItem = item;
    }

    IEnumerator showPlatePrompt()
    {
        platePrompt.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        platePrompt.gameObject.SetActive(false);
    }
}
