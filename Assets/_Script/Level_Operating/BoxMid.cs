/* This is a smaller Script to delecting a Player Movement in a Box 2D Trigger
 * Forward will be knock back but near by it, BackGround will be scrolling;
 * You need to kill all current enemies to operate those function under
 * For the Num, they using for limite Player playable area, if near by 0 ~ 3 Num, Player will be knock back
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

public class BoxMid : MonoBehaviour
{
    public int Num;
    LevelSystem LS;
    void Awake()
    {
        LS = GameObject.FindGameObjectWithTag("System").GetComponent<LevelSystem>();
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" )
        {
            GameObject a = col.gameObject;


            switch (Num)
            {
                case 0:
                    a.transform.position = new Vector2(a.transform.position.x, a.transform.position.y - 2.05f * Time.deltaTime);
                    LS.forwardTo = false;
                    Debug.Log("0");
                    break;
                case 1:
                    a.transform.position = new Vector2(a.transform.position.x - 2.05f * Time.deltaTime, a.transform.position.y);
                    LS.forwardTo = false;
                    Debug.Log("1");
                    break;
                case 2:
                    a.transform.position = new Vector2(a.transform.position.x, a.transform.position.y + 2.05f * Time.deltaTime);
                    LS.forwardTo = false;
                    Debug.Log("2");
                    break;
                case 3:
                    a.transform.position = new Vector2(a.transform.position.x + 2.05f * Time.deltaTime, a.transform.position.y);
                    LS.forwardTo = false;
                    Debug.Log("3");
                    break;
                case 5:
                    var dist = a.transform.position.y - transform.position.y;
                    if (dist > 0)
                    {
                        a.transform.position = new Vector2(a.transform.position.x, a.transform.position.y - 10f * Time.deltaTime);
                        Debug.Log("5");
                        LS.forwardTo = true;
                        break;
                    }
                    else
                    {
                        Debug.Log("5");
                        a.transform.position = new Vector2(a.transform.position.x, a.transform.position.y - 2.05f * Time.deltaTime);
                        break;
                    }
                    
                    
                    
            }

            
            
            
        }
    }

    public void txtGoodLucky()
    {
        LS.LvlMessages.text = "Good Lucky";
    }//ForAnimation
}
