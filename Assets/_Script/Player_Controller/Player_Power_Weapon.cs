using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Power_Weapon : MonoBehaviour {

	public List<Transform> waypoints = new List<Transform>();
    float keepGetDmg;
    // Use this for initialization

    void Start()
    {
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
    void Update()
    {


        if (waypoints[0] == null)
        {
            Destroy(gameObject);
        }
        if (keepGetDmg > 0)
        {
            keepGetDmg -= 1 * Time.deltaTime;
        }
        else if (keepGetDmg <= 0)
        {
            keepGetDmg = 0.2f;
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (keepGetDmg <= 0)
            {
                col.gameObject.GetComponent<Enemy_HP>().GetDmg(7);
            }

        }
    }
}
