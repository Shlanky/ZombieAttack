using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulHit : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] int speed;
    [SerializeField] Rigidbody rb;
    [SerializeField] float destroyTime;
    [SerializeField] GameObject hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = (gameManager.instance.player.transform.position - transform.position).normalized * speed;

        Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<iDamageable>() != null)
        {
            iDamageable isDamagable = other.GetComponent<iDamageable>();
            isDamagable.takeDamage(damage);
        }
        Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
        Destroy(gameObject);
    }
}