using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunPickUp : MonoBehaviour
{
    [SerializeField] gunStats gunStats;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.gunPickUp(gunStats.fireRate, gunStats.damage, gunStats.magSize, gunStats.resSize, gunStats.muzzleFlash, gunStats.gunModel, gunStats);

            //mayeb leave this out for wall buys for survical 
            Destroy(gameObject);
        }
    }
}
