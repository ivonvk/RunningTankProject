/* This is a Enemies Spwaning System Script, a few of GameObject who is a enemy of Player will be swpaned
 * A spwaned Enemy will get a path in Enemy_Moving_System from this Script
 * Also, it will add to the total of enemies who need the Player Destroy them and pass the way
 * 
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

public class SpwanPoint : MonoBehaviour {
    public EditablePathController path;
    public List<GameObject> Enemies = new List<GameObject>();

    public float keepSpwaning;//The number of enemies who will be spwaned, 1 sec for creating 1 enemy



    LevelSystem LS;
    // Use this for initialization
   void Awake()
    {
        LS = GameObject.FindGameObjectWithTag("System").GetComponent<LevelSystem>();
    }
    void Start () {
        InvokeRepeating("Spwaning", 0, 1f);
    }
	
	// Update is called once per frame
	void Update () {
		if(keepSpwaning <= 0)
        {
            CancelInvoke("Spwaning");
        }
        else
        {
            keepSpwaning -= 1f * Time.deltaTime;
        }
	}
    void Spwaning()
    {
        int a = Random.Range(0, Enemies.Count);
        GameObject g = Instantiate(Enemies[a], transform.position, transform.rotation);
        g.GetComponent<Enemy_Moving_System>().path = path;
        LS.AddSpwanEnemy(g.GetComponent<Enemy_HP>());
        
    }
}
