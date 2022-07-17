using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckEscape : MonoBehaviour
{
    bool key_check = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            key_check = gameManager.instance.playerScript.checkKey(key_check);

            if (key_check == true)
            {
                gameManager.instance.checkKeysForWin(key_check);
            }
            else
            {
                StartCoroutine(keyMessages());
            }
        }
    }

    IEnumerator keyMessages()
    {
        gameManager.instance.notEnoughKeys.SetActive(true);
        yield return new WaitForSeconds(2);
        gameManager.instance.notEnoughKeys.SetActive(false);
    }
}
