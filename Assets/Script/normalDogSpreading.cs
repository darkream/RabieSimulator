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
	private List<int> role, groupid, kcenter;
	public int[,] dogamount;
	private Griddrawing gridmap;
	private int xsize, ysize, maxsize;
	private int[] coord;
	private List<float> chances ;
	private List<int> selectedx ;
	private List<int> selectedy ;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("x")){
			dogMove();
		}
		if (blockallocator.griddata.mouseTriggerToMap()){
			coord = blockallocator.griddata.getMouseTriggerToMap();
			blockallocator.griddata.lowertext.text = "#Dog: " + dogamount[coord[0],coord[1]];
			blockallocator.griddata.uppertext.text = "#Group ID: " + groupid[getIDFrom(coord[0], coord[1])];
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
		groupid = new List<int>();
		for (int i = 0 ; i < locx.Count ; i++){
			role.Add(SINGLE);
			groupid.Add(0);
			dogamount[locx[i], locy[i]] += 1;
			dogs.transform.GetChild(i).position = blockallocator.blocks.transform.GetChild((locy[i]*xsize)+locx[i]).position;
			dogs.transform.GetChild(i).localScale = blockallocator.blocks.transform.GetChild((locy[i]*xsize)+locx[i]).localScale;
		}

		//Initialize group and connection
		int initial_k = role.Count / 10;	//10 dogs per group per size of area
		if (initial_k <= 0){
			initial_k = 1;
		}
		for (int i = 0 ; i < 1; i++){
			List<int> temp_groupid = new List<int>();
			groupAssign(randomUniqueNumber(initial_k, 0, role.Count));
		}
	}

	void dogMove(){
		for (int i = 0 ; i < role.Count ; i++){
			int[] possloc = {locx[i] + 1, locy[i], locx[i] - 1, locy[i], //right or left
							locx[i], locy[i] + 1, locx[i], locy[i] - 1, //up or down
							locx[i], locy[i]}; //no movement

			chances = new List<float>();
			selectedx = new List<int>();
			selectedy = new List<int>();

			pathDistribution(possloc, i);

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

	List<int> randomUniqueNumber(int size, int from, int to){
		List<int> unique = new List<int>();
		kcenter = new List<int>();
		for (int i = 0 ; i < size ; i++){
			while (true){
				int currentrandom = Random.Range(from,to);
				if (!unique.Contains(currentrandom)){
					unique.Add(currentrandom);
					kcenter.Add(currentrandom);
					break;
				}
			}
		}
		return unique;
	}

	void groupAssign(List<int> selected_k){
		for (int i = 0 ; i < role.Count ; i++){
			int leastdistance = findDistanceBetween(locx[selected_k[0]], locx[i], locx[selected_k[0]], locy[i]);
			groupid[i] = 1;
			for (int j = 1 ; j < selected_k.Count ; j++){
				int distance = findDistanceBetween(locx[selected_k[j]], locx[i], locx[selected_k[j]], locy[i]);
				if (distance < leastdistance){
					leastdistance = distance;
					groupid[i] = j + 1;
				}
			}
		}
	}

	int findDistanceBetween(int x1, int x2, int y1, int y2){
		int x_dist = (x1 - x2);
		int y_dist = (y1 - y2);
		if (x_dist < 0){
			x_dist *= -1;
		}
		if (y_dist < 0){
			y_dist *= -1;
		}
		return (x_dist * x_dist) + (y_dist * y_dist);
	}

	int getIDFrom(int blockx, int blocky){
		for (int i = 0 ; i < locx.Count ; i++){
			if (locx[i] == blockx && locy[i] == blocky)
				return i;
		}
		return 0;
	}

	void pathDistribution(int[] possloc, int i){
		//Normal Path Distribution
		for (int alloc = 0 ; alloc < possloc.Length / 2 ; alloc++){
			int dirx = possloc[2*alloc];
			int diry = possloc[(2*alloc) + 1];
			if (dirx >= 0 && diry >= 0 && dirx < xsize && diry < ysize){
				float thischance = 100.0f;
				thischance *= (100.0f - blockallocator.griddata.intensity[dirx, diry]);
				//thischance *= behaviourPathDistribution(i, alloc);
				chances.Add(thischance);
				selectedx.Add(dirx);
				selectedy.Add(diry);
			}
		}
	}

	float behaviourPathDistribution(int i, int direction){
		if (role[i] == SINGLE){
			return singleMovementToCenter(i, direction);
		}
		else if (role[i] == HORDE){

		}
		else if (role[i] == EXPLORE){

		}
		else if (role[i] == FLEE){
			if (direction > 2){
				direction -= 2;
			}
			else {
				direction += 2;
			}
			return singleMovementToCenter(i, direction);
		}
		return 0.0f;
	}

	float singleMovementToCenter(int i, int direction){
		int targetindex = kcenter[groupid[i] - 1];
		if (direction == 1 && locx[targetindex] > locx[i]){
			return 1.2f * (locx[targetindex] - locx[i]);
		}
		else if (direction == 2 && locx[targetindex] < locx[i]){
			return 1.2f * (locx[i] - locx[targetindex]);
		}
		else if (direction == 3 && locy[targetindex] > locy[i]){
			return 1.2f * (locy[targetindex] - locy[i]);
		}
		else if (direction == 4 && locy[targetindex] < locy[i]){
			return 1.2f * (locy[i] - locy[targetindex]);
		}
		return 0.0f;
	}
}
