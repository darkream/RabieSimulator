using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Samplezeromapgrid : MonoBehaviour {

	

	public int[,] chance = new int[32,32];
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

	//void map ,free roam
	public void setallfree(){
		for(int i = 0; i<32 ;i++)
		{
			for(int j = 0; j<32 ;j++)
			{
				chance[i,j] = 100;
			}
		}
	}
	public void settwentyfree(){
		for(int i = 0; i<32 ;i++)
		{
			for(int j = 0; j<32 ;j++)
			{
				if(i<=20&&j<=20)chance[i,j] = 100;
				else chance[i,j] = 0;
			}
		}
	}
	public void setmapA(){
		for(int i = 0; i<32 ;i++)
		{
			for(int j = 0; j<32 ;j++)
			{
				chance[i,j] = 100;
				if(i>11&&j<11)
				{
				chance[i,j] = 0;
				}

				if(i>=20||j>=20)
				{
				chance[i,j] = 0;
				}
			}
		}
		//map size 20*20
		chance[0,2] = 0;
		chance[1,2] = 0;
		chance[2,2] = 0;
		chance[3,2] = 0;
		chance[4,2] = 0;
		chance[0,3] = 0;
		chance[1,3] = 0;
		chance[2,3] = 0;
		chance[3,3] = 0;
		chance[4,3] = 0;
	}

	public void setmapB(){
	for(int i = 0; i<32 ;i++)
		{
			for(int j = 0; j<32 ;j++)
			{
				chance[i,j] = 100;
				if(i==j)
				{
				chance[i,j] = 0;
				}
				if(i>=20||j>=20)
				{
				chance[i,j] = 0;
				}
			}
		}
	//map size 20*20
	chance[0,0] = 100;
	chance[1,1] = 100;
	chance[10,10] = 50;
	chance[20,20] = 100;
	}
	public void setmapC(){
		for(int i = 0; i<32 ;i++)
		{
			for(int j = 0; j<32 ;j++)
			{
				if(i<=20&&j<=20)chance[i,j] = 100;
				else chance[i,j] = 0;
			}
		}
	chance[0,1] = 0;
	chance[1,1] = 0;
	chance[2,1] = 0;
	chance[3,1] = 0;	
	chance[4,1] = 0;
	chance[5,1] = 0;
	chance[6,1] = 0;
	chance[7,1] = 0;
	chance[8,1] = 0;
	chance[8,0] = 0;
	}
}


