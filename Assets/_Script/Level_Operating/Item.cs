/* This Script is Manager the Item decide which item type with a Random Range.
 * There are 4 weapons , 5 function item , could collect by player
 * 
 * 
 * 
 * 
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

public class Item : MonoBehaviour {
    public int int_itemType;
    public int Random_itemType;
    string[] strAnim = new string[4] { "Scaling_Anim_0", "Scaling_Anim_1", "Scaling_Anim_2", "Scaling_Anim_3" };
    string[] strAnimScores = new string[5] { "Scaling_Anim_Scores_1", "Scaling_Anim_Scores_2" ,"Scaling_Anim_Scores_3", "Scaling_Anim_Scores_4", "Scaling_Anim_Scores_5" };
    string[] strRestoreScores = new string[1] { "Scaling_Anim_Item_HP" };
    Animator anim;
    LevelSystem LS;
    Player_Movement PM;

    // Use this for initialization
    void Awake()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        LS = GameObject.FindGameObjectWithTag("System").GetComponent<LevelSystem>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
    }
    void Start () {
        Destroy(gameObject, 10f);
        Random_itemType = Random.Range(0, 10);
        anim = GetComponent<Animator>();
        switch (Random_itemType)
        {
            case 0:
                int_itemType = Random.Range(0, strAnim.Length);
                anim.Play(strAnim[int_itemType]);
                break;
            case 1:
                int_itemType = Random.Range(0, strAnimScores.Length);
                anim.Play(strAnimScores[int_itemType]);
                break;
            case 2:
                int_itemType = Random.Range(0, strRestoreScores.Length);
                anim.Play(strRestoreScores[int_itemType]);
                LS.scoresGet(int_itemType + 1);
                break;
            default:
                int_itemType = Random.Range(0, strAnimScores.Length);
                anim.Play(strAnimScores[int_itemType]);
                LS.scoresGet(int_itemType + 1);
                break;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            switch (Random_itemType)
            {
                case 0:
                    
                    PM.ChangingWeaponGet(int_itemType);
                    LS.iMP += 100;
                    Destroy(gameObject);
                    break;

                case 1:
                    
                    break;
                case 2:
                    LS.iHP+=int_itemType + 1*40;
                    Destroy(gameObject);
                    break;
                default:
                    
                    break;
            }
        }
        
    }
    public void destroyItem()
    {
        Destroy(gameObject);
    }
}
