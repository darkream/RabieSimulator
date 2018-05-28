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
public Text data;


	// Use this for initialization
	void Start () {
		Griddrawing gridnumdata = grid.GetComponent<Griddrawing>();
			Gridinfo [,] mapinfo = new Gridinfo[32,32]; //max grid
			
			for(int i = 0; i<32 ;i++)
			{
				for(int j = 0; j<32 ;j++)
				{
				mapinfo[i,j].incline = i;
				mapinfo[i,j].intensity = j;
				}
			}

	}
	
	// Update is called once per frame
	void Update () {
	 
	
	
 Vector3 mousePosworld = - Vector3.one ;
	Plane plane = new Plane( new Vector3(0.0f,-1.0f,0.0f),new Vector3(0.0f,0.0f,0.0f));
	Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	float disttoplane;
	if (plane.Raycast (ray, out disttoplane))
	{
		 mousePosworld = ray.GetPoint (disttoplane);
	}
	data.text = "x : " +   mousePosworld.x +  " Z : " + mousePosworld.z;
	
	
	}
}
