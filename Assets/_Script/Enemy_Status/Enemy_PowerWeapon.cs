using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_PowerWeapon : MonoBehaviour {
    public List<Transform> waypoints = new List<Transform>();
    float keepGetDmg;
    LevelSystem LS;
    // Use this for initialization
     void Awake()
    {
        LS = GameObject.FindGameObjectWithTag("System").GetComponent<LevelSystem>();
    }
    void Start () {
        Transform[] tmpArray = GetComponentsInChildren<Transform>();
        waypoints.Clear();
        foreach (Transform i in tmpArray)
        {
            if (i != transform)
            {
                waypoints.Add(i);
            }
        }
		Destroy (gameObject, 8f);
    }
	
	// Update is called once per frame
	void Update () {
        
        
        if(waypoints[0] == null)
        {
            Destroy(gameObject);
        }
        if (keepGetDmg > 0)
        {
            keepGetDmg -= 1 * Time.deltaTime;
        }
        else if(keepGetDmg <= 0)
        {
            keepGetDmg = 0.6f;
        }
	}
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (keepGetDmg <= 0)
            {
                LS.HPGetDmg(1);
            }
            
}
    }
}
