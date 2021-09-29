using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSave : MonoBehaviour {
    public static PlayerSave instance;
    int[] Lvl_scores = new int[5] { 0, 1, 2, 3, 4 };
    int TS;
    int HS;
    // Use this for initialization
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        instance = this;
        
    }
    void Start () {
        TS = PlayerPrefs.GetInt("TotalScores");
        HS = PlayerPrefs.GetInt("HighestScores");

        for (int i = 0; i < Lvl_scores.Length; i++)
        {
            Lvl_scores[i] = PlayerPrefs.GetInt("Lvl" + i + "_HighScores");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void addTotalScores(int a,int curLvl) {
        if(a > Lvl_scores[curLvl])
        {
            PlayerPrefs.SetInt("TotalScores", TS +  Lvl_scores[curLvl] + a);
        }
        
    }

}
