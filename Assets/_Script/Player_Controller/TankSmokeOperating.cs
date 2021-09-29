/* This is a simple script to changing ParticleSystem value
 * To Stop the Particles go down , gravityModifier should be 0
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

public class TankSmokeOperating : MonoBehaviour {
    ParticleSystem ps;
    LevelSystem LS;
	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>();
        LS = GameObject.FindGameObjectWithTag("System").GetComponent<LevelSystem>();
        
	}
	
	// Update is called once per frame
	void LateUpdate () {
        bool a = LS.forwardTo;
        bool b = LS.attacking;
        bool c = LS.stop;
        var main = ps.main;
        if (!a && !b && !c)
        {
            
                main.gravityModifier = 0.01f;
            
            
        }
        else
        {
            main.gravityModifier = 0f;
        }
	}
}
