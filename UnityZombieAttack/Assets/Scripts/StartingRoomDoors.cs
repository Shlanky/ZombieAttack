using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingRoomDoors : MonoBehaviour
{
    int gameMode;
    bool in_range = false;
    public GameObject[] StartingDoors;
    public GameObject[] test;
    public bool canStartSpawners = false;

    // Start is called before the first frame update
    void Start()
    {
        gameMode = buttonFunction.gameModeNum;
        StartingDoors = GameObject.FindGameObjectsWithTag("Starting Doors");
        test = GameObject.FindGameObjectsWithTag("Spawner");
        for (int i = 0; i < test.Length; i++)
        {
            test[i].SetActive(false);
        }
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
            gameManager.instance.startRoomDoorstxt.SetActive(true);
        }
    }

    void StartMaze()
    {
        if (Input.GetButtonDown("Buy"))
        {
            canStartSpawners = true;
            if (canStartSpawners == true)
            {
                foreach (GameObject item in test)
                {
                    item.SetActive(true);
                }
            }
            for (int i = 0; i < StartingDoors.Length; i++)
            {
                canStartSpawners = true;
                StartingDoors[i].GetComponent<MeshCollider>().enabled = false;
                StartingDoors[i].GetComponent<BoxCollider>().enabled = false;
                StartingDoors[i].GetComponent<MeshRenderer>().enabled = false;

            }
            gameManager.instance.startRoomDoorstxt.SetActive(false);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            in_range = false;
            gameManager.instance.startRoomDoorstxt.SetActive(false);
        }
    }

}
