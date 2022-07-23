using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoPickUp : MonoBehaviour
{
    bool ammoCheck = false;
    //1 for placing on the map for kill and escape
    //2 for drops in survival

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
                //gameManager.instance.playerScript.ammoPickedUp(ammoCheck);
            }
            else
            {
                StartCoroutine(AmmoMessage());
                //gameManager.instance.playerScript.ammoPickedUp(ammoCheck);
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
