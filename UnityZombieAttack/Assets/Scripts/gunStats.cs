using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//dont really need this 
using UnityEngine.UI;


[CreateAssetMenu]

public class gunStats : ScriptableObject
{

    public int priceOfGun;
    public float fireRate;
    public int damage;
    public int magSize;
    public int resSize;
    public GameObject muzzleFlash;
    public GameObject gunModel;

    //add muzzle flash pos here so it goes in the right spot

}
