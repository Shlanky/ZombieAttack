using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class KillSelf : MonoBehaviour
{
    [SerializeReference] float TimeTiDie = 1;
    [SerializeField] VisualEffect bile;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Buff());
        bile.Play();

        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(TimeTiDie);
        Destroy(gameObject);
    }
    IEnumerator Buff()
    {
        yield return new WaitForSeconds(.7f);
    }
}
