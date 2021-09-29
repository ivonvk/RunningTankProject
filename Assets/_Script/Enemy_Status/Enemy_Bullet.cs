/* This is a script to decide Enemy bullet dmg and function;
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * --------(Ctrl + F) to search method below --------
 * //If an Enemy shoot a bullet with powerful dmg would be spwan a extra dmg in an area(this is belong to Animation
 * 
 * 
 */







using UnityEngine;

public class Enemy_Bullet : MonoBehaviour {
    public GameObject powerWeapon;
    public GameObject explore;
    public Vector2 direction;

    public int dmg;
    public int ipowerWeapon;
    public float movingSpeed;
    public float killedTimer;
    

    LevelSystem LS;
    Animator anim;
    Enemy_Bullet_Parent parent;
    void Awake()
    {
        LS = GameObject.FindGameObjectWithTag("System").GetComponent<LevelSystem>();
        if(transform.parent != null)
        {
            parent = transform.parent.GetComponent<Enemy_Bullet_Parent>();
        }
        

    }
    void Start()
    {
        dmg += LS.curLvl+1 * Random.Range(2, 4);
        direction = transform.up;
        GetComponent<Rigidbody2D>().velocity = direction * movingSpeed;
        Invoke("DestroyBullet", killedTimer);
        anim = GetComponent<Animator>();
        
    }
    //If an Enemy shoot a bullet with powerful dmg would be spwan a extra dmg in an area(this is belong to Animation------------------------------------------------------------------------------------------------------------------------------------------------------
    public void DestroyBullet()
    {
        if (ipowerWeapon == 1)
        {
            Instantiate(powerWeapon, transform.position, transform.rotation);
            Destroy(gameObject);
            parentDie();
        }
        else
        {
            Destroy(gameObject);
            parentDie();
        }
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
       if(col.gameObject.tag == "Player")
        {
            LS.HPGetDmg(dmg);
            Instantiate(explore, transform.position, transform.rotation);
            if(ipowerWeapon == 1)
            {
                GameObject a =  Instantiate(powerWeapon, transform.position, transform.rotation);
                AudioSource s = a.GetComponent<AudioSource>();
                float randomPitch = Random.Range(0.3f, 0.7f);
                s.pitch = randomPitch;
                s.volume = randomPitch;
            }
            Destroy(gameObject);
            parentDie();
        }
        if (col.gameObject.tag == "BlockingBox")
        {
            col.gameObject.GetComponent<Enemy_HP>().GetDmg(1);
            //GameObject a = Instantiate(powerWeapon, transform.position, transform.rotation);
            //AudioSource s = a.GetComponent<AudioSource>();
            float randomPitch = Random.Range(0.7f, 0.9f);
            //s.pitch = randomPitch;
            if (ipowerWeapon == 1)
            {
                Instantiate(powerWeapon, transform.position, transform.rotation);
                Destroy(gameObject);
                parentDie();
            }
            else
            {
                Destroy(gameObject);
                parentDie();


            }
            
            
        }
        
    }
    void parentDie()
    {
        if (transform.parent != null)
        {
            parent.GetDmg();
        }
    }
}
