using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{

    public bool roomSpawn = false;
    [SerializeField] int roomNum;
    GameObject[] spawners;

    private void Start()
    {
        if (roomNum == 1)
        {
            spawners = GameObject.FindGameObjectsWithTag("Spawner");
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].SetActive(false);
            }
        }
        if (roomNum == 2)
        {
            spawners = GameObject.FindGameObjectsWithTag("Spawner1");
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].SetActive(false);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (roomSpawn == true)
        {
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].SetActive(true);
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomSpawn = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            roomSpawn = false;
            for (int i = 0; i < spawners.Length; i++)
            {
                spawners[i].SetActive(false);
            }
        }
    }
}
