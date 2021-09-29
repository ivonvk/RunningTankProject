using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditablePathController : MonoBehaviour {
    public Color rayColor = Color.white;
    public List<Transform> waypoints = new List<Transform>();
    
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        Transform[] tmpArray = GetComponentsInChildren<Transform>();
        waypoints.Clear();
        foreach(Transform i in tmpArray)
        {
            if (i != transform)
            {
                waypoints.Add(i);
            }
        }
        for(int i = 0;i < waypoints.Count-1; i++)
        {
            Vector3 pos = waypoints[i].position;
            Vector3 nextPos = waypoints[i + 1].position;
            Gizmos.DrawLine(pos, nextPos);
        }
    }
}
