using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    [SerializeField] public int numEnemiesToSpawn;
    [SerializeField] int timer;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject Spitter;
    [SerializeField] GameObject Sprinter;

    public int spawnedEnemyNum;
    int killed = 0;
    bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {

    }

   public IEnumerator spawnEnemy()
    {
        int enemyNum = Random.Range(1, 4);

        if (enemyNum == 1)
        {
            canSpawn = false;
            Instantiate(enemy, transform.position, enemy.transform.rotation);
            spawnedEnemyNum++;
            yield return new WaitForSeconds(timer);
            canSpawn = true;
        }

        if (enemyNum == 2)
        {
            canSpawn = false;
            Instantiate(Spitter, transform.position, enemy.transform.rotation);
            spawnedEnemyNum++;
            yield return new WaitForSeconds(timer);
            canSpawn = true;
        }

        if (enemyNum == 3)
        {
            canSpawn = false;
            Instantiate(Sprinter, transform.position, enemy.transform.rotation);
            spawnedEnemyNum++;
            yield return new WaitForSeconds(timer);
            canSpawn = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        killed = gameManager.instance.enimiesKilled;
        if (canSpawn && spawnedEnemyNum < numEnemiesToSpawn)
        {
            StartCoroutine(spawnEnemy());
        }
    }
}
