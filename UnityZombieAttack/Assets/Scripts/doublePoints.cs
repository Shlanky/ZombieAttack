using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doublePoints : MonoBehaviour
{
    // Update is called once per frame
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
        _PlayerControl point = player.GetComponent<_PlayerControl>();
        point.shotPoint *= 2;
        point.killPoint *= 2;

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        StartCoroutine(txtTimer());
        yield return new WaitForSeconds(8);

        point.shotPoint /= 2;
        point.killPoint /= 2;

        Destroy(gameObject);
    }

    IEnumerator txtTimer()
    {

        gameManager.instance.MoneyRushtxt.SetActive(true);
        yield return new WaitForSeconds(3);
        gameManager.instance.MoneyRushtxt.SetActive(false);
    }
}
