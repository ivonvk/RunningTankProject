/* This is a Level Manager System to operating the current Level
 * Including wave changing, Player Information, BackGround Scolling delect by Player Movement
 * 
 * 
 * --------(Ctrl + F) to search method below --------
 * //Checking if Player finish all the Wave
 * //Adding Enemy to current enemies which is spwaned
 * //Removing Enemy from current enemies which is destroy
 * //Stop BackGround Moving if direction facing is wrong
 * //Stop BackGround Moving if arrive a battle location
 * //delect Movement then BackGround decide to scolling or not
 * //losing HP if active some function
 * //losing MP if active some function
 * //GetScores
 */



using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelSystem : MonoBehaviour {
    public Text startIntLvl;
    int[] Lvl_scores = new int[5] { 0, 1, 2, 3, 4 };
    public int curLvl;
    public GameObject FailedPanel;
    public List<GameObject> allWaves = new List<GameObject>();
    public List<Enemy_HP> curWaveEnemies = new List<Enemy_HP>();
    public List<GameObject> allBuilding = new List<GameObject>();
    public List<GameObject> RandomBuilding = new List<GameObject>();
    public GameObject winPanel;
    public GameObject bossWavePanel;

    public Animator Lives;
    public Animator Crash;
    public Animator animPlayer;
    public int iLives;

    public Text LvlArea;
    public Text LvlMessages;

    public int lvl;

    string[] Alphabet = new string[26] { "1", "2", "3", "4", "5", "6", "7", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
    string[] txtLvlReady = new string[5] { "Wave 1 is Starting\n Keep Forward", "Wave 2 is Starting\n Keep Forward", "Wave 3 is Starting\n Keep Forward", "Wave 4 is Starting\n Keep Forward", "Boss Wave is Starting\n Keep Forward" };
    
    public GameObject bg_up;
    public GameObject bg_down;

    public float speed;
    
    public SpriteRenderer FacingSR;
    
    
    public bool forwardTo;

    Player_Movement PM;
    Animator BoxMid;
    Vector3 bg_up_pos;
    Vector3 bg_down_pos;


    Rigidbody2D rb;

    int currentArea;

    public float curLongWay = 0;
    float levelLongWay = 5f;

    public bool attacking;
    public bool stop;
    //--------------------------------------------------------------------------------------
    public float iMAXHP;
    public float iMAXMP;
    
    public Image HPBAR;
    public Image MPBAR;
    public int scores;
    public Text txtScores;
    public Text FinalScores;

    public float iHP;
    public float iMP;
    float r = 2f;
    bool GameOver;
    void Awake()
    {
        startIntLvl.text = (curLvl + 1).ToString();
        animPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        iLives = 3;
        //AudioListener.volume = 2;
        Destroy(GameObject.FindGameObjectWithTag("TitleSong"));

        FacingSR = GameObject.FindGameObjectWithTag("FacingTo").GetComponent<SpriteRenderer>();
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement>();
        BoxMid = GameObject.FindGameObjectWithTag("BoxMid").GetComponent<Animator>();
        bg_up_pos = bg_up.transform.position;
        bg_down_pos = bg_down.transform.position;
        LvlMessages.text = txtLvlReady[0];

        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();

        iHP = iMAXHP;
        iMP = iMAXMP;
    }
    void Start()
    {
        for (int i = 0; i < Lvl_scores.Length; i++)
        {
            Lvl_scores[i] = PlayerPrefs.GetInt("Lvl" + i + "_HighScores");
        }
        rb.isKinematic = true;
        /*for (int i = 0; i < 7; i++)
        {
            int a = Random.RandomRange(0, 13);
            if (!allBuilding[a].GetComponent<Enemy_HP>().selectedBox)
            {
                allBuilding[a].GetComponent<Enemy_HP>().selectedBox = true;
                RandomBuilding.Add(allBuilding[a]);

                allBuilding[a].transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                allBuilding[a].transform.localScale = new Vector3(Random.Range(1f, 2.25f), Random.Range(1f, 2.25f), Random.Range(1f, 2.25f));
                allBuilding[a].GetComponent<Animator>().Play("BuildingSpwan_Anim");
            }
            else
            {

                i--;
            }
        }*/


            for (int k = 0; k < allBuilding.Count; k++)
            {
                
            allBuilding[k].GetComponent<Animator>().Play("BuildingNotSpwan_Anim");
            allBuilding[k].transform.localScale = new Vector3(Random.Range(1f, 2.25f), Random.Range(1f, 2.25f), Random.Range(1f, 2.25f));
            allBuilding[k].transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }


        
    }
    void Update()
    {

        

    }
    void FixedUpdate()
    {
        
        if (curLongWay < 10.16f && !attacking && !stop)
        {
            curLongWay += 2.25f*Time.deltaTime;
            
        }
        else if (!attacking&& curLongWay >= 10.16f)
        {
            if (lvl == 1 && Alphabet[currentArea] == "1")
            {
                

                Invoke("GenerateNextWave", 1.25f);
                
            }
            else
            {
                Invoke("GenerateNextWave", 1.25f);
                
            }
            StartAttack();
            
        }
        PlayerStatus();





    }
    void LateUpdate()
    {
        LvlArea.text = "Lvl: " + lvl + "/" + Alphabet[currentArea].ToString();
        BackGroundMoving();

        txtScores.text = "Scores: " + scores.ToString();
        
        HPBAR.transform.localScale = new Vector3(iHP / iMAXHP, 1, 1);
        MPBAR.transform.localScale = new Vector3(iMP / iMAXMP, 1, 1);

        if (GameOver)
        {
            
            r -= 0.425f * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(r <= 0)
                {
                    SceneManager.LoadScene("LevelMenu");
                }
                
            }
        }
    }
    //Checking if Player finish all the Wave------------------------------------------------------------------------------------------------------------------------------------------------------
    void WaveCompleted()
    {
        PM.cantMove = true;
        PM.rb.velocity = Vector2.zero;
        Destroy(allWaves[0]);
        allWaves.Remove(allWaves[0]);
        if (allWaves.Count == 0)
        {
            winPanel.SetActive(true);
            GameOver = true;
            
                Lvl_scores[curLvl] = PlayerPrefs.GetInt("Lvl" + curLvl + "_HighScores");
            if(scores > Lvl_scores[curLvl])
            {
                PlayerSave.instance.addTotalScores(scores, curLvl);
                FinalScores.text = "Highest Scores!\n" + scores.ToString();
                PlayerPrefs.SetInt("Lvl" + curLvl + "_HighScores",scores);
               
            }
            else
            {
                FinalScores.text = "Scores: " + scores.ToString();
            }
            


        }
        else
        {
            PM.StopShooting();
            rb.isKinematic = true;
            if(allWaves.Count == 1)
            {
                bossWavePanel.SetActive(true);
            }
            attacking = false;
            curWaveEnemies.Clear();
            currentArea++;
            
            
            BoxMid.Play("BoxMidMoving");
            
            curWaveEnemies.Clear();
            LvlMessages.text = txtLvlReady[currentArea];




            /*for (int i = 0; i < 7; i++)
            {
                int a = Random.RandomRange(0, 13);
                if (!allBuilding[a].GetComponent<Enemy_HP>().selectedBox)
                {
                    allBuilding[a].GetComponent<Enemy_HP>().selectedBox = true;
                    RandomBuilding.Add(allBuilding[a]);

                    allBuilding[a].transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                    allBuilding[a].transform.localScale = new Vector3(Random.Range(1f, 2.25f), Random.Range(1f, 2.25f), Random.Range(1f, 2.25f));
                    allBuilding[a].GetComponent<Animator>().Play("BuildingSpwan_Anim");
                }
                
            }*/
        }


    }
   void GenerateNextWave()
    {
        BoxMid.Play("BoxMidMoving_Return");

        
        allWaves[0].SetActive(true);
        rb.isKinematic = false;

        Enemy_HP[] enemies = allWaves[0].GetComponentsInChildren<Enemy_HP>();
        for (int i = 0; i < enemies.Length; i++)
        {
            curWaveEnemies.Add(enemies[i]);
        }

    }
    //Adding Enemy to current enemies which is spwaned------------------------------------------------------------------------------------------------------------------------------------------------------
    public void AddSpwanEnemy(Enemy_HP a)
    {
        curWaveEnemies.Add(a.GetComponent<Enemy_HP>());
        
    }
    //Adding Building to Script in Background------------------------------------------------------------------------------------------------------------------------------------------------------
    public void AddBuilding(GameObject a)
    {
        allBuilding.Add(a);

    }
    //Removing Enemy from current enemies which is destroy ------------------------------------------------------------------------------------------------------------------------------------------------------
    public void EnemyDie(Enemy_HP enemy)
    {
        curWaveEnemies.Remove(enemy);
        if (curWaveEnemies.Count == 0)
        {
            
            for (int k = 0; k < allBuilding.Count; k++)
            {
                
                    allBuilding[k].GetComponent<Animator>().Play("BuildingNotSpwan_Anim");
                    allBuilding[k].GetComponent<Enemy_HP>().selectedBox = false;
                
            }
            RandomBuilding.Clear();
            WaveCompleted();
            
        }
    }
    //Stop BackGround Moving if direction facing is wrong------------------------------------------------------------------------------------------------------------------------------------------------------
    void stopRunning()
    {
        bg_up.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0, 0f);
        bg_down.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, 0, 0f);
        if (rb.velocity.y != 0)
        {
            PM.playingTankMovingSound(true);
            FacingSR.color = new Color(1, 0, 0);
            stop = true;
            forwardTo = false;
        }

        else if (!PM.turning)
        {
            PM.playingTankMovingSound(false);
            FacingSR.color = new Color(1, 0, 0);
            stop = true;
            forwardTo = false;
        }
    }
    //Stop BackGround Moving if arrive a battle location------------------------------------------------------------------------------------------------------------------------------------------------------
    void StartAttack()
    {
        PM.cantMove = false;
        attacking = true;
        curLongWay = 0f;
        levelLongWay = 10.16f;
        for (int k = 0; k < allBuilding.Count; k++)
        {

            allBuilding[k].GetComponent<Animator>().Play("BuildingSpwan_Anim");
            allBuilding[k].GetComponent<Enemy_HP>().selectedBox = false;

        }


    }
    //delect Movement then BackGround decide to scolling or not ------------------------------------------------------------------------------------------------------------------------------------------------------
    void BackGroundMoving()
    {
        if (bg_up.transform.position.y < bg_down_pos.y)
        {
            bg_down.transform.position = bg_down_pos;
            bg_up.transform.position = bg_up_pos;
        }
        else
        {
            if (!attacking && forwardTo)
            {
                bg_up.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -2.25f, 0f);
                bg_down.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -2.25f, 0f);
                PM.playingTankMovingSound(true);
                FacingSR.color = new Color(0, 1, 0);
                stop = false;
            }
            else if (((PM.transRotate >= 0 && PM.transRotate <= 15) || (PM.transRotate >= 345 && PM.transRotate <= 360)) && rb.velocity.y >= 0 && !attacking)
            {
                if (!attacking)
                {
                    bg_up.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -2.25f, 0f);
                    bg_down.GetComponent<Rigidbody2D>().velocity = new Vector3(0f, -2.25f, 0f);
                    PM.playingTankMovingSound(true);
                    FacingSR.color = new Color(0, 1, 0);
                    stop = false;
                    
                }
            }
            else
            {
                stopRunning();

            }
        }
    }
    //Checking Player Status ------------------------------------------------------------------------------------------------------------------------------------------------------
    void PlayerStatus()
    {
        /*if (iMP < 100)
        {
            iMP += 12*Time.deltaTime;
        }*/
         if (iMP > iMAXMP) {
            iMP = iMAXMP;
        }

        if(iHP > iMAXHP)
        {
            iHP = iMAXHP;
        }
        else if(iHP < 0)
        {
            if (iLives <= 0)
            {
                Time.timeScale = 0;
                FailedPanel.SetActive(true);
            }
            else
            {
                Time.timeScale = 0;
                iHP = iMAXHP;
                iLives -= 1;
                Lives.Play(iLives.ToString());
                animPlayer.Play("Player_Destroy");
                Crash.Play("Crash_Anim");
            }

        }
    }
    //losing HP if active some function ------------------------------------------------------------------------------------------------------------------------------------------------------
    public void HPGetDmg(int v)
    {
        animPlayer.Play("Player_GetDmg");
        iHP -= v;
    }
    //losing MP if active some function ------------------------------------------------------------------------------------------------------------------------------------------------------
    public void MPLosing(int v)
    {
        iMP -= v;
    }
    //GetScores ------------------------------------------------------------------------------------------------------------------------------------------------------
    public void scoresGet(int c)
    {
        scores += c;
    }



    public void PausePanelOperating()
    {
        

        
    }
}
