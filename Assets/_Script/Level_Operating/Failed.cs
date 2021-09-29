using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Failed : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void LevelMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("LevelMenu");
    }
}
