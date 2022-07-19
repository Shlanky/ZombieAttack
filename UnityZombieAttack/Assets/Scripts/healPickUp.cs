using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healPickUp : MonoBehaviour
{
    bool hpCheck = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
          hpCheck = gameManager.instance.playerScript.checkHP(hpCheck);

            if (hpCheck == true)
            {
                Destroy(gameObject);
                gameManager.instance.playerScript.giveHP(5);
            }
            else
            {
                StartCoroutine(HealsMessage());
            }
        }
    }

    IEnumerator HealsMessage()
    {
        gameManager.instance.healMaxedMSG.SetActive(true);
        yield return new WaitForSeconds(2);
        gameManager.instance.healMaxedMSG.SetActive(false);
    }
}
