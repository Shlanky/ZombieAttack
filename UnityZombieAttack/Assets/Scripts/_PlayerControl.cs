using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PlayerControl : MonoBehaviour, iDamageable
{
    [Header("Components")]
    [SerializeField] CharacterController controller;

    [Header("Player Atrobutes")]
    [Header("----------------------------------------------")]
    [Range(5, 20)] [SerializeField] int HP;
    [Range(3, 6)] [SerializeField] float playerSpeed;
    [Range(1.5f, 4.5f)] [SerializeField] float sprintMult;
    [Range(6, 10)] [SerializeField] float jumpHeight;
    [Range(15, 30)] [SerializeField] float gravityValue;
    [Range(1, 4)] [SerializeField] int jumps;

    [Header("Player Weapon Stats")]
    [Header("----------------------------------------------")]
    [Range(0.1f, 3)] [SerializeField] float shootRate;
    [Range(1, 10)] [SerializeField] int weaponDamage;
    [Range(5, 30)] [SerializeField] public int roundsInMag;
    [Range(0, 180)] [SerializeField] public int roundsInReserve;
    public int roundsShot;
    [SerializeField] public int keysFound;

    [Header("Effects")]
    [Header("----------------------------------------------")]
    [SerializeField] GameObject hitEffectSpark;
    [SerializeField] GameObject muzzleFlash;

    [Header("--------Physics----------")]

    public Vector3 pushback = Vector3.zero;
    [SerializeField] int pushResolve;

    [Header("--------Audio----------")]
    public AudioSource aud;

    //gun shot
    [SerializeField] AudioClip[] gunshot;
    [Range(0, 1)] [SerializeField] float gunshotVol;

    //player damaged
    [SerializeField] AudioClip[] playerHurt;
    [Range(0, 1)] [SerializeField] float playerHurtVol;

    //footsteps sound
    [SerializeField] AudioClip[] playerFootsteps;
    [Range(0, 1)] [SerializeField] float playerFootstepsVol;

    //reload sound
    [SerializeField] AudioClip[] reloadSound;
    [Range(0, 1)] [SerializeField] float ReloadSoundVol;

    //no ammo sound
    [SerializeField] AudioClip[] noAmmo;
    [Range(0, 1)] [SerializeField] float noAmmoVol;

    [SerializeField] AudioClip[] pickUpKey;
    [Range(0, 1)] [SerializeField] float pickUpKeyVol;


    bool isSprint = false;
    float playerSpeedOg;
    int Times_jump;
    Vector3 playerVelocity;
    Vector3 move;
    bool canShoot = true;
    int hpOriginal;
    Vector3 playerSpawnPos;
    int ogRoundsinMag;
    int OgRoundsInReserve;


    int keysNeeded = 3;

    bool footstepPlaying;

    private void Start()
    {
        playerSpeedOg = playerSpeed;
        hpOriginal = HP;
        playerSpawnPos = transform.position;
        ogRoundsinMag = roundsInMag;
        OgRoundsInReserve = roundsInReserve;
        gameManager.instance.updateMagCount();
        gameManager.instance.updateReserveCount();

    }

    void Update()
    {
        if (!gameManager.instance.paused)
        {
            pushback = Vector3.Lerp(pushback, Vector3.zero, Time.deltaTime * pushResolve);

            MovePLayer();
            Sprint();
            StartCoroutine(playFootsteps());

            //for cheking ammo status
            if (Input.GetButtonDown("Reload") && roundsInReserve > 0)
            {
                Reload();
            }
            else if (roundsInMag > 0 && canShoot == true)
            {
                StartCoroutine(shoot());
            }
            else if (roundsInMag == 0 && roundsInReserve == 0)
            {
                gameManager.instance.noAmmo.SetActive(true);
                canShoot = false;
            }
            else if (Input.GetButtonDown("Shoot") && canShoot == false)
            {
                aud.PlayOneShot(noAmmo[Random.Range(0, noAmmo.Length)], noAmmoVol);
            }

        }

    }


    private void MovePLayer()
    {

        if ((controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            playerVelocity.y -= 3;
        }


        if (controller.isGrounded && playerVelocity.y < 0)
        {
            Times_jump = 0;
            playerVelocity.y = 0f;
        }

        move = (transform.right * Input.GetAxis("Horizontal")) + (transform.forward * Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && Times_jump < jumps)
        {
            Times_jump++;
            playerVelocity.y = jumpHeight;
        }

        //adding gravity
        playerVelocity.y -= gravityValue * Time.deltaTime;

        controller.Move((playerVelocity + pushback) * Time.deltaTime);
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            isSprint = true;
            playerSpeed = playerSpeed * sprintMult;
        }

        else if (Input.GetButtonUp("Sprint"))
        {
            isSprint = false;
            playerSpeed = playerSpeedOg;
        }
    }

    IEnumerator playFootsteps()
    {
        if (controller.isGrounded && move.normalized.magnitude > 0.4f && !footstepPlaying)
        {
            footstepPlaying = true;

            aud.PlayOneShot(playerFootsteps[Random.Range(0, playerFootsteps.Length)], playerFootstepsVol);
            if (isSprint)
                yield return new WaitForSeconds(0.3f);
            else
                yield return new WaitForSeconds(0.4f);

            footstepPlaying = false;
        }
    }

    IEnumerator shoot()
    {
        // Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.forward * 100, Color.red);
        if (Input.GetButton("Shoot") && canShoot)
        {

            canShoot = false;

            aud.PlayOneShot(gunshot[Random.Range(0, gunshot.Length)], gunshotVol);
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)), out hit))
            {

                Instantiate(hitEffectSpark, hit.point, hitEffectSpark.transform.rotation);
                if (hit.collider.GetComponent<iDamageable>() != null)
                {
                    iDamageable isDamageable = hit.collider.GetComponent<iDamageable>();
                    if (hit.collider is SphereCollider)
                    {
                        isDamageable.takeDamage(10000);
                    }
                    else
                    {
                        isDamageable.takeDamage(weaponDamage);
                    }

                }
            }

            //muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
            muzzleFlash.SetActive(true);
            yield return new WaitForSeconds(.05f);
            muzzleFlash.SetActive(false);

            roundsInMag--;
            roundsShot++;
            gameManager.instance.shot();
            yield return new WaitForSeconds(shootRate);
            AmmoRemain();
        }

    }



    public void Reload()
    {
        if (Input.GetButtonDown("Reload") && ogRoundsinMag > roundsInMag)
        {
            aud.PlayOneShot(reloadSound[Random.Range(0, reloadSound.Length)], ReloadSoundVol);
            gameManager.instance.reload_txt.SetActive(false);
            gameManager.instance.reload();
            if (roundsInReserve < 30)
            {
                int tmp = roundsInReserve;
                roundsInMag += tmp;
                roundsInReserve -= tmp;
                canShoot = true;
                roundsShot = 0;
            }
            else
            {
                roundsInMag += roundsShot;
                roundsInReserve -= roundsShot;
                canShoot = true;
                roundsShot = 0;
            }
            gameManager.instance.resetAmmoMagCount();
        }
    }

    public void takeDamage(int dmg)
    {
        HP -= dmg;

        aud.PlayOneShot(playerHurt[Random.Range(0, playerHurt.Length)], playerHurtVol);

        updatePlayerHP();

        StartCoroutine(damageFlash());

        if (HP <= 0)
        {
            gameManager.instance.playerDead();
        }
    }

    IEnumerator damageFlash()
    {
        gameManager.instance.playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.playerDamageFlash.SetActive(false);

    }

    public void updatePlayerHP()
    {
        gameManager.instance.HPBar.fillAmount = (float)HP / (float)hpOriginal;
    }

    public void respawn()
    {

        HP = hpOriginal;
        updatePlayerHP();
        controller.enabled = false;
        transform.position = playerSpawnPos;
        controller.enabled = true;
        pushback = Vector3.zero;
    }

    //ammo
    public void giveAmmo(int amount)
    {
        if (roundsInReserve < OgRoundsInReserve)
        {
            roundsInReserve += amount;
            if (roundsInReserve > OgRoundsInReserve)
            {
                roundsInReserve = OgRoundsInReserve;
            }
        }
    }

    public bool checkAmmo(bool check)
    {
        if (roundsInReserve == OgRoundsInReserve)
        {
            check = false;
        }
        else
        {
            check = true;
        }


        return check;
    }

    public void AmmoRemain()
    {

        if (roundsInMag == 0 && roundsInReserve > 0)
        {
            canShoot = false;
            gameManager.instance.reload_txt.SetActive(true);
        }

        else
        {
            canShoot = true;
        }
    }



    //heals
    public void giveHP(int amount)
    {
        if (HP < hpOriginal)
        {
            HP += amount;
            if (HP > hpOriginal)
            {
                HP = hpOriginal;
            }
        }
        updatePlayerHP();
    }

    public bool checkHP(bool check)
    {
        if (HP == hpOriginal)
        {
            check = false;
        }
        else
        {
            check = true;
        }


        return check;
    }



    //keys
    public void giveKey(int amount)
    {
        keysFound++;
        gameManager.instance.pickUpKey();
        aud.PlayOneShot(pickUpKey[Random.Range(0, pickUpKey.Length)], pickUpKeyVol);
        //update the ui, make a update function for ui
    }

    public bool checkKey(bool key)
    {
        if (keysFound < keysNeeded)
        {
            key = false;
        }
        else if (keysFound == keysNeeded)
        {
            key = true;
        }

        return key;
    }


}
