using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuy : MonoBehaviour
{
    bool buyAble = false;
    bool in_Range = false;
    [SerializeField] int Price;

    public void Update()
    {
        if (in_Range)
        {
            wasDoorBought();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            in_Range = true;
            if (Price == 100000)
            {
                gameManager.instance.finalDoormsg.SetActive(true);
                buyAble = gameManager.instance.playerScript.checkBalance(buyAble, Price);
            }
            else
            {
                gameManager.instance.WallBuyTxt.SetActive(true);
                buyAble = gameManager.instance.playerScript.checkBalance(buyAble, Price);
            }


        }
    }

    public void wasDoorBought()
    {
        if (Input.GetButton("Buy") && buyAble == true)
        {
            Destroy(gameObject);
            gameManager.instance.WallBuyTxt.SetActive(false);
            gameManager.instance.playerScript.CheckOut(Price);

            if (Price == 100000)
            {
                gameManager.instance.SurvivalWin();
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Price == 100000)
            {
                gameManager.instance.finalDoormsg.SetActive(false);
                buyAble = gameManager.instance.playerScript.checkBalance(buyAble, Price);
            }
            else
            {
                gameManager.instance.WallBuyTxt.SetActive(false);
                buyAble = gameManager.instance.playerScript.checkBalance(buyAble, Price);
            }
        }
    }
}
