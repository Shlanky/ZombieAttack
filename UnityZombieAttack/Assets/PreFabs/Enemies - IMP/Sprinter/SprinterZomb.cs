using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SprinterZomb : MonoBehaviour, iDamageable
{
    [Header("Components")]

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Renderer rend;
    [SerializeField] Animator anim;

    [Header("----------------------------------")]
    [Header("Enemy Attributes")]
    [SerializeField] int HP;
    [SerializeField] int viewAngle;
    [SerializeField] int playerFaceSpeed;
    [SerializeField] int roamRadius;
    [SerializeField] float AttackAnimBuffer;

    [Header("----------------------------------")]
    [Header("Weapon Stats")]
    [SerializeField] float shootRate;

    [Header("----------------------------------")]
    [Header("Power Ups")]
    [SerializeField] OneShotOneKIll damageDrop;
    [SerializeField] doublePoints MoneyRush;
    [SerializeField] Healer heal;
    [SerializeField] FullOfBullets ammo;


    [SerializeField] bool playerInRange;
    Vector3 playerDir;
    Vector3 startingPos;
    float StoppingDistOrig;

    [Header("----------------------------------")]
    [Header("Explosive Timer")]
    [SerializeField] int timer;
    [SerializeField] SprinterExplosion Sprinter_Explosion;

    [Header("----------------------------------")]
    [Header("Audio")]
    public AudioSource aud;

    //gun shot
    [SerializeField] AudioClip[] zombieHit_sound;
    [SerializeField] AudioClip[] zombieDamage_sound;
    [Range(0, 1)] [SerializeField] float volume;

    int shotPoints;
    int killPoints;

     int cur_rounds;

    gameManager test;

    public static int GameModeHolder;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
        StoppingDistOrig = agent.stoppingDistance;
        gameManager.instance.updateEnemyNumber();

        GameModeHolder = buttonFunction.gameModeNum;
    }

    // Update is called once per frame
    void Update()
    {
        //aud.PlayOneShot(zombieHit_sound[Random.Range(0, zombieHit_sound.Length)], volume);
        shotPoints = gameManager.instance.playerScript.shotPoint;
        killPoints = gameManager.instance.playerScript.killPoint;
        if (agent.isActiveAndEnabled)
        {

            anim.SetFloat("Speed", Mathf.Lerp(anim.GetFloat("Speed"), agent.velocity.normalized.magnitude, Time.deltaTime * 5));

            playerDir = gameManager.instance.player.transform.position - transform.position;
            agent.SetDestination(gameManager.instance.player.transform.position);
            facePlayer();
            if (playerInRange)
            {
                StartCoroutine(goBOOM());
            }

        }
    }

    void facePlayer()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            playerDir.y = 0;
            var rotation = Quaternion.LookRotation(playerDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * playerFaceSpeed);
        }
    }

    IEnumerator goBOOM()
    {
        //  aud.PlayOneShot(zombieHit_sound[Random.Range(0, zombieHit_sound.Length)], volume);

        yield return new WaitForSeconds(timer);
        if (playerInRange)
        {

            Instantiate(Sprinter_Explosion, agent.transform.position, Sprinter_Explosion.transform.rotation);
            //Instantiate Explosion at Agent.Position, following Rotation of Explosion
            gameManager.instance.checkEnemyKills();

            Destroy(gameObject);
        }
    }
    IEnumerator goBoomNOW()
    {
        Instantiate(Sprinter_Explosion, agent.transform.position, Sprinter_Explosion.transform.rotation);
        //Instantiate Explosion at Agent.Position, following Rotation of Explosion
        Destroy(gameObject);
        yield return new WaitForSeconds(0.001f);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            playerInRange = true;
            // canShoot = true;
            agent.stoppingDistance = StoppingDistOrig;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            agent.stoppingDistance = 0;
        }
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;
        playerInRange = true;
        gameManager.instance.playerScript.earnPoints(shotPoints);

        StartCoroutine(flashColor());
        if (HP <= 0)
        {
            gameManager.instance.checkEnemyKills();
            agent.enabled = false;
            anim.SetBool("Dead", true);
            gameManager.instance.playerScript.earnPoints(killPoints);

            foreach (Collider col in GetComponents<Collider>())
                col.enabled = false;
            StartCoroutine(goBoomNOW());
            powerUpDrop();
        }

    }
    IEnumerator flashColor()
    {
        rend.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        rend.material.color = Color.white;
    }

    public void powerUpDrop()
    {
        //make a random number
        int maybePowerUp = Random.Range(0, 25);
        if (maybePowerUp == 4)
        {
            //make a heal power up on the body
            Instantiate(heal, transform.position + new Vector3(0, 1f, 0), Quaternion.Euler(0, 0, 0));
        }
        if (maybePowerUp == 8 && GameModeHolder == 2)
        {
            //make a damage power drop on body
            Instantiate(damageDrop, transform.position + new Vector3(0, 1f, 0), Quaternion.Euler(0, 0, 0));
        }

        if (maybePowerUp == 8 && GameModeHolder == 3)
        {
            //make a damage power drop on body
            Instantiate(damageDrop, transform.position + new Vector3(0, 1f, 0), Quaternion.Euler(0, 0, 0));
        }

        if (maybePowerUp == 15)
        {
            //make a ammo drop on body
            Instantiate(ammo, transform.position + new Vector3(0, 1f, 0), Quaternion.Euler(0, 0, 0));
        }

        if (maybePowerUp == 21 && GameModeHolder == 2)
        {
            //make a double points drop on body
            Instantiate(MoneyRush, transform.position + new Vector3(0, 1f, 0), Quaternion.Euler(0, 0, 0));
        }

        if (maybePowerUp == 21 && GameModeHolder == 3)
        {
            //make a damage power drop on body
            Instantiate(MoneyRush, transform.position + new Vector3(0, 1f, 0), Quaternion.Euler(0, 0, 0));
        }


    }

    public void roundIncreaseBuff()
    {
        if (cur_rounds <= 10)
        {
            Sprinter_Explosion.damage += 1;
            HP += 5;
        }
    }


}
