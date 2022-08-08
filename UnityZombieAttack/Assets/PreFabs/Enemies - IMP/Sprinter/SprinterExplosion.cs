using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinterExplosion : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float SlowTimer;
    float DefaultSpeed;
    float slowEffect;
    // Update is called once per frame
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(SlowingExplode());
        }
        IEnumerator SlowingExplode()
        {
            // Apply Slow to player.
            gameManager.instance.playerScript.ToggleSlowOn();
          
            //Apply Damage
            if (other.GetComponent<iDamageable>() != null)
            {
                iDamageable isDamagable = other.GetComponent<iDamageable>();
                isDamagable.takeDamage(damage);
            }

            //How long you want Slow to last
            yield return new WaitForSeconds(SlowTimer);

            //Reset Player Speed
            gameManager.instance.playerScript.ToggleSlowOff();

        }
    }
}
