using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyFinding : MonoBehaviour
{
    [Header("--------Audio----------")]
    public AudioSource aud;

    //gun shot/Famas
    [SerializeField] AudioClip[] RoundStartingSound;
    [Range(0, 1)] [SerializeField] float volume;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            gameManager.instance.playerScript.giveKey(1);
        }

    }

}
