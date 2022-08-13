using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class _PlayerControl : MonoBehaviour, iDamageable
{
    [Header("Components")]
    [SerializeField] CharacterController controller;

    [Header("Player Atrobutes")]
    [Header("----------------------------------------------")]
    [Range(5, 40)] [SerializeField] public int HP;
    [Range(3, 6)] [SerializeField] float playerSpeed;
    [Range(1.5f, 4.5f)] [SerializeField] float sprintMult;
    [Range(6, 10)] [SerializeField] float jumpHeight;
    [Range(15, 30)] [SerializeField] float gravityValue;
    [Range(1, 4)] [SerializeField] int jumps;
    public int points = 0;
    static int totalPoints;

    [Header("Player Slowing")]
    [Header("----------------------------------------------")]
    [SerializeField] float SlowSpeed;
    bool isSlowed = false;

    [Header("Player Weapon Stats")]
    [Header("----------------------------------------------")]
    [Range(0.1f, 3)] [SerializeField] float shootRate;
    [Range(1, 10)] [SerializeField] public int weaponDamage;
    [Range(5, 30)] [SerializeField] public int roundsInMag;
    [Range(0, 180)] [SerializeField] public int roundsInReserve;
    public int roundsShot;
    [SerializeField] public int keysFound;
    [SerializeField] gunStats currentGun;
    static gunStats Primary;
    static gunStats Secondary;

    [Header("Effects")]
    [Header("----------------------------------------------")]
    [SerializeField] GameObject hitEffectSpark;
    [SerializeField] GameObject muzzleFlash;

    //finsih off when i get the muzzel flashes from chris
    [SerializeField] GameObject Ak_Muzzel_Flash;
    [SerializeField] GameObject FaMas_Muzzel_Flash;
    [SerializeField] GameObject Ghost_Muzzel_Flash;
    [SerializeField] GameObject Uzi_Muzzel_Flash;
    [SerializeField] GameObject M16_Muzzel_Flash;
    [SerializeField] GameObject M1911_Muzzel_Flash;
    [SerializeField] GameObject MP5_Muzzel_Flash;
    [SerializeField] GameObject Revolver_Muzzel_Flash;



    [SerializeField] GameObject gunModel;
    public List<gunStats> gunList = new List<gunStats>();

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

    //gun pick up sounds
    [SerializeField] AudioClip[] gunWasPickedUp;
    [Range(0, 1)] [SerializeField] float GWPU_Volumue;


    bool isSprint = false;
    float playerSpeedOg;
    int Times_jump;
    Vector3 playerVelocity;
    Vector3 move;
    bool canShoot = true;
    public int hpOriginal;
    Vector3 playerSpawnPos;
    public int ogRoundsinMag;
    public int OgRoundsInReserve;

    public int shotPoint = 25;
    public int killPoint = 100;

    int keysNeeded = 3;

    bool footstepPlaying;

    // buttonFunction gameMode;
    static int GameModeHolder;

    public GameObject bloclking_wall;
    bool cantEscape = true;
    bool canReload = true;

    int muzzle_pos = 0;

    bool shooting = false;

    bool can_heal;
    private void Start()
    {
        //for the muzzel pos
        //make a switch that will turn a certain one on based on which gun it is
        //1 ak
        //2 Famas
        //3 Phost
        //4 Lil Uzi
        //5 m!6
        //6 m1911
        //7 Mp5
        //8 Revolver

        muzzle_pos = gunList[0].gunMuzzel;
        currentGun = gunList[0];
        shootRate = gunList[0].fireRate;
        weaponDamage = gunList[0].damage;
        roundsInMag = gunList[0].magSize;
        roundsInReserve = gunList[0].resSize;
        gunModel.GetComponent<MeshFilter>().sharedMesh = gunList[0].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunList[0].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
        playerSpeedOg = playerSpeed;
        hpOriginal = HP;
        playerSpawnPos = transform.position;
        ogRoundsinMag = roundsInMag;
        OgRoundsInReserve = roundsInReserve;
        gameManager.instance.updateMagCount();
        gameManager.instance.updateReserveCount();

        GameModeHolder = buttonFunction.gameModeNum;

        if (GameModeHolder == 1)
        {
            bloclking_wall = GameObject.FindGameObjectWithTag("HidingEnd");
        }

    }

    void Update()
    {

        if (!gameManager.instance.paused)
        {
            

            pushback = Vector3.Lerp(pushback, Vector3.zero, Time.deltaTime * pushResolve);

            MovePLayer();
            Sprint();

            StartCoroutine(playFootsteps());

            gameManager.instance.updatePoints();

            //can mess w this later if needed

            if (HP < hpOriginal)
            {
                StartCoroutine(healOverTime());
            }

            if (Input.GetButtonUp("Shoot"))
            {
                shooting = false;
            }

            if (Input.GetButtonDown("Reload") && roundsInReserve > 0 && canReload == true && shooting == false)
            {
                 Reload();
                StartCoroutine(reload_timer());
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
            else if (Input.GetButtonDown("Shoot") && roundsInMag == 0 && canShoot == false)
            {
                aud.PlayOneShot(noAmmo[Random.Range(0, noAmmo.Length)], noAmmoVol);
            }

        }

    }

    IEnumerator txtTimer()
    {
        gameManager.instance.EscapeNow.SetActive(true);
        cantEscape = false;
        yield return new WaitForSeconds(3);
        gameManager.instance.EscapeNow.SetActive(false);
        cantEscape = true;
    }

    IEnumerator reload_timer()
    {
        canReload = false;
        yield return new WaitForSeconds(2);
        canReload = true;
    }

    //dont need this anymore
    //public void switch_guns()
    //{
    //    if (Input.GetButtonDown("Primary"))
    //    {
    //        currentGun = gunList[0];
    //        shootRate = gunList[0].fireRate;
    //        weaponDamage = gunList[0].damage;
    //        roundsInMag = gunList[0].magSize;
    //        roundsInReserve = gunList[0].resSize;
    //        roundsShot = 0;
    //        gameManager.instance.updateMagCount();
    //        //will need to update the ui for the ammo stuff later

    //        gunModel.GetComponent<MeshFilter>().sharedMesh = gunList[0].gunModel.GetComponent<MeshFilter>().sharedMesh;
    //        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunList[0].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
    //        StartCoroutine(switchTimer());

    //    }

    //    if (Input.GetButtonDown("Secondary"))
    //    {
    //        currentGun = gunList[1];
    //        shootRate = gunList[1].fireRate;
    //        weaponDamage = gunList[1].damage;
    //        roundsInMag = gunList[1].magSize;
    //        roundsInReserve = gunList[1].resSize;
    //        roundsShot = 0;
    //        gameManager.instance.updateMagCount();
    //        gunModel.GetComponent<MeshFilter>().sharedMesh = gunList[1].gunModel.GetComponent<MeshFilter>().sharedMesh;
    //        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunList[1].gunModel.GetComponent<MeshRenderer>().sharedMaterial;
    //        StartCoroutine(switchTimer());
    //    }

    //}

    //use this somewher so they can j reload constantly while shooting
    IEnumerator readyToShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(2);
        canShoot = true;
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

        // Decide what movement speed to use if player is currently slowed.
        if (isSlowed == true)
        {
            // add our vector to the character controller move
            controller.Move(move * Time.deltaTime * SlowSpeed);
        }
        else
        {
            // add our vector to the character controller move
            controller.Move(move * Time.deltaTime * playerSpeed);
        }

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
        if (Input.GetButtonDown("Sprint") && isSlowed == false)
        {
            isSprint = true;
            playerSpeed = playerSpeed * sprintMult;

        }
        else if (Input.GetButtonDown("Sprint") && isSlowed == true)
        {
            isSprint = true;
            playerSpeed = SlowSpeed * sprintMult;
        }
        else if (Input.GetButtonUp("Sprint") && isSlowed == true)
        {
            isSprint = false;
            playerSpeed = SlowSpeed;
        }
        else if (Input.GetButtonUp("Sprint") && isSlowed == false)
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

    public void earnPoints(int val)
    {
        points += val;
        totalPoints = points;
    }

    IEnumerator shoot()
    {
        // Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.forward * 100, Color.red);
        if (Input.GetButton("Shoot") && canShoot)
        {
            shooting = true;
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

            //add the switch here for the dif muzzle flashes n check teh muzzel pos
            switch (muzzle_pos)
            {
                //ak47
                case 1:
                   // Ak_Muzzel_Flash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                    Ak_Muzzel_Flash.SetActive(true);
                    yield return new WaitForSeconds(.05f);
                    Ak_Muzzel_Flash.SetActive(false);
                    break;

                //FaMas
                case 2:
                    //FaMas_Muzzel_Flash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                    FaMas_Muzzel_Flash.SetActive(true);
                    yield return new WaitForSeconds(.05f);
                    FaMas_Muzzel_Flash.SetActive(false);
                    break;

                //Ghost
                case 3:
                    //Ghost_Muzzel_Flash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                    Ghost_Muzzel_Flash.SetActive(true);
                    yield return new WaitForSeconds(.05f);
                    Ghost_Muzzel_Flash.SetActive(false);
                    break;

                //Uzi
                case 4:
                    //Uzi_Muzzel_Flash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                    Uzi_Muzzel_Flash.SetActive(true);
                    yield return new WaitForSeconds(.05f);
                    Uzi_Muzzel_Flash.SetActive(false);
                    break;

                //m16
                case 5:
                   // M16_Muzzel_Flash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                    M16_Muzzel_Flash.SetActive(true);
                    yield return new WaitForSeconds(.05f);
                    M16_Muzzel_Flash.SetActive(false);
                    break;

                //m1911
                case 6:
                   // M1911_Muzzel_Flash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                    M1911_Muzzel_Flash.SetActive(true);
                    yield return new WaitForSeconds(.05f);
                    M1911_Muzzel_Flash.SetActive(false);
                    break;

                //Mp5
                case 7:
                  //  MP5_Muzzel_Flash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                    MP5_Muzzel_Flash.SetActive(true);
                    yield return new WaitForSeconds(.05f);
                    MP5_Muzzel_Flash.SetActive(false);
                    break;

                //revolver
                case 8:
                  //  Revolver_Muzzel_Flash.transform.localRotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                    Revolver_Muzzel_Flash.SetActive(true);
                    yield return new WaitForSeconds(.05f);
                    Revolver_Muzzel_Flash.SetActive(false);
                    break;

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

    //need to play around with 
    public void Reload()
    {
        if (Input.GetButtonDown("Reload") && ogRoundsinMag > roundsInMag)
        {
            aud.PlayOneShot(reloadSound[Random.Range(0, reloadSound.Length)], ReloadSoundVol);
            gameManager.instance.reload_txt.SetActive(false);
            gameManager.instance.reload();

            //teh final mag bug is here j need to manipulate the magsize
            if (roundsInReserve < ogRoundsinMag)
            {
                int tmp = roundsInReserve;
                roundsInMag += tmp;
                roundsInReserve -= tmp;
                canShoot = true;
                roundsShot = 0;

                if (roundsInMag > ogRoundsinMag)
                {
                    while (roundsInMag > ogRoundsinMag)
                    {
                        roundsInMag--;
                        roundsInReserve++;

                        if (roundsInMag == ogRoundsinMag)
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                roundsInMag += roundsShot;
                roundsInReserve -= roundsShot;
                canShoot = true;
                roundsShot = 0;
            }
            gameManager.instance.updateMagCount();
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
        aud.PlayOneShot(pickUpKey[Random.Range(0, pickUpKey.Length)], pickUpKeyVol);
        if (roundsInReserve < OgRoundsInReserve)
        {
            roundsInReserve += amount;
            if (roundsInReserve > OgRoundsInReserve)
            {
                roundsInReserve = OgRoundsInReserve;
            }
        }
        gameManager.instance.noAmmo.SetActive(false);
        canShoot = true;
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

        if (GameModeHolder == 1 && keysFound == 3)
        {
            Destroy(bloclking_wall);
            //add message for the player to let them know that found all the keys
            StartCoroutine(txtTimer());
        }

    }

    public bool checkKey(bool key)
    {
        if (keysFound < keysNeeded)
        {
            key = false;
        }
        else if (keysFound >= keysNeeded)
        {
            key = true;
            keysFound = 0;
        }

        return key;
    }

    //needs some fine tooning for it to work
    public void gunPickUp(int price, float firerate, int damage, int magSize, int resSize, GameObject muzzle_Flash, GameObject model, gunStats stats)
    {

        if (GameModeHolder == 1)
        {
            int tmp = 0;
            for (var i = 0; i < gunList.Count; i++)
            {
                if (gunList[i].gunModel == currentGun.gunModel)
                {
                    tmp = i;
                    break;
                }
            }

            gunList.Remove(currentGun);
            shootRate = firerate;
            weaponDamage = damage;
            roundsInMag = magSize;
            muzzleFlash = muzzle_Flash;
            roundsInReserve = resSize;
            //that ends here
            gunModel.GetComponent<MeshFilter>().sharedMesh = model.GetComponent<MeshFilter>().sharedMesh;
            gunModel.GetComponent<MeshRenderer>().sharedMaterial = model.GetComponent<MeshRenderer>().sharedMaterial;
            // gunList.Add(stats);
            gunList.Insert(tmp, stats);
            currentGun = gunList[tmp];
            gameManager.instance.updateMagCount();
            gameManager.instance.updateReserveCount();
            roundsShot = 0;
            ogRoundsinMag = gunList[tmp].magSize;
            OgRoundsInReserve = gunList[tmp].resSize;
            canShoot = true;
            gameManager.instance.noAmmo.SetActive(false);
            muzzle_pos = gunList[tmp].gunMuzzel;

        }


        if (GameModeHolder == 2 || GameModeHolder == 3)
        {
            int tmp = 0;
            for (var i = 0; i < gunList.Count; i++)
            {
                if (gunList[i].gunModel == currentGun.gunModel)
                {
                    tmp = i;
                    break;
                }
            }

            if (points >= price)
            {
                gunList.Remove(currentGun);
                CheckOut(price);
                shootRate = firerate;
                weaponDamage = damage;
                roundsInMag = magSize;
                muzzleFlash = muzzle_Flash;
                roundsInReserve = resSize;
                //that ends here
                gunModel.GetComponent<MeshFilter>().sharedMesh = model.GetComponent<MeshFilter>().sharedMesh;
                gunModel.GetComponent<MeshRenderer>().sharedMaterial = model.GetComponent<MeshRenderer>().sharedMaterial;
                // gunList.Add(stats);
                gunList.Insert(tmp, stats);
                currentGun = gunList[tmp];
                gameManager.instance.updateMagCount();
                gameManager.instance.updateReserveCount();
                roundsShot = 0;
                ogRoundsinMag = gunList[tmp].magSize;
                OgRoundsInReserve = gunList[tmp].resSize;
                canShoot = true;
                gameManager.instance.noAmmo.SetActive(false);
                muzzle_pos = gunList[tmp].gunMuzzel;

            }

            else
            {
                //do nothing or print message for the player to get more money or audio que
            }
        }

        aud.PlayOneShot(gunWasPickedUp[0], GWPU_Volumue);


    }

    public bool checkBalance(bool enough, int price)
    {
        if (points < price)
        {
            enough = false;
        }
        else if (points >= price)
        {
            enough = true;
        }
        return enough;
    }

    public int CheckOut(int price)
    {
        points -= price;
        return points;
    }

    public void Perks(int price, int tank, int Damage, int Jump)
    {
        points -= price;
        HP += tank;
        weaponDamage += Damage;
        jumps += Jump;
        if (tank > 0)
        {
            hpOriginal = HP;
        }
    }

    IEnumerator healOverTime()
    {
        yield return new WaitForSeconds(5);
        giveHP(5);
    }

    public void ToggleSlowOn()
    {
        isSlowed = true;
    }
    public void ToggleSlowOff()
    {
        isSlowed = false;
    }
}
