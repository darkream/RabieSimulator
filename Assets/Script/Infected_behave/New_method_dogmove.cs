using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;


//using System.Math;
public class New_method_dogmove : MonoBehaviour {

	public int posx,posy;
	public int range;
	public int detectrange=8;
	public GameObject grid;
	public Griddrawing griddata;
	public Samplezeromapgrid mapdata;
	public Sampleblockallocater blockallo;
	public int dogstate ; //0 is normal 1 is infected
	public GameObject otherdog;//use inherit for sample purpose
	New_method_dogmove otherdogdata;
	int [,] realchance= new int[32,32];
	Vector3 targetpos = new Vector3(0.0f,0.0f,0.0f)	;
	// Use this for initialization
	void Start () {
		griddata = grid.GetComponent<Griddrawing>();
		mapdata = grid.GetComponent<Samplezeromapgrid>();
		otherdogdata =otherdog.GetComponent<New_method_dogmove>();
		//mapdata.setallfree();
		//mapdata.setmapA();
		mapdata.setmapB();
		realchanceset();
		blockallo.blockinit();
		 targetpos =new Vector3(posx*32.0f/20.0f,posy*32.0f/20.0f,0.0f);
		transform.position = targetpos;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("x")){
			detect();
			dogMove();
			realchanceset();
			//Debug.Log("x pos :"+posx+"   y pos :"+posy);
		}
		 transform.position = Vector3.MoveTowards(transform.position, targetpos, 0.5f);
	}

	void realchanceset()
	{
		for(int i=0;i<32;i++)
		{
			for(int j=0;j<32;j++)
			{
				realchance[i,j]=mapdata.chance[i,j];
			}
		}
	}

	void detect()//increase chance to attack
	{
		if(dogstate==1)
		{
			//sample , realdeal will use mapdata to track otherdog position
			//use inherit for sample purpose
			int lowerx,higherx,lowery,highery;
			if(posx>=otherdogdata.posx)
			{
				lowerx=otherdogdata.posx;higherx=posx;
			}
			else
			{
				lowerx=posx;higherx=otherdogdata.posx;
			}

			if(posy>=otherdogdata.posy)
			{
				lowery=otherdogdata.posy;highery=posy;
			}
			else
			{
				lowery=posy;highery=otherdogdata.posy;
			}
			
			if(higherx-lowerx+highery-lowery<=detectrange)
			{
				for(int i=lowerx;i<=higherx;i++)
				{
					for(int j=lowery;j<=highery;j++)
					{
						realchance[i,j]*=10;
					}
				}
				
			}
			
			
		}
	}
	void dogMove()
	{
		//collect all chance around dog pos in range
		int moveable=0;
		double collectchance=0;
		for (int i=posx-range;i<posx+range;i++)
		{
			for (int j=posy-range;j<posy+range;j++)
			{
				if(i<0||j<0||i>=griddata.maxgridnum||j>=griddata.maxgridnum)
				{
					//do nothing
				}	
				else
				{
					if (Mathf.Abs(posx-i)+Mathf.Abs(posy-j) <range )
					{
						moveable++;
						collectchance+=realchance[i,j];
					}
				}
			}
		}
		Debug.Log("M"+moveable+"C"+collectchance);

		//now random position to move and moving
		double randompoint,selectchance=0;
		bool getmovepoint=false;
		randompoint = randomizer()*100.0f; //random value between 0-100 with double value
		Debug.Log(randompoint);
		for (int i=posx-range;i<posx+range;i++)
		{
			for (int j=posy-range;j<posy+range;j++)
			{
				if(i<0||j<0||i>=griddata.maxgridnum||j>=griddata.maxgridnum)
				{
					//do nothing
				}	
				else
				{
					if (Mathf.Abs(posx-i)+Mathf.Abs(posy-j) <range )
					{
						selectchance += System.Convert.ToDouble(realchance[i,j])*100.0f/collectchance;
						Debug.Log(selectchance);
						if(selectchance >= randompoint && realchance[i,j]!=0 ) //got point to land
						{
							//moving
							posx=i;
							posy=j;
							//calculate target pos
							targetpos=new Vector3(posx*32.0f/20.0f,posy*32.0f/20.0f,0.0f);
							getmovepoint=true;
							break;
						}
					}
				}
			}
			if (getmovepoint==true) break;
		}
	}

	double randomizer ()
	{
      double number;
      System.Random rnd = new System.Random();
      number = rnd.NextDouble(); //random double value between 0-1
      return number;
	}
}
