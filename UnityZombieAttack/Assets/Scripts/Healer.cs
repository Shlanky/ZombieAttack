using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    void Update()
    {
        //need to slow this down but it works
        //gameObject.transform.localRotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        StartCoroutine(lifeTimer());
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(powerUpTimer(other));
        }
    }

    IEnumerator lifeTimer()
    {
        yield return new WaitForSecondsRealtime(15f);
        Destroy(gameObject);
    }

    IEnumerator powerUpTimer(Collider player)
    {
        _PlayerControl healer = player.GetComponent<_PlayerControl>();

        healer.HP = healer.hpOriginal;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        StartCoroutine(txtTimer());
        yield return new WaitForSeconds(8f);



        Destroy(gameObject);
    }

    IEnumerator txtTimer()
    {

        gameManager.instance.Healertxt.SetActive(true);
        yield return new WaitForSeconds(3);
        gameManager.instance.Healertxt.SetActive(false);
    }
}
