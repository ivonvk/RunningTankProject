using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet_Parent : MonoBehaviour {
    public List<GameObject> allBullet = new List<GameObject>();
    public int i;
	// Use this for initialization
	void Start () {
        foreach (Transform child in transform)
        {
            allBullet.Add(child.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
    public void GetDmg()
    {
        if(i > 0)
        {
            i--;
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
