using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteInitiator : MonoBehaviour {

	public GameObject mapblock;
	// Use this for initialization
	void Start () {
		for (int i = 0 ; i < 1024 ; i++){
			Instantiate(mapblock,new Vector3(50.0f,50.0f,0.0f),Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
