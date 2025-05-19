using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FridgeManager : MonoBehaviour
{
    public Animator fridgeopenclose1, fridgeopenclose2;
    public bool open;
    public Transform Player;

    void Start()
    {
        open = false;
    }
    void OnMouseOver()
    {
        {
            if (Player)
            {
                float dist = Vector3.Distance(Player.position, transform.position);
                if (dist < 15)
                {
                    if (open == false)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            StartCoroutine(opening());
                        }
                    }
                    else
                    {
                        if (open == true)
                        {
                            if (Input.GetMouseButtonDown(0))
                            {
                                StartCoroutine(closing());
                            }
                        }

                    }

                }
            }

        }

    }

    IEnumerator opening()
    {
        fridgeopenclose1.Play("open");
        fridgeopenclose2.Play("open2");
        Debug.Log("Opening Fridge");
        open = true;
        yield return new WaitForSeconds(.5f);
    }

    IEnumerator closing()
    {
        fridgeopenclose1.Play("close");
        fridgeopenclose2.Play("close2");
        Debug.Log("Closing Door");
        open = false;
        yield return new WaitForSeconds(.5f);
    }


}
