using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normalDogSpreading : MonoBehaviour {
	public GameObject dogs;
	public GameObject grid;
	public BlockAllocator blockAllocator;
	private int xsize, ysize, maxsize;
	private Griddata griddata;
	private Griddrawing gridmap;
	public List<int> allocationx, allocationy;
	public List<float> doggroup;
	public float hordesizeperblock;
	private float runtime = 0.0f;

	// Use this for initialization
	void Start () {
		normalDogInitiation();
	}
	
	// Update is called once per frame
	void Update () {
		clickDogDisplay();
		runtime += Time.deltaTime;
		if (runtime > 1.0f){
			runtime = 0.0f;
			dogWalk();
		}
	}

	void normalDogInitiation(){
		blockAllocator.blockAllocatorInitialization();
		gridmap = grid.GetComponent<Griddrawing>();
		griddata = grid.GetComponent<Griddata>();
		ysize = gridmap.verticalgridnum;
		xsize = gridmap.horizongridnum;
		maxsize = gridmap.maxgridnum;
		
		for (int groupid = 0 ; groupid < doggroup.Count ; groupid++){
			dogs.transform.GetChild(groupid).position = blockAllocator.blocks.transform.GetChild((allocationy[groupid]*xsize)+allocationx[groupid]).position;
			dogs.transform.GetChild(groupid).localScale = blockAllocator.blocks.transform.GetChild((allocationy[groupid]*xsize)+allocationx[groupid]).localScale;
		}
	}

	void clickDogDisplay(){
		bool click = griddata.mouseTriggerToMap();
		int[] coord = {-1,-1};
		if (click){
			coord = griddata.getMouseTriggerToMap();
		}

		bool found = false;

		for (int groupid = 0 ; groupid < doggroup.Count ; groupid++){
			bool x_match = (allocationx[groupid] == coord[0]);
			bool y_match = (allocationy[groupid] == coord[1]);

			if (x_match && y_match){
				griddata.data.text += "\nDog amount: " + doggroup[groupid];
				found = true;
			}
		}

		if (click && !found){
			griddata.data.text += "\nNo dog exist";
		}
	}

	void dogWalk(){
		int thisSizeOnly = doggroup.Count;
		for (int groupid = 0 ; groupid < thisSizeOnly ; groupid++){
			int[] loc = {allocationx[groupid], allocationy[groupid] + 1, allocationx[groupid], allocationy[groupid] - 1,
					allocationx[groupid] - 1, allocationy[groupid], allocationx[groupid] + 1, allocationy[groupid]};
			
			float separationsize = doggroup[groupid] / (1.0f + ((loc.Length / 2)*hordesizeperblock));
			int pickedid = -1;
			for (int i = 0 ; i < loc.Length / 2 ; i++){
				int currentx = loc[2*i];
				int currenty = loc[(2*i)+1];
				if (outOfBoundChecking(currentx, currenty)){
					separationsize *= getIntensityReduction(currentx, currenty);
					if (separationsize > 0.5f){
						pickedid = dogEncounter(currentx,currenty);
						if(pickedid == -1){
							addNewDog(currentx, currenty, separationsize);
							dogs.transform.GetChild(doggroup.Count - 1).position = blockAllocator.blocks.transform.GetChild((currenty*xsize)+currentx).position;
							dogs.transform.GetChild(doggroup.Count - 1).localScale = blockAllocator.blocks.transform.GetChild((currenty*xsize)+currentx).localScale;
						}
						else {
							addToExistedDog(pickedid, separationsize);
						}
						doggroup[groupid] -= separationsize;
					}
				}
			}
		}
	}

	int dogEncounter(int x, int y){
		for (int groupid = 0 ; groupid < doggroup.Count ; groupid++){
			if (allocationx[groupid] == x && allocationy[groupid] == y){
				return groupid;
			}
		}
		return -1;
	}

	void addNewDog(int locx, int locy, float dogamount){
		doggroup.Add(dogamount);
		allocationx.Add(locx);
		allocationy.Add(locy);
	}

	void addToExistedDog(int dogid, float dogamount){
		doggroup[dogid] += dogamount;
	}

	float getIntensityReduction(int locx, int locy){
		return 1.0f - (griddata.intensity[locx,locy] / 100.0f);
	}

	bool outOfBoundChecking(int locx, int locy){
		return (locx >= 0 && locy >= 0 && locx < gridmap.horizongridnum && locy < gridmap.verticalgridnum);
	}
}
