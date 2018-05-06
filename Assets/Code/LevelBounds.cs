using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBounds : MonoBehaviour {

	public static Bounds bounds;
	// Use this for initialization
	void Awake () {
		bounds = GetComponent<Renderer>().bounds;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
