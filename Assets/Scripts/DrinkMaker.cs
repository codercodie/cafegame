using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrinkMaker : MonoBehaviour
{
    public GameObject emptyCup, fullCup;
    public Transform Player;
    public bool applianceEmpty, cupFull;
    public PlayerItems playerItems;

    // Start is called before the first frame update
    void Start()
    {
        emptyCup.SetActive(false);
        fullCup.SetActive(false);
        applianceEmpty = true;
        cupFull = false;
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
                            if (playerItems.currentItem.name == "EmptyCup" && applianceEmpty)
                            {
                                applianceEmpty = false;
                                pouring();
                                playerItems.HoldingNone();
                            }
                              
                        }
                    }

                }
            }

        }

    }

    IEnumerator pouring()
    {
        emptyCup.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        emptyCup.SetActive(false);
        fullCup.SetActive(true);
    }


}
