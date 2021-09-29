/* This is a bullet belong to player, 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */




using UnityEngine;

public class Player_Bullet : MonoBehaviour {
    public GameObject explore;
    public GameObject powerWeapon;
    public AudioSource Sound;
    public int dmg;//Bullet give target Damge;
    public float movingSpeed;//Bullet Speed;
    public float destroyTime;//Fire Range of bullet
    public string typeweapon;//Difference weapon would be changed

    public int ipowerWeapon;
    Vector2 direction;
    Animator anim;
    AudioSource FireSound;
    bool dead;
    string[] alltypeweapon = new string[4] { "bullet_1", "bullet_2", "bullet_3", "bullet_4" };

    // Use this for initialization
    void Awake()
    {
        anim = GetComponent<Animator>();
        FireSound = GetComponent<AudioSource>();
        float randomPitch = Random.Range(0.5f, 0.825f);

        FireSound.pitch = randomPitch;
        FireSound.volume = randomPitch;
        Sound.pitch = randomPitch;
        Sound.volume = randomPitch;
        direction = transform.up;
    }
    void Start () {
        GameObject q = Instantiate(Sound.gameObject, transform.position, transform.rotation);
        Destroy(q,1f);
        float a = Random.Range(0, 100);
        if (a > 50)
        {
            movingSpeed = Random.Range(11, 13);

        }
        else
        {
            movingSpeed = Random.Range(7, 9);
        }
       
        switch (typeweapon)
        {
            case "bullet_1":
                break;
        }
        
        
        GetComponent<Rigidbody2D>().velocity = direction * movingSpeed;
        Invoke("DestroyBullet", destroyTime);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BlockingBox"&&!dead)
        {
            dead = true;
            Instantiate(explore, transform.position,transform.rotation);
            GetComponent<Rigidbody2D>().velocity = direction * 0;
            col.gameObject.GetComponent<Enemy_HP>().GetDmg(5);
            Destroy(gameObject);
        
        }
        if (col.gameObject.tag == "Enemy" && !dead)
        {
            dead = true;
            if(ipowerWeapon == 1)
            {
                Instantiate(powerWeapon, transform.position, transform.rotation);
            }
            Instantiate(explore, transform.position, transform.rotation);
            GetComponent<Rigidbody2D>().velocity = direction * 0;
            col.gameObject.GetComponent<Enemy_HP>().GetDmg(dmg);
            Destroy(gameObject);
      
        }
    }



    public void DestroyBullet()//ForAnimation
    {
        if (ipowerWeapon == 1)
        {
            Instantiate(powerWeapon, transform.position, transform.rotation);
            
           
        }
        Instantiate(explore, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
