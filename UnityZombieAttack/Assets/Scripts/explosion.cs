using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{

    [SerializeField] int damage;


    public void OnTrigggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.playerScript.pushback = (gameManager.instance.player.transform.position - transform.position) * damage;

            if (other.GetComponent<iDamageable>() != null)
            {
                iDamageable isDamageable = other.GetComponent<iDamageable>();

                isDamageable.takeDamage(damage);
            }
        }
    }
}
