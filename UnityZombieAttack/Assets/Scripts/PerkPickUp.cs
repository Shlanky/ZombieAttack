using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerkPickUp : MonoBehaviour
{
    [SerializeField] Perks perk;

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
            Price = perk.Price;
            in_Range = true;

            if (gameObject.CompareTag("TANK"))
            {
                gameManager.instance.TankTxt.SetActive(true);
                buyAble = gameManager.instance.playerScript.checkBalance(buyAble, Price);
            }
            else if (gameObject.CompareTag("JUMP PERK"))
            {
                gameManager.instance.JumpTxt.SetActive(true);
                buyAble = gameManager.instance.playerScript.checkBalance(buyAble, Price);
            }
            else if (gameObject.CompareTag("DAMAGE PERK"))
            {
                gameManager.instance.DamageTxt.SetActive(true);
                buyAble = gameManager.instance.playerScript.checkBalance(buyAble, Price);
            }
        }
    }

    public void wasDoorBought()
    {
        if (Input.GetButton("Buy") && buyAble == true)
        {
            Destroy(gameObject);
            if (gameObject.CompareTag("TANK"))
            {
                gameManager.instance.TankTxt.SetActive(false);
                gameManager.instance.playerScript.Perks(perk.Price, perk.Tank, perk.damage, perk.jumps);

            }
            else if (gameObject.CompareTag("JUMP PERK"))
            {
                gameManager.instance.JumpTxt.SetActive(false);
                gameManager.instance.playerScript.Perks(perk.Price, perk.Tank, perk.damage, perk.jumps);
            }
            else if (gameObject.CompareTag("DAMAGE PERK"))
            {
                gameManager.instance.DamageTxt.SetActive(false);
                gameManager.instance.playerScript.Perks(perk.Price, perk.Tank, perk.damage, perk.jumps);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            in_Range = false;
            if (gameObject.CompareTag("TANK"))
            {
                gameManager.instance.TankTxt.SetActive(false);
            }
            else if (gameObject.CompareTag("JUMP PERK"))
            {
                gameManager.instance.JumpTxt.SetActive(false);
            }
            else if (gameObject.CompareTag("DAMAGE PERK"))
            {
                gameManager.instance.DamageTxt.SetActive(false);
            }
        }
    }
}
