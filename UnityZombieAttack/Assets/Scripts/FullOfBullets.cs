using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullOfBullets : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    void Update()
    {
        //need to slow this down but it works
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
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
        _PlayerControl bulletMan = player.GetComponent<_PlayerControl>();

        bulletMan.roundsInReserve = bulletMan.OgRoundsInReserve;
        bulletMan.roundsInMag = bulletMan.ogRoundsinMag;
        bulletMan.roundsShot = 0;
        gameManager.instance.updateMagCount();
        gameManager.instance.updateReserveCount();
        StartCoroutine(txtTimer());

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(8f);



        Destroy(gameObject);
    }

    IEnumerator txtTimer()
    {

        gameManager.instance.FoBTxt.SetActive(true);
        yield return new WaitForSeconds(3);
        gameManager.instance.FoBTxt.SetActive(false);
    }
}
