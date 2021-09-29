
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy_Moving_System : MonoBehaviour {

    public GameObject enemyBullet;
    public GameObject firePoint;
    float reachDistance = 1f;
    public int curWayPointID = 0;
    public Transform GunPoint;
    public Transform PlayerPos;
    Transform target;
    Rigidbody2D rb;
    public EditablePathController path;
    LevelSystem LS;
    public float speed;
    float saveSpeed;
    float reloadBullet;
    public float reloadSpeed;
    public GameObject smoke;
    public AudioSource FireSound;
    GameObject Player;
    float fRandomAttack;
    public bool hit;
    int tp;
    // Use this for initialization
    void Awake()
    {
        LS = GameObject.FindGameObjectWithTag("System").GetComponent<LevelSystem>();
        saveSpeed = speed + (LS.curLvl+1) * 0.10f + Random.Range(0.05f, 0.1f);
    }
    void Start () {
        reloadBullet = reloadSpeed;
        reloadSpeed = Random.Range(reloadSpeed, reloadSpeed+1.25f);
        Player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        GoToNextWayPoint();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!hit)
        {
            rb.velocity = ((Vector2)(target.position - transform.position)).normalized * speed;
        }
        
        if (reloadBullet > 0)
        {
            reloadBullet -= 1f * Time.deltaTime;
        }
        if(reloadBullet <= 0)
        {
            reloadBullet = reloadSpeed;
            Instantiate(enemyBullet, firePoint.transform.position, firePoint.transform.rotation);
            Instantiate(smoke, firePoint.transform);
            float randomPitch = Random.Range(0.7f, 1.1f);


            FireSound.pitch = randomPitch;




            FireSound.Play();
        }
        PlayerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        var gunRotation = Quaternion.LookRotation(Vector3.forward, PlayerPos.position - GunPoint.position);
        GunPoint.rotation = Quaternion.Slerp(GunPoint.rotation, gunRotation, 5f * Time.deltaTime);

        if (curWayPointID < path.waypoints.Count)
        {
            Transform target = path.waypoints[curWayPointID];

            
            float dist = Vector2.Distance((Vector2)target.position, transform.position);
            if (dist < reachDistance)
            {
                
                GoToNextWayPoint();
            }
        }
        var targetRotation = Quaternion.LookRotation(Vector3.forward, target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5f*Time.deltaTime);

    }
    void FixedUpdate()
    {
        if(fRandomAttack > 0)
        {
            fRandomAttack -= 1f * Time.deltaTime;
        }
    }
    void GoToNextWayPoint()
    {



        if (curWayPointID == path.waypoints.Count - 1)
        {
            rb.velocity = Vector2.zero;
            int RandomF = Random.Range(0, 100);
            if(RandomF >50)
                curWayPointID =0;
            

        }
        else 
        {
            curWayPointID++;
            speed = saveSpeed;

        }
        target = path.waypoints[curWayPointID];







    }
    void OnCollisionStay2D(Collision2D col)
    {
        hit = true;
        if (fRandomAttack < 0)
        {
            
            float a = Random.Range(0, 100);
            tp++;
            if (a < 50)
            {
                if (curWayPointID >= 1)
                {
                    curWayPointID-=2;
                }
                else
                {
                    curWayPointID++;
                }
                target = path.waypoints[curWayPointID];

            }
            fRandomAttack = 5f;
            if(col.gameObject.tag != "Enemy")
            {
                speed = 0.9f;
            }
            if(tp >= 3)
            {
                target = Player.transform;
            }
            
        }
            //else
            //{
                //
            //}
        
        
    }
    void OnCollisionExit2D(Collision2D col)
    {
        hit = false;
    }






}


