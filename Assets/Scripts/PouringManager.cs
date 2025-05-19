using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PouringManager : MonoBehaviour
{
    public Transform Player;
    public string itemName;
    public PlayerItems playerItems;
    public Animator mochaAnim, teaAnim;

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
                            Debug.Log("Pouring");
                            if (playerItems.emptyCupOnCounter && playerItems.holdingMochaPot) {
                                StartCoroutine(pourMocha());
                                return;
                            }
                            if (playerItems.emptyCupOnCounter && playerItems.holdingTeaPot)
                            {
                                StartCoroutine(pourTea());
                                return;
                            }
                        }
                    }

                }
            }

        }

    }

    IEnumerator pourMocha()
    {
        Debug.Log("Pouring Mocha");
        mochaAnim.Play("pourmocha");
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Mocha Poured");
        playerItems.emptyCup.SetActive(false);
        playerItems.HoldingNone();
        playerItems.holdingMochaPot = false;
        playerItems.MochaPot.SetActive(true);
        playerItems.emptyCupOnCounter = false;
        playerItems.coffeeCup.SetActive(true);
        playerItems.canHoldMore = true;
    }

    IEnumerator pourTea()
    {
        Debug.Log("Pouring Tea");
        mochaAnim.Play("pourtea");
        yield return new WaitForSeconds(2.5f);
        Debug.Log("Teaa Poured");
        playerItems.emptyCup.SetActive(false);
        playerItems.HoldingNone();
        playerItems.holdingTeaPot = false;
        playerItems.TeaPot.SetActive(true);
        playerItems.emptyCupOnCounter = false;
        playerItems.teaCup.SetActive(true);
        playerItems.canHoldMore = true;
    }


}
