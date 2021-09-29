using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimNearTarget : MonoBehaviour {
    Player_Movement pm;
	// Use this for initialization
	void Start () {
        pm = GetComponentInParent<Player_Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Enemy" && pm.s == null)
        {
            pm.GunAimTarget(col.gameObject.transform);
            
        }
        
            pm.Aiming = true;

        
        
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy" && pm.s != null)
        {
            
            pm.Aiming = false;
            pm.s = null;
        }
    }

}
