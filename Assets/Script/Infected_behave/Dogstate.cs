using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dogstate : MonoBehaviour {
	
	public GameObject thisdog;
	New_method_dogmove dogdata ; //0 is normal 1 is exposed 2 is infected 3 is vaccine
	SpriteRenderer dogsprite;
	int exposedlifespan=0;
	int infectlifespan=0;
	// Use this for initialization
	void Start () {

		dogdata = thisdog.GetComponent<New_method_dogmove>();
		dogsprite = thisdog.GetComponent<SpriteRenderer>(); ;
	}
	
	// Update is called once per frame
	void Update () {
		if(dogdata.dogstate==0)//normal
		{
			dogsprite.color = Color.white;
			if (dogdata.fighting == true)
			{
				dogdata.dogstate=1;
			}
		}
		if(dogdata.dogstate==1)//suscept
		{
			dogsprite.color = Color.yellow;
			if (Input.GetKeyDown("x"))
			{
				exposedlifespan++;
			}
			if (exposedlifespan == 5)
			{
				dogdata.dogstate=2;
			}
		}
		if(dogdata.dogstate==2)//infected
		{
			dogsprite.color = Color.red;
			if (Input.GetKeyDown("x"))
			{
				infectlifespan++;
			}
			if (infectlifespan == -1)
			{
				Destroy(gameObject);
			}
		}
		if(dogdata.dogstate==3)//vaccinate
		{
			dogsprite.color = Color.blue;
			
		}
	}
}
