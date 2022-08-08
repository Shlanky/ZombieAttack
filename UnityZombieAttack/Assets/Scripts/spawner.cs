using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] int numEnemiesToSpawn;
    [SerializeField] int timer;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject Spitter;
    [SerializeField] GameObject Sprinter;

    int spawnedEnemyNum;
    bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator spawnEnemy()
    {
        //int spawnThatEnemy = Random.RandomRange(1, 3);


        //if (spawnedEnemyNum == 1)
        //{
        //    canSpawn = false;
        //    Instantiate(enemy, transform.position, enemy.transform.rotation);
        //    spawnedEnemyNum++;
        //    yield return new WaitForSeconds(timer);
        //    canSpawn = true;
        //}

        //if (spawnedEnemyNum == 2)
        //{
        //    canSpawn = false;
        //    Instantiate(Spitter, transform.position, enemy.transform.rotation);
        //    spawnedEnemyNum++;
        //    yield return new WaitForSeconds(timer);
        //    canSpawn = true;
        //}

        //if (spawnedEnemyNum == 3)
        //{
        //    canSpawn = false;
        //    Instantiate(Sprinter, transform.position, enemy.transform.rotation);
        //    spawnedEnemyNum++;
        //    yield return new WaitForSeconds(timer);
        //    canSpawn = true;
        //}



        canSpawn = false;
        Instantiate(enemy, transform.position, enemy.transform.rotation);
        spawnedEnemyNum++;
        yield return new WaitForSeconds(timer);
        canSpawn = true;




    }

    // Update is called once per frame
    void Update()
    {
        if (canSpawn && spawnedEnemyNum < numEnemiesToSpawn)
        {
            StartCoroutine(spawnEnemy());
        }
    }
}
