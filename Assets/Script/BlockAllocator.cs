using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAllocator : MonoBehaviour {
	public GameObject blocks;
	public GameObject grid;
	//public Griddata gridobj;
	Griddrawing gridmap;
	public Griddata griddata;
	private int xsize;
	private int ysize;
	private int maxsize;
	private Color blockcolor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void blockAllocatorInitialization(){
		gridmap = grid.GetComponent<Griddrawing>();
		griddata = grid.GetComponent<Griddata>();
		griddata.setIntensityAndIncline();
		ysize = gridmap.verticalgridnum;
		xsize = gridmap.horizongridnum;
		maxsize = gridmap.maxgridnum;

		for (int y = 0 ; y < ysize ; y ++){
			for (int x = 0 ; x < xsize ; x ++){
				float textureScaling = 0.1f + (0.9f * (griddata.intensity[x,y] / 100.0f));
				blocks.transform.GetChild((y*xsize)+x).position = new Vector3(((float)maxsize / xsize) * x,((float)maxsize / ysize) * y,0.0f);
				blocks.transform.GetChild((y*xsize)+x).localScale = new Vector3(5.0f / xsize, 5.0f / ysize,1.0f);
				blockcolor = new Color(textureScaling,textureScaling,textureScaling,textureScaling) ;
				blocks.transform.GetChild((y*xsize)+x).GetComponent<SpriteRenderer>().color = blockcolor;
			}
		}
	}
}
