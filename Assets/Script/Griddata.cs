using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Griddata : MonoBehaviour {

	public GameObject grid;
	Griddrawing gridnumdata;	
	public Text data;

	public int[,] incline = new int[32,32];
	public int[,] intensity = new int[32,32];
	// Use this for initialization
	void Start () {
		gridnumdata = grid.GetComponent<Griddrawing>();

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 mousePosworld = - Vector3.one ;
		Plane plane = new Plane( new Vector3(0.0f,0.0f,-1.0f),new Vector3(0.0f,0.0f,0.0f));
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float disttoplane;
		if (plane.Raycast (ray, out disttoplane))
		{
		 	mousePosworld = ray.GetPoint (disttoplane);
		}

		if (Input.GetMouseButtonDown(0)){
			int blockx = (int)(mousePosworld.x * ((float)gridnumdata.horizongridnum/gridnumdata.maxgridnum));
			int blocky = (int)(mousePosworld.y * ((float)gridnumdata.verticalgridnum/gridnumdata.maxgridnum)); 
			if(blockx >=0 && blockx<=32 && blocky >=0 && blocky<=32)
			{
			data.text = "("+blockx+","+blocky+") : "+intensity[blockx,blocky];
			}
		}

		//data.text = "x : " +   mousePosworld.x +  " Z : " + mousePosworld.z;
	}

	public void setIntensityAndIncline(){
		for(int i = 0; i<32 ;i++)
		{
			for(int j = 0; j<32 ;j++)
			{
				float random = Random.Range(0.0f,1.0f);
				if (random < 0.3f){
					intensity[i,j] = 0;
				}
				else if (random < 0.6f){
					intensity[i,j] = 50;
				}
				else {
					intensity[i,j] = 100;
				}
				incline[i,j] = 20;
			}
		}
	}
}
