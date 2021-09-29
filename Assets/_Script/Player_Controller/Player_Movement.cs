/*
 *
 * 
 * --------(Ctrl + F) to search method below --------
 * //Tank Moving Sound After Fixed
 * //Lock a target with Aiming Line if target is not null
 * //Fixing the gun which it look at if a sec does not have any action
 * //A function with four direction Movement of Player
 * //Random FireSound pitch and improve quality 
 * //Changing Weapon if Enter an Box 2D Trigger with single type of weapon
 * //Check if it should keep shooting
 * //4 Weapons belong to Player
 *  //Check if No MP then stop attack
 */

using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {
    public GameObject gunMovingPoint;
    public Transform trans;
    public Transform gunTrans;
    public Transform lockGun;
    public Transform s;
    public Rigidbody2D rb;
    public Vector2 vec2Direction;
    public AudioSource tankMovingSound;

    public float gunRotateSpeed;
    public float rotateSpeed;
    public float movingSpeed;
    public float slowlySpeed;
    public float defaultGunRotate;
    public float saveRotate;
    public float resetGunRot;
    public float transRotate;
    public bool turning;
    public bool Aiming;
    public bool cantMove;

    
    float playerXMoving;
    float playerYMoving;
    float gunTurning;
    bool movingSound;
    //---------------------------------------------------------------------------------------------

    public AudioSource FireSound;
    public GameObject bullet_0;
    public GameObject bullet_1;
    public GameObject bullet_2;
    public GameObject bullet_3;
    public GameObject smoke;
    public GameObject firePoint_0;
    public GameObject firePoint_1;
    public GameObject firePoint_2;
    public GameObject firePoint_L;
    public GameObject firePoint_R;
    public int intWhichWeapon = 0;
    public float shootRate = 2f;
    float a ;
    public bool shouldFire;
    public bool stoppedFire;
    public List<Player_Bullet> curWaveEnemies = new List<Player_Bullet>();

    string[] whichWeapon = new string[4] { "Fire_0", "Fire_1", "Fire_2", "Fire_3" };

    LevelSystem LS;

    //---------------------------------------------------------------------------------------------
    void Awake()
    {
        shootRate = 4f;
        cantMove = true;
        LS = GameObject.FindGameObjectWithTag("System").GetComponent<LevelSystem>();
        slowlySpeed = 1f;
        rb = GetComponent<Rigidbody2D>();
        trans = GetComponent<Transform>();
        gunTrans = gunMovingPoint.GetComponent<Transform>();
        defaultGunRotate = 0;
        ChangingWeaponGet(0);
    }
  

    // Update is called once per frame
    void Update() {
        vec2Direction = transform.up;
        
        if(Time.timeScale == 1)
        {
            if (!cantMove)
            {
                movingturn();
                Shooting();
                rb.velocity = vec2Direction * movingSpeed * playerYMoving;
               


            }
            
            
        }
        
        if (gunTurning == 0 && Aiming && s != null)
        {
            gunTrans.rotation = Quaternion.LookRotation(Vector3.forward, s.position - gunTrans.position);
        }
        if (cantMove)
        {
            if (transRotate < 180 && transRotate > 2)
            {
                transform.Rotate(0, 0, -rotateSpeed * Time.deltaTime);
                StopShooting();

            }

            else if (transRotate > 180 && transRotate < 358)
            {
                transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);

            }
        }
        //easymoving();

    }
    void FixedUpdate()
    {
        
        playerXMoving = Input.GetAxis("Horizontal");
        playerYMoving = Input.GetAxis("Vertical");
        gunTurning = Input.GetAxis("PlayerGunTurning");

        saveRotate = Mathf.Round(gunTrans.localEulerAngles.z);
        transRotate = Mathf.Round(trans.localEulerAngles.z);
        CheckifNoMP();
        
        gunForcetoForward();
        gunTurningFunction();
        
        if (s != null)
        {
            GunAimTarget(s);
        }
        if(a > 0)
        {
            a -= 1f * Time.deltaTime;
        }
        else
        {
            stoppedFire = false;
        }

    }
    void LateUpdate()
    {
        turning = false;
    }
    //Tank Moving Sound After Fixed------------------------------------------------------------------------------------------------------------------------------------------------------
    public void playingTankMovingSound(bool v)
    {
        
        float randomPitch = Random.Range(0.9f, 1.1f);


        tankMovingSound.pitch = randomPitch;



        if (!movingSound && v) { 

        tankMovingSound.Play();
            movingSound = true;
        }

        
        if (movingSound &&!v)
        {
            tankMovingSound.Stop();
            movingSound = false;

        }
        
    }


    //Lock a target with Aiming Line if target is not null------------------------------------------------------------------------------------------------------------------------------------------------------
    public void GunAimTarget(Transform a)
    {
        s = a;

        
    }


    //Moving gun facing ------------------------------------------------------------------------------------------------------------------------------------------------------
    void gunTurningFunction()
    {
        if (gunTurning > -180 && gunTurning < 0)
        {
            turning = true;
            gunTrans.Rotate(0, 0, gunRotateSpeed * Time.deltaTime);
            resetGunRot = 1f;

        }

        else if (gunTurning > 0 && gunTurning < 180)
        {
            turning = true;
            gunTrans.Rotate(0, 0, -gunRotateSpeed * Time.deltaTime);
            resetGunRot = 1f;

        }
    }


    //Fixing the gun which it look at if a sec does not have any action ------------------------------------------------------------------------------------------------------------------------------------------------------
    void gunForcetoForward()
    {
        if (resetGunRot > 0)
        {
            resetGunRot -= 0.85f * Time.deltaTime;
        }

        if (!turning && rb.velocity.y == 0 && resetGunRot <=0f)
        {
            resetGunRot = 0;
            if (saveRotate < 180 && saveRotate > 2)
            {
                gunTrans.Rotate(0, 0, -gunRotateSpeed * Time.smoothDeltaTime);


            }

            else if (saveRotate > 180 && saveRotate < 358)
            {
                gunTrans.Rotate(0, 0, gunRotateSpeed * Time.smoothDeltaTime);

            }



        }
    }


    //A function with four direction Movement of Player----------------------------------------------------------------------------------------------------------------
    void movingturn()
    {
        if (playerXMoving > 0 && playerYMoving == 0f)
        {
            trans.Rotate(0, 0, -100 * Time.deltaTime);
        }
        else if (playerXMoving < 0&&playerYMoving == 0f)
        {
            trans.Rotate(0, 0, 100 * Time.deltaTime);
        }
        //----------------------------------------------------------------------------
        /*0--
          ---
          ---
          MovingFaceTo == 0;vvv
        */

        if (playerXMoving < 0 && playerYMoving > 0)
        {
            turning = true;
            trans.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }

        //----------------------------------------------------------------------------    
        /*---
          ---
          --0
          MovingFaceTo == 0;vvv
        */
        if (playerXMoving > 0 && playerYMoving < 0)
        {
            turning = true;
            trans.Rotate(0, 0, rotateSpeed * Time.deltaTime);


        }
        //----------------------------------------------------------------------------
        /*---
          ---
          0--
          MovingFaceTo == 0;vvv
        */

        if (playerXMoving < 0 && playerYMoving < 0)
        {
            turning = true;
            trans.Rotate(0, 0, -rotateSpeed * Time.deltaTime);

        }

        //----------------------------------------------------------------------------
        /*--0
          ---
          ---
          MovingFaceTo == 0;vvv
        */
        if (playerXMoving > 0 && playerYMoving > 0)
        {
            turning = true;
            trans.Rotate(0, 0, -rotateSpeed * Time.deltaTime);


        }
        //----------------------------------------------------------------------------
    }
    void easymoving()
    {
        //rb.velocity = new Vector2(0, playerYMoving*movingSpeed);
        if(playerXMoving > 0 )
        {
            trans.Rotate(0, 0, -100* Time.deltaTime);
        }else if(playerXMoving < 0)
        {
            trans.Rotate(0, 0, 100 * Time.deltaTime);
        }
        
    }

    //null--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
    void resetbody()
    {
        rb.velocity = gunMovingPoint.transform.up * 2f;
    }


    //Random FireSound pitch and improve quality------------------------------------------------------------------------------------------------------------------------------------------------------
    void SoundFire()
    {
        float randomPitch = Random.Range(0.7f, 1.1f);
        FireSound.pitch = randomPitch;
        FireSound.Play();
    }

    //Changing Weapon if Enter an Box 2D Trigger with single type of weapon------------------------------------------------------------------------------------------------------------------------------------------------------
    public void ChangingWeaponGet(int a)
    {
        intWhichWeapon = a;
        for (int i = 0; i < 4; i++) { 
            CancelInvoke(whichWeapon[i]);
    }


        if (shouldFire)
        {

            InvokeRepeating(whichWeapon[intWhichWeapon], 0, 1f / shootRate);


        }
        switch (intWhichWeapon)
        {
            case 0:

                shootRate =4f;
                break;
            case 1:

                shootRate = 3f;
                break;
            case 2:

                shootRate = 3f;
                break;
            case 3:

                shootRate = 2.33f;
                break;

        }
    }

    //Check if No MP then stop attack------------------------------------------------------------------------------------------------------------------------------------------------------
    void CheckifNoMP()
    {
        if (LS.iMP < 0)
        {
            intWhichWeapon = 0;
            shootRate = 4;
            LS.iMP = LS.iMAXMP;
            for (int i = 0; i < 4; i++)
                CancelInvoke(whichWeapon[i]);
        }
    }
    //Check if it should keep shooting------------------------------------------------------------------------------------------------------------------------------------------------------
    public void SetShouldFire(bool v)
    {

        shouldFire = v;
        
            if (shouldFire )
            {
            
            InvokeRepeating(whichWeapon[intWhichWeapon], 0,1f / shootRate);
            

        }
        else 
        {

            for (int i = 0; i < 4; i++)
            {
                CancelInvoke(whichWeapon[i]);
            }

            }
            
        }
    public bool GetShouldFire()
    {
        
        return shouldFire;
    }


    //Check if it push Attack Button------------------------------------------------------------------------------------------------------------------------------------------------------
    void Shooting() {
       



        if (Input.GetKeyDown(KeyCode.Space)&&!stoppedFire) 
            {
     
            
                SetShouldFire(true);
            stoppedFire = true;
            a = 1f / shootRate;
        }
           
            


            

        
        else if (Input.GetKeyUp(KeyCode.Space))
        { 
        
            SetShouldFire(false);
            
            }
        
    }


    //4 Weapons belong to Player------------------------------------------------------------------------------------------------------------------------------------------------------
    void Fire_0()
    {
        Instantiate(smoke, firePoint_0.transform);
        Instantiate(bullet_0, firePoint_0.transform.position, gunMovingPoint.transform.rotation);
        
        //LS.MPLosing((intWhichWeapon + 1) * 5);
        


    }
    void Fire_1()
    {
        Instantiate(bullet_1, firePoint_0.transform.position, gunMovingPoint.transform.rotation);
        Instantiate(bullet_1, firePoint_1.transform.position, gunMovingPoint.transform.rotation);
        Instantiate(bullet_1, firePoint_2.transform.position, gunMovingPoint.transform.rotation);
        Instantiate(smoke, firePoint_0.transform);
        SoundFire();
        LS.MPLosing((intWhichWeapon + 1)*2 );
      
    }
    void Fire_2()
    {
        Instantiate(bullet_2, firePoint_0.transform.position, gunMovingPoint.transform.rotation);
        Instantiate(bullet_2, firePoint_0.transform.position, gunMovingPoint.transform.rotation);
        Instantiate(bullet_2, firePoint_0.transform.position, gunMovingPoint.transform.rotation);
        Instantiate(smoke, firePoint_0.transform);
        SoundFire();
        LS.MPLosing((intWhichWeapon + 1)*3);
        
    }
    void Fire_3()
    {
        Instantiate(bullet_3, firePoint_0.transform.position, gunMovingPoint.transform.rotation);
        Instantiate(bullet_3, firePoint_0.transform.position, firePoint_L.transform.rotation);
        Instantiate(bullet_3, firePoint_0.transform.position, firePoint_R.transform.rotation);
        Instantiate(smoke, firePoint_0.transform);
        SoundFire();
        LS.MPLosing((intWhichWeapon + 1)*4 );
        
    }
    public void ReSpwan()
    {
        Time.timeScale = 1;
    }
    public void StopShooting()
    {
        shouldFire = false;
        for (int i = 0; i < 4; i++)
        {
            
            CancelInvoke(whichWeapon[i]);
        }
    }
}
