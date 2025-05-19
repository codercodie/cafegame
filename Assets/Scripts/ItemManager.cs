using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemManager : MonoBehaviour
{
    public Transform Player;
    public string itemName;
    public PlayerItems playerItems;

    void Start()
    {
        itemName = gameObject.name;
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
                            playerItems.HoldItem(itemName);
                        }
                    }

                }
            }

        }

    }


}
