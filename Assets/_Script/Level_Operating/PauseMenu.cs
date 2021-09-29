using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public bool bPause;
    public float selectingBtn;
    public int selectingInt;
    public float stopping = 0.5f;
    Animator animPause;
    bool press;
    public AudioSource AS;
    // Use this for initialization
    void Awake()
    {
        animPause = GetComponent<Animator>();  
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        selectingBtn = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.Escape) && !bPause)
        {
            animPause.Play("PausePanel_Start");
            Time.timeScale = 0;
            bPause = true;
            


        }
        else if (Input.GetKeyDown(KeyCode.Escape) && bPause)
        {


            animPause.Play("PausePanel_0_Push");
            bPause = false;
        }
        if (bPause)
        {
            PauseSelect();
           }
    }
    void FixedUpdate()
    {
        

    }
    void PauseSelect()
    {
        if (!press)
        {
            animPause.Play("PausePanel_" + selectingInt.ToString());
        }
        
        if (stopping > 0)
        {
            stopping -= 0.0225f * Time.unscaledTime;
        }
        else
        {
            stopping = 0;
        }
        if (selectingBtn < 0 && selectingInt >= 0 && selectingInt < 3&& stopping == 0)
        {
            AS.Play();
            stopping = 1.55f;
            

            selectingInt++;


            Debug.Log(selectingInt);
        }
        if (selectingBtn > 0 && selectingInt <= 3 && selectingInt >0 && stopping == 0)
        {
            AS.Play();
            stopping = 1.55f;
            

            selectingInt--;



            Debug.Log(selectingInt);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Animator a = GetComponentInParent<Animator>();
            press = true;
            animPause.Play("PausePanel_" + selectingInt.ToString() + "_Push");

        }
    }
    public void Back()
    {
        Time.timeScale = 1;
        press = false;
        bPause = false;
    }
    public void LevelMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelMenu");
    }
    public void Quit()
    {
        Application.Quit();

        
    }
}


