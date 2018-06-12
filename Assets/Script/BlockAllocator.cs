using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockAllocator : MonoBehaviour {


	public GameObject blocks;
	public GameObject grid;
	//public Griddata gridobj;
	Griddrawing gridmap;
	Griddata griddata;

	int intense;
	private int xsize;
	private int ysize;
	private int maxsize;
	private Color blockcolor;
	// Use this for initialization
	void Start () {
		gridmap = grid.GetComponent<Griddrawing>();
		griddata = grid.GetComponent<Griddata>();
		griddata.setIntensityAndIncline();
		ysize = gridmap.verticalgridnum;
		xsize = gridmap.horizongridnum;
		maxsize = gridmap.maxgridnum;

		for (int i = 0 ; i < ysize ; i ++){
			for (int j = 0 ; j < xsize ; j ++){
				float textureScaling = 0.1f + (0.9f * (griddata.intensity[j,i] / 100.0f));
				//int thisintensity = griddata.mapinfo[j,i].intensity;
				blocks.transform.GetChild((i*xsize)+j).position = new Vector3(((float)maxsize / xsize) * j,((float)maxsize / ysize) * i,0.0f);
				blocks.transform.GetChild((i*xsize)+j).localScale = new Vector3(5.0f / xsize, 5.0f / ysize,1.0f);
				blockcolor = new Color(textureScaling,textureScaling,textureScaling,textureScaling) ;
				blocks.transform.GetChild((i*xsize)+j).GetComponent<SpriteRenderer>().color = blockcolor;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
