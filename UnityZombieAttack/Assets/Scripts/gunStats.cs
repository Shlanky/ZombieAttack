using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//dont really need this 
using UnityEngine.UI;


[CreateAssetMenu]

public class gunStats : ScriptableObject
{

    public float fireRate;
    public int damage;
    public int magSize;
    public int resSize;
    public GameObject muzzleFlash;
    public GameObject gunModel;

    public void giveAmmo(int ammo)
    {
        resSize += ammo;
    }

}
