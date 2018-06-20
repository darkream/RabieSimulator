using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapdrag : MonoBehaviour {

	  // Update is called once per frame
    public float panSpeed = 2.0f;
    private bool bDragging=false;
    Vector3 oldPos = new Vector3(0.0f,0.0f,0.0f);
    Vector3 panOrigin= new Vector3(0.0f,0.0f,0.0f);

      void Update() {
           if(Input.GetMouseButtonDown(0))
         {
             bDragging = true;
             oldPos = Camera.main.transform.position;
             panOrigin = Camera.main.ScreenToViewportPoint(Input.mousePosition);                    //Get the ScreenVector the mouse clicked
         }
 
         if(Input.GetMouseButton(0))
         {
             Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) - panOrigin;    //Get the difference between where the mouse clicked and where it moved
             Camera.main.transform.position = oldPos + -pos * panSpeed;                                         //Move the position of the camera to simulate a drag, speed * 10 for screen to worldspace conversion
         }
 
         if(Input.GetMouseButtonUp(0))
         {
             bDragging = false;
         }
      }
}
