/* This is a Script Operating the Level Menu Scene
 * After Animation Finish, active function below
 * To check Axis Vertical Up And Down Selecting Level
 * Keycode.Space to confirm Level
 * 
 * --------(Ctrl + F) to search method below --------
 * //Put inside to array if a Game object with Text
 * //Put inside to array if a Game object with Animator
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */


using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour {
    int TS;
    int HS;
    public Text totalScores;
    public GameObject ReturnPlaySong;
    float selectingBtn;
    public int selectingInt;
    public bool turnOn;
    bool[] bLvlEnable = new bool[5];
    public string[] whichLvl = new string[5] { "Level_1", "Level_2", "Level_3", "Level_4", "Level_5" };
    public List<GameObject> lvlDone = new List<GameObject>();
    public List<GameObject> lvlNotDone = new List<GameObject>();
    public List<Animator> animBtns = new List<Animator>();
    public List<Text> levelTextBox = new List<Text>();
    float stopping = 0.5f;
    public GameObject oldSong;
    public AudioSource select;
    public AudioSource choose;
    void Awake()
    {
       
        oldSong = GameObject.FindGameObjectWithTag("TitleSong");
        TS = PlayerPrefs.GetInt("TotalScores");
        HS = PlayerPrefs.GetInt("HighestScores");
    }
    void Start () {
        if (TS > HS)
        {
            PlayerPrefs.SetInt("HighestScores", TS);
        }
        int[] isLvlDone = new int[5] { 0, 1, 2, 3, 4 };
        for (int i = 0; i < 5; i++)
        {

            isLvlDone[i] = PlayerPrefs.GetInt("Lvl" + i + "_HighScores");

            if (isLvlDone[i] != 0)
            {
                lvlDone[i].SetActive(true);
                
                    bLvlEnable[i] = true;
                bLvlEnable[i + 1] = true;
            }
			else if(i ==0){
				bLvlEnable[0] = true;
			}else if(!bLvlEnable[i])
            {
                lvlNotDone[i].SetActive(true);
                
            }
        }
        turnOn = false;
        animBtnsWorks();
        levelTextBoxWorks(); 
        if(oldSong == null)
        {
            ReturnPlaySong.SetActive(true);
        }
        totalScores.text = "Total Scores: \n" + TS;
    }


	void FixedUpdate () {
        if (turnOn)
        {
            if (stopping > 0)
            {
                stopping -= 1f * Time.deltaTime;
            }
            else
            {
                stopping = 0;
            }
            selectingBtn = Input.GetAxis("Vertical");
            if (selectingBtn < 0 && selectingInt < animBtns.Count - 1 && stopping == 0)
            {
                
                if (bLvlEnable[selectingInt+1]&& (selectingInt + 1)< 5)
                {
                    stopping = 0.5f;
                    levelTextBox[selectingInt].gameObject.SetActive(false);
                    animBtns[selectingInt].Play("Stop");
                    selectingInt++;

                    levelTextBox[selectingInt].gameObject.SetActive(true);
                    Debug.Log(selectingInt);
                    animBtns[selectingInt].Play("Level_Button_Selecting");
                    select.Play();
                    
                    

                }
                else if (selectingInt+1 == 3)
                {
                    stopping = 0.5f;
                    levelTextBox[selectingInt].gameObject.SetActive(false);
                    animBtns[selectingInt].Play("Stop");
                    selectingInt++;

                    levelTextBox[selectingInt].gameObject.SetActive(true);
                    Debug.Log(selectingInt);
                    animBtns[selectingInt].Play("Level_Button_Selecting");
                    select.Play();
                }

            }
            if (selectingBtn > 0 && selectingInt > 0 && stopping == 0)
            {
                stopping = 0.5f;
                levelTextBox[selectingInt].gameObject.SetActive(false);
                animBtns[selectingInt].Play("Stop");
                selectingInt--;
                
                if (bLvlEnable[selectingInt])
                {
                    animBtns[selectingInt].Play("Level_Button_Selecting");
                    select.Play();

                }
                levelTextBox[selectingInt].gameObject.SetActive(true);
                Debug.Log(selectingInt);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Animator a = GetComponentInParent<Animator>();
                a.Play("LevelMenuChanging");
               
                choose.Play();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                selectingInt = 3;
                Animator a = GetComponentInParent<Animator>();
                a.Play("LevelMenuChanging");

                choose.Play();
            }


        }
    }
    //Put inside to array if a Game object with Animator------------------------------------------------------------------------------------------------------------------------------------------------------
    void animBtnsWorks()
    {
        Animator[] tmpArray_animBtns = GetComponentsInChildren<Animator>();
        animBtns.Clear();
        foreach (Animator i in tmpArray_animBtns)
        {
            if (i != GetComponent<Animator>() )
            {
                animBtns.Add(i);
            }
        }

        animBtns[selectingInt].Play("Level_Button_Selecting");
        totalScores.text = "Total Scores: \n" + TS;




    }
    //Put inside to array if a Game object with Text------------------------------------------------------------------------------------------------------------------------------------------------------
    void levelTextBoxWorks()
    {
        Text[] tmpArray_Text= GetComponentsInChildren<Text>();
        levelTextBox.Clear();
        foreach (Text i in tmpArray_Text)
        {
            if (i != GetComponent<Text>())
            {
                levelTextBox.Add(i);
                
            }
        }
        
        for(int i = 0;i < levelTextBox.Count; i++)
        {
            levelTextBox[i].transform.gameObject.SetActive(false);
        }



        levelTextBox[0].gameObject.SetActive(true);
        
    }
    public void turnOnMenu()
    {
        turnOn = true;
    }
    public void whichLvlAnim()
    {
        if(selectingInt != 3)
        {
            SceneManager.LoadScene(whichLvl[selectingInt]);
        }
        else
        {
            
            SceneManager.LoadScene("StartMenu");
        }
        
    }

}
