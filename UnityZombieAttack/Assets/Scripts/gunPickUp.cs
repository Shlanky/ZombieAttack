using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickUp : MonoBehaviour
{
    //use this for the weapon wall buy
    [SerializeField] gunStats gunStats;
    bool in_Range = false;


    public void Update()
    {
        if (in_Range)
        {
            BuyGun();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            in_Range = true;
            //display teh info about gun
        }
    }

    public void BuyGun()
    {
        if (Input.GetButtonDown("Buy") && in_Range == true)
        {
            gameManager.instance.playerScript.gunPickUp(gunStats.priceOfGun, gunStats.fireRate, gunStats.damage, gunStats.magSize, gunStats.resSize, gunStats.muzzleFlash, gunStats.gunModel, gunStats);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            in_Range = false;
            //turn off the buy message
        }
    }
}
