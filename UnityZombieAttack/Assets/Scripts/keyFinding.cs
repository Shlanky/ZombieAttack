using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyFinding : MonoBehaviour
{

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.instance.playerScript.giveKey(1);
        }

    }
}
