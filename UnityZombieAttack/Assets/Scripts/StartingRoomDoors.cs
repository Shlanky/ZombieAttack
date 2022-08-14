using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRoomDoors : MonoBehaviour
{
    int gameMode;

    bool in_range = false;
    public GameObject[] StartingDoors;
    public bool canStartSpawners = false;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = buttonFunction.gameModeNum;
        StartingDoors = GameObject.FindGameObjectsWithTag("Starting Doors");
    }

    // Update is called once per frame
    void Update()
    {
        if (in_range)
        {
            StartMaze();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            in_range = true;
        }
    }

    void StartMaze()
    {
        if (Input.GetButtonDown("Buy"))
        {
            canStartSpawners = true;

            for (int i = 0; i < StartingDoors.Length; i++)
            {
                Destroy(StartingDoors[i]);
            }

        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            in_range = false;
            gameManager.instance.WallBuyTxt.SetActive(false);
        }
    }

}

//bool buyAble = false;
//bool in_Range = false;
//[SerializeField] int Price;

//public void Update()
//{
//    if (in_Range)
//    {
//        wasDoorBought();
//    }
//}

//public void OnTriggerEnter(Collider other)
//{
//    if (other.CompareTag("Player"))
//    {
//        in_Range = true;
//        gameManager.instance.WallBuyTxt.SetActive(true);
//        buyAble = gameManager.instance.playerScript.checkBalance(buyAble, Price);

//    }
//}

//public void wasDoorBought()
//{
//    if (Input.GetButton("Buy") && buyAble == true)
//    {
//        Destroy(gameObject);
//        gameManager.instance.WallBuyTxt.SetActive(false);
//        gameManager.instance.playerScript.CheckOut(Price);
//    }
//}

//public void OnTriggerExit(Collider other)
//{
//    if (other.CompareTag("Player"))
//    {
//        in_Range = false;
//        gameManager.instance.WallBuyTxt.SetActive(false);
//    }
//}
