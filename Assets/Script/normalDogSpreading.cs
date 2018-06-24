using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalDogSpreading : MonoBehaviour {
	const int SINGLE = 0;
	const int HORDE = 1;
	const int EXPLORE = 2;
	const int FLEE = 3;
	public BlockAllocator blockallocator;
	public GameObject dogs;
	public float groupsize;
	public List<int> locx, locy;
	private List<int> role, groupid;
	public int[,] dogamount;
	private Griddrawing gridmap;
	private int xsize, ysize, maxsize;
	private int[] coord;
	// Use this for initialization
	void Start () {
		normalDogInitiation();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("x")){
			dogMove();
		}
		if (blockallocator.griddata.mouseTriggerToMap()){
			coord = blockallocator.griddata.getMouseTriggerToMap();
			blockallocator.griddata.lowertext.text = "#Dog: " + dogamount[coord[0],coord[1]];
		}
	}

	public void normalDogInitiation(){
		blockallocator.blockAllocatorInitialization();
		gridmap = blockallocator.grid.GetComponent<Griddrawing>();

		assignInitialDogToMap();
	}

	void assignInitialDogToMap(){
		ysize = gridmap.verticalgridnum;
		xsize = gridmap.horizongridnum;
		dogamount = new int[xsize,ysize];

		//Initialize dogamount
		for (int y = 0 ; y < ysize ; y++){
			for (int x = 0 ; x < xsize ; x++){
				dogamount[x,y] = 0;
			}
		}

		//Initialize role and add amount
		role = new List<int>();
		for (int i = 0 ; i < locx.Count ; i++){
			role.Add(SINGLE);
			dogamount[locx[i], locy[i]] += 1;
			dogs.transform.GetChild(i).position = blockallocator.blocks.transform.GetChild((locy[i]*xsize)+locx[i]).position;
			dogs.transform.GetChild(i).localScale = blockallocator.blocks.transform.GetChild((locy[i]*xsize)+locx[i]).localScale;
		}

		//Initialize group and connection
		groupid = new List<int>();
		//still does nothing in here
	}

	void dogMove(){
		for (int i = 0 ; i < role.Count ; i++){
			int[] possloc = {locx[i] + 1, locy[i], locx[i] - 1, locy[i], //left or right
							locx[i], locy[i] + 1, locx[i], locy[i] - 1, //up or down
							locx[i], locy[i]}; //no movement

			List<float> chances = new List<float>();
			List<int> selectedx = new List<int>();
			List<int> selectedy = new List<int>();
			for (int alloc = 0 ; alloc < possloc.Length / 2 ; alloc++){
				int dirx = possloc[2*alloc];
				int diry = possloc[(2*alloc) + 1];
				if (dirx >= 0 && diry >= 0 && dirx < xsize && diry < ysize){
					float thischance = 100.0f;
					thischance *= (100.0f - blockallocator.griddata.intensity[dirx, diry]);
					chances.Add(thischance);
					selectedx.Add(dirx);
					selectedy.Add(diry);
				}
			}
			int selectedpath = primitiveRandomMovement(chances);
			dogamount[locx[i], locy[i]]--;
			if (dogamount[locx[i], locy[i]] == 0){
				dogs.transform.GetChild(i).position = blockallocator.blocks.transform.GetChild((selectedy[selectedpath] * xsize)+ selectedx[selectedpath]).position;
			}
			locx[i] = selectedx[selectedpath];
			locy[i] = selectedy[selectedpath];
			dogamount[locx[i], locy[i]]++;
		}
	}

	int primitiveRandomMovement(List<float> chances){
		float maxchances = 0.0f;
		for (int i = 0 ; i < chances.Count ; i++){
			maxchances += chances[i];
		}
		float selectedChance = Random.Range(0.0f, maxchances);
		
		for (int i = 0 ; i < chances.Count ; i++){
			selectedChance -= chances[i];
			if (selectedChance < 0)
				return i;
		}
		return -1;
	}
}
