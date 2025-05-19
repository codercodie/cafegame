using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CounterItems : MonoBehaviour
{
    public Transform Player;
    public List<GameObject> items;
    public GameObject plate;
    public PlayerItems playerItems;
    public TextMeshProUGUI prompt;
    public bool foodPlaced, drinkPlaced;

    // Start is called before the first frame update
    void Start()
    {
        // Set all to inactive as player is not holding any items yet
        foreach (var item in items)
        {
            item.SetActive(false);
        }
        plate.SetActive(false);
        prompt.gameObject.SetActive(false);
        foodPlaced = false;
        drinkPlaced = false;

    }

    void OnMouseOver()
    {
        {
            if (Player)
            {
                float dist = Vector3.Distance(Player.position, transform.position);
                if (dist < 15)
                {
                    if (Player)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            PlaceItemsOnCounter(); 
                        }
                    }

                }
            }

        }

    }


    public void PlaceItemsOnCounter()
    {
        if (playerItems.currentItem.name == "Plate")
        {
            prompt.gameObject.SetActive(true);
            prompt.text = "I should probably put a food item on the plate before giving it to the customer";
            return;
        }
        if (playerItems.currentItem.name == "CupEmpty")
        {
            prompt.gameObject.SetActive(true);
            prompt.text = "I should probably fill the cup before giving it to the customer";
            return;
        }
        PlaceItemByName(playerItems.currentItem.name);
        playerItems.HoldingNone();
    }

    public void PlaceItemByName(string name)
    {
        foreach (var item in items)
        {
            if (item.name == name)
            {
                item.SetActive(true);
                if (item.tag == "Food")
                {
                    if (foodPlaced)
                    {
                        prompt.gameObject.SetActive(true);
                        prompt.text = "There is no space to put any more food on this tray.";
                        return;
                    }
                    if (item.name != "Croissant")
                    {
                        plate.SetActive(true);
                        return;
                    }
                    foodPlaced = true;
                    return;
                }
                if (item.tag == "Drink")
                {
                    if (drinkPlaced)
                    {
                        prompt.gameObject.SetActive(true);
                        prompt.text = "There is no space to put any more drinks on this tray.";
                        return;
                    }
                    drinkPlaced = true;
                    return;
                }
            }
        }

        Debug.Log("Item " + name + " is not found");

    }



}
