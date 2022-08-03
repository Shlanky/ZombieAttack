using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckEscape : MonoBehaviour
{
    bool key_check = false;
    int mazeNumComp;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            key_check = gameManager.instance.playerScript.checkKey(key_check);

            if (key_check == true)
            {
                mazeNumComp++;
                gameManager.instance.checkKeysForWin(mazeNumComp);
            }
            else
            {
                StartCoroutine(keyMessages());
            }
        }
    }

    IEnumerator keyMessages()
    {
        int keycount = 0;
        keycount = gameManager.instance.playerScript.keysFound;
        if (keycount == 0)
        {
            //you dont have a key to try on the car 
            gameManager.instance.noKeys.SetActive(true);
            yield return new WaitForSeconds(2);
            gameManager.instance.noKeys.SetActive(false);
        }
        else if (keycount == 1)
        {
            //the key doesnt fit the car
            gameManager.instance.Key_1.SetActive(true);
            yield return new WaitForSeconds(2);
            gameManager.instance.Key_1.SetActive(false);
        }
        else if (keycount == 2)
        {
            //the key is broken
            gameManager.instance.Key_2.SetActive(true);
            yield return new WaitForSeconds(2);
            gameManager.instance.Key_2.SetActive(false);
        }
    }
}
