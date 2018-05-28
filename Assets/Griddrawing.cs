using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Griddrawing : MonoBehaviour {

	public GameObject horiobj;
	public GameObject vertiobj;
	private LineRenderer horizonline;
	private LineRenderer verticalline;
	public int maxgridnum = 32;
	public int horizongridnum =1 ;
	public int verticalgridnum =1 ;
	//will need to get map size from object ,but use this for temporary
	private const double mapsize = 32;
	// Use this for initialization

	void Start () {

		if (horizongridnum > maxgridnum){
			horizongridnum = maxgridnum;
		}
		if (verticalgridnum > maxgridnum){
			verticalgridnum = maxgridnum;
		}
		if (horizongridnum < 2){
			horizongridnum = 2;
		}
		if (verticalgridnum < 2){
			verticalgridnum = 2;
		}

		horizonline = horiobj.GetComponent<LineRenderer>();
		verticalline =  vertiobj.GetComponent<LineRenderer>();
		int horigriduse =(horizongridnum*2)-1;
		int vertigriduse =(verticalgridnum*2)-1;
		 // Set some positions
        Vector3[] horipositions = new Vector3[horigriduse];
		Vector3[] vertipositions = new Vector3[vertigriduse];


		//draw like this
		//	_ _ _ _ _					 			_ _ _ _ _
		// |		 |   			   |   		   |	|	 |
		// |		 | boarder +  	   |        =  |	|	 |
		// |		 |			       |   		   |	|	 |
		// |_ _ _ _ _|				_ _|   		   |_ _ | _ _|


		//horizontal divide
		double HorizontalGridSize= mapsize / (float)horizongridnum ; //gridnumber
		//vertical divide
		double VerticalGridSize= mapsize / (float)verticalgridnum ; //gridnumber
		int i;
		//horizontal divider draw 
		horipositions[0] = new Vector3(0.0f, 0.0f, 0.0f);
		
		for( i = 1;i<horigriduse;i++)
		{
 			//positions[0] is 0 0 0
			if(i % 2 != 0) //  draw _ line  left to right
			{
			 horipositions[i] = new Vector3 (horipositions[i-1].x+(float)HorizontalGridSize , horipositions[i-1].y , 0.0f  );
			}
			 if(i % 2 == 0) //  draw | line 
			{
					 if((i/2) % 2 != 0) // down to top
					{
						horipositions[i] = new Vector3 (horipositions[i-1].x , horipositions[i-1].y + (float)mapsize, 0.0f ); //should be hiegh map size
					}
					 else if((i/2) % 2 == 0)// top to down
					 {
						horipositions[i] = new Vector3 (horipositions[i-1].x , horipositions[i-1].y -(float)mapsize,0.0f );//should be width map size
					 }
			}	
		}


		//vertical divider draw 
		vertipositions[0] = new Vector3(0.0f, 0.0f, 0.0f);

		for( i = 1;i<vertigriduse;i++)
		{
 			//positions[0] is 0 0 0
			if(i % 2 != 0) //  draw | line down to top
			{
			 vertipositions[i] = new Vector3 (vertipositions[i-1].x , vertipositions[i-1].y+(float)VerticalGridSize,0.0f   );
			}
			 if(i % 2 == 0) //  draw _ line 
			{
					 if((i/2) % 2 != 0) // left to right
					{
						vertipositions[i] = new Vector3 (vertipositions[i-1].x + (float)mapsize, vertipositions[i-1].y,0.0f  ); //should be hiegh map size
					}
					 else if((i/2) % 2 == 0)// right to left
					 {
						vertipositions[i] = new Vector3 (vertipositions[i-1].x- (float)mapsize , vertipositions[i-1].y,0.0f  );//should be width map size
					 }
			}	
		}

        horizonline.positionCount = horipositions.Length;
		verticalline.positionCount = vertipositions.Length;
		horizonline.SetPositions(horipositions);
		verticalline.SetPositions(vertipositions);
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
