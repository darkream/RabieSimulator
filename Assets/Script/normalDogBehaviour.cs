using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalDogBehaviour : MonoBehaviour {
	public normalDogSpreading dogspread;
	// Use this for initialization
	void Start () {
		behaviourInitiation();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void behaviourInitiation(){
		dogspread.normalDogInitiation();
	}

}
