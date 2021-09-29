using UnityEngine;

public class Enemy_HP : MonoBehaviour {
    public GameObject Item;

    public float EnemyHP;
    public float EnemyHPMAX;
    public string typeOfEnemy;
    public bool selectedBox;
    Animator anim;
    Color SR;
    Player_Movement pm;
    LevelSystem LS;
    Transform spwanItemBoxPos;

    bool changed;

    void Awake()
    {

        if (typeOfEnemy != "Item")
        {
            anim = GetComponent<Animator>();
        }
        LS = GameObject.FindGameObjectWithTag("System").GetComponent<LevelSystem>();
        spwanItemBoxPos = GameObject.FindGameObjectWithTag("SpwanItemBox").GetComponent<Transform>();
        if(gameObject.tag == "BlockingBox")
        {
            LS.AddBuilding(gameObject);
        }
        EnemyHPMAX += LS.curLvl + 1 * 5 + Random.Range(5, 10);
    }
    void Start () {
        SR = GetComponent<SpriteRenderer>().color;
        EnemyHP = EnemyHPMAX;
	}

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (EnemyHP < 0)
        {
            Instantiate(Item, transform.position, transform.rotation);
            switch (typeOfEnemy)
            {
                
                case "Enemy_1":
                    LS.scores += 1;
                    Destroy(gameObject);
                    LS.EnemyDie(this);
                    break;
                case "Enemy_2":
                    LS.scores += 3;
                    Destroy(gameObject);
                    LS.EnemyDie(this);
                    break;
                case "Enemy_3":
                    LS.scores += 2;
                    Destroy(gameObject);
                    LS.EnemyDie(this);
                    break;
                case "Enemy_4":
                    LS.scores += 5;
                    Destroy(gameObject);
                    LS.EnemyDie(this);
                    break;
                case "Item":
                    gameObject.GetComponent<Animator>().Play("BuildingNotSpwan_Anim");
                    EnemyHP = EnemyHPMAX;
                    

                    //float x = Random.Range((spwanItemBoxPos.position.x / 2) * -1, spwanItemBoxPos.position.x / 2);
                    //float y = Random.Range((spwanItemBoxPos.position.y / 2) * -1, spwanItemBoxPos.position.y / 2);
                    //gameObject.transform.position = new Vector2(x, y);
                    
                    //gameObject.SetActive(false);
                    changed = true;
                    SR = new Color(Random.Range(0f, 1.0f), Random.Range(0f, 1.0f), Random.Range(0f, 1.0f));
                    
                    
                    break;
                default:
                    Destroy(gameObject);
                    LS.EnemyDie(this);
                    break;

            }
        }
    }
    void LateUpdate()
    {
       
        //SR = new Color(1, EnemyHP / EnemyHPMAX, EnemyHP / EnemyHPMAX);

    }

    public void GetDmg(int v)
    {
        EnemyHP -= v;
        if(typeOfEnemy != "Item")
        {
            anim.Play("Enemy_GetDmg");
        }
        
    }
    

}
