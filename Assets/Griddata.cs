using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct Gridinfo
{
    public int incline, intensity;

    /*public Gridinfo (int p1, int p2)
    {
        incline = p1;
        intensity = p2;
    }*/
}

public class Griddata : MonoBehaviour {

public GameObject grid;
public Camera camera;

private Gridinfo[,] mapinfo;
Griddrawing gridnumdata;

public Text data;


	// Use this for initialization
	void Start () {
		gridnumdata = grid.GetComponent<Griddrawing>();
			mapinfo = new Gridinfo[32,32]; //max grid
			
			for(int i = 0; i<32 ;i++)
			{
				for(int j = 0; j<32 ;j++)
				{
					float random = Random.Range(0.0f,1.0f);
					if (random < 0.3f){
						mapinfo[i,j].intensity = 0;
					}
					else if (random < 0.6f){
						mapinfo[i,j].intensity = 50;
					}
					else {
						mapinfo[i,j].intensity = 100;
					}
					mapinfo[i,j].incline = 0;
				}
			}

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

			data.text = "("+blockx+","+blocky+") : "+mapinfo[blockx,blocky].intensity;
		}

		//data.text = "x : " +   mousePosworld.x +  " Z : " + mousePosworld.z;
	}
}
