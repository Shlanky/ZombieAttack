using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickUp : MonoBehaviour
{
    //use this for the weapon wall buy
    [SerializeField] gunStats gunStats;
    bool in_Range = false;
    public int gunNum;

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

            //Ak 47
            if (gunNum == 1)
            {
                gameManager.instance.AK47_msg.SetActive(true);
            }

            //FaMas
            if (gunNum == 2)
            {
                gameManager.instance.FaMas_msg.SetActive(true);
            }

            //Ghost
            if (gunNum == 3)
            {
                gameManager.instance.Ghost_msg.SetActive(true);
            }

            //M16
            if (gunNum == 4)
            {
                gameManager.instance.M16_msg.SetActive(true);
            }

            //m1911
            if (gunNum == 5)
            {
                gameManager.instance.M1911_msg.SetActive(true);
            }

            //MP5
            if (gunNum == 6)
            {
                gameManager.instance.MP5_msg.SetActive(true);
            }

            //Revolver
            if (gunNum == 7)
            {
                gameManager.instance.Revolver_msg.SetActive(true);
            }

            //Uzi
            if (gunNum == 8)
            {
                gameManager.instance.Uzi_msg.SetActive(true);
            }
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
            if (gunNum == 1)
            {
                gameManager.instance.AK47_msg.SetActive(false);
            }

            //FaMas
            if (gunNum == 2)
            {
                gameManager.instance.FaMas_msg.SetActive(false);
            }

            //Ghost
            if (gunNum == 3)
            {
                gameManager.instance.Ghost_msg.SetActive(false);
            }

            //M16
            if (gunNum == 4)
            {
                gameManager.instance.M16_msg.SetActive(false);
            }

            //m1911
            if (gunNum == 5)
            {
                gameManager.instance.M1911_msg.SetActive(false);
            }

            //MP5
            if (gunNum == 6)
            {
                gameManager.instance.MP5_msg.SetActive(false);
            }

            //Revolver
            if (gunNum == 7)
            {
                gameManager.instance.Revolver_msg.SetActive(false);
            }

            //Uzi
            if (gunNum == 8)
            {
                gameManager.instance.Uzi_msg.SetActive(false);
            }
        }
    }
}
