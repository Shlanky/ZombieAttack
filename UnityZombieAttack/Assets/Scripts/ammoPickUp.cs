using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickUp : MonoBehaviour
{
    bool ammoCheck = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ammoCheck = gameManager.instance.playerScript.checkAmmo(ammoCheck);

            if (ammoCheck == true)
            {
                Destroy(gameObject);
                gameManager.instance.playerScript.giveAmmo(30);
                gameManager.instance.pickUpAmmo();
            }
            else
            {
                StartCoroutine(AmmoMessage());
            }
        }

    }

    IEnumerator AmmoMessage()
    {
        gameManager.instance.ammoMAxed.SetActive(true);
        yield return new WaitForSeconds(1f);
        gameManager.instance.ammoMAxed.SetActive(false);

    }
}
