/* This is a Start Menu Scene Controller, a simple script selecting button with axix
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
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartMenuController : MonoBehaviour {
   
    // Use this for initialization
    float selectingBtn;
    public GameObject song;
    public AudioSource select;
    Animator Anim;
    int selectingInt;
    public GameObject oldSong;
    public Text TotalScores;
    void Awake()
    {
        oldSong = GameObject.FindGameObjectWithTag("TitleSong");
    }
    void Start () {
        
        TotalScores.text = "Highest Scores: " + PlayerPrefs.GetInt("HighestScores").ToString();
        Anim = GetComponent<Animator>();
        Anim.Play("Start_Selecting");
        if(oldSong == null)
        {
            song.SetActive(true);
            DontDestroyOnLoad(song);
        }
        
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (selectingInt)
            {
                case 0:
                    Anim.Play("Start_Push");
                    break;
                case 1:
                    Anim.Play("Quit_Push");
                    break;

            }
        }
	}
    void FixedUpdate()
    {
        selectingBtn = Input.GetAxis("Vertical");
        if (selectingBtn > 0)
        {
            Anim.Play("Start_Selecting");
            select.Play();
            selectingInt = 0;
        }
        else if (selectingBtn < 0)
        {
            Anim.Play("Quit_Selecting");
            select.Play();
            selectingInt = 1;
        }
    }
    public void GoToLevel()//ForButton
    {
        SceneManager.LoadScene("LevelMenu");
    }
    public void QuitGame()//ForButton
    {
        Application.Quit();
    }
}
