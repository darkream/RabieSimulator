using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Ontest_walksample : MonoBehaviour {

public Text data;
//sample area prob
	public float[,] area_prob = new float[32,32];
	//test dog
	public float[,] dog_num = new float[32,32];
	//sample dog prob (3*3 walk sample space)
	public float[,] dog_prob = new float[3,3];
	public float[,] cal_prob = new float[3,3];
	public float[,] new_dog_num = new float[32,32];
	// Use this for initialization
	void Start () {
		//prob assign
		for(int i=0;i<32;i++)
		 {
			 for (int j=0;j<32;j++)
			 {
				area_prob[i,j]=0.0f;
				dog_num[i,j]=0.0f;
				new_dog_num[i,j]=0.0f;
			 }
		 }
		// sample [walk only around first 4*4]
		area_prob[0,0]= 1f;
		area_prob[0,1]=1f;
		area_prob[0,2]=1f;
		area_prob[0,3]=1f;
		area_prob[1,0]=1f;
		area_prob[1,1]=1f;
		area_prob[1,2]=1f;
		area_prob[1,3]=1f;
		area_prob[2,0]=1f;
		area_prob[2,1]=1f;
		area_prob[2,2]=1f;
		area_prob[2,3]=1f;
		area_prob[3,0]=1f;
		area_prob[3,1]=1f;
		area_prob[3,2]=1f;
		area_prob[3,3]=1f;

		//dog unit on [0,0] 1000 dog
		dog_num[0,0]=1000.0f;

		//assign dog prob
		dog_prob[0,0]=1 ;
		dog_prob[0,1]=1;
		dog_prob[0,2]=1;
		dog_prob[1,0]=1;
		dog_prob[1,1]=1f;
		dog_prob[1,2]=1;
		dog_prob[2,0]=1;
		dog_prob[2,1]=1;
		dog_prob[2,2]=1;

		for(int i=0;i<3;i++)
		 {
			 for (int j=0;j<3;j++)
			 {
				cal_prob[i,j]=0.0f;
			 }
		 }
	}
	
	// Update is called once per frame
	void Update () {

		float sumdognum=0;
		for(int i=0;i<4;i++)
		{
			for (int j=0;j<4;j++)
			{
				sumdognum+=dog_num[i,j];
			}
		}

		/* 
		Vector3 mousePosworld = - Vector3.one ;
		Plane plane = new Plane( new Vector3(0.0f,0.0f,-1.0f),new Vector3(0.0f,0.0f,0.0f));
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float disttoplane;
		if (plane.Raycast (ray, out disttoplane))
		{
		 	mousePosworld = ray.GetPoint (disttoplane);
		}
		int blockx = (int)(mousePosworld.x * ((float)1/2));
		int blocky = (int)(mousePosworld.y* ((float)1/2)); 
			
		if(blockx >=0 && blockx<32 && blocky >=0 && blocky<32)
		{
			data.text = "("+blockx+","+blocky+") : ";
		}
		*/

		if (Input.GetMouseButtonDown(0)){ //update sample time frame
		    //
			float all_prob=0;
			for(int i=0;i<32;i++)
		 	{
				for (int j=0;j<32;j++)
			 	{
					if(dog_num[i,j]>0) //walk
			 		{
					//calculate prob
					//dogprob is 3*3 so hacked
					//cal all prob

						if (i==0||j==0)
						

							if(i==0&&j!=0)
							{
								cal_prob[0,0] =0.0f;
								cal_prob[1,0] =dog_prob[1,0] * area_prob[i,j-1];
								cal_prob[2,0] =dog_prob[2,0] * area_prob[i+1,j-1];
								cal_prob[0,1] =0.0f;
								cal_prob[0,2] =0.0f;
							}
							else if(j==0&&i!=0)
							{
								cal_prob[0,0] =0.0f;
								cal_prob[1,0] =0.0f;
								cal_prob[2,0] =0.0f;
								cal_prob[0,1] =dog_prob[0,1] * area_prob[i-1,j];
								cal_prob[0,2] =dog_prob[0,2] * area_prob[i-1,j+1];
							}
							else if(j==0&&i==0)
							{
								cal_prob[0,0] =0.0f;
								cal_prob[1,0] =0.0f;
								cal_prob[2,0] =0.0f;
								cal_prob[0,1] =0.0f;
								cal_prob[0,2] =0.0f;
							}

						}
						else
						{
								cal_prob[0,0] =dog_prob[0,0] * area_prob[i-1,j-1];
								cal_prob[1,0] =dog_prob[1,0] * area_prob[i,j-1];
								cal_prob[2,0] =dog_prob[2,0] * area_prob[i+1,j-1];
								cal_prob[0,1] =dog_prob[0,1] * area_prob[i-1,j];
								cal_prob[0,2] =dog_prob[0,2] * area_prob[i-1,j+1];
						}
						cal_prob[1,1]=dog_prob[1,1] * area_prob[i,j];
						cal_prob[1,2]=dog_prob[1,2] * area_prob[i,j+1];
						cal_prob[2,1]=dog_prob[2,1] * area_prob[i+1,j];
						cal_prob[2,2]=dog_prob[2,2] * area_prob[i+1,j+1];

						for(int a=0;a<3;a++)
						{

							for(int b=0;b<3;b++)
								all_prob +=cal_prob[a,b] ;
						}
					
				
						//assign dog
						new_dog_num [i,j] +=dog_num[i,j];
						if(i>1)
						{
							new_dog_num [i-1,j] += dog_num[i,j]*cal_prob[0,1]/all_prob   ; //midleft
							new_dog_num [i,j] -=dog_num[i,j]*cal_prob[0,1]/all_prob   ;
							new_dog_num [i-1,j+1] += dog_num[i,j]*cal_prob[0,2]/all_prob   ; //topleft
							new_dog_num [i,j] -=dog_num[i,j]*cal_prob[0,2]/all_prob   ;
						}
						if(j>1)
						{
							new_dog_num [i,j-1] += dog_num[i,j]*cal_prob[1,0]/all_prob   ; //downmid
							new_dog_num [i,j] -=dog_num[i,j]*cal_prob[1,0]/all_prob   ;
							new_dog_num [i+1,j-1] += dog_num[i,j]*cal_prob[2,0]/all_prob   ;//downright
							new_dog_num [i,j] -=dog_num[i,j]*cal_prob[2,0]/all_prob   ;
						}
						if(i>1&&j>1)
						{
							new_dog_num [i-1,j-1] += dog_num[i,j]*cal_prob[0,0]/all_prob   ;
							new_dog_num [i,j] -=dog_num[i,j]*cal_prob[0,0]/all_prob   ;
						}
					
						new_dog_num [i,j+1] += dog_num[i,j]*cal_prob[1,2]/all_prob   ;
						new_dog_num [i,j] -=dog_num[i,j]*cal_prob[1,2]/all_prob   ;
						new_dog_num [i+1,j] += dog_num[i,j]*cal_prob[2,1]/all_prob   ;
						new_dog_num [i,j] -=dog_num[i,j]*cal_prob[2,1]/all_prob   ;
						new_dog_num [i+1,j+1] += dog_num[i,j]*cal_prob[2,2]/all_prob   ;
						new_dog_num [i,j] -=dog_num[i,j]*cal_prob[2,2]/all_prob   ;
					}
			 	}
		 	}
			for(int i=0;i<32;i++)
			{
				for (int j=0;j<32;j++)
				{
					dog_num [i,j]=new_dog_num [i,j];
					new_dog_num [i,j] = 0.0f;
				}
			}

		}
	}
	
