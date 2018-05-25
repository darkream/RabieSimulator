using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    public GameObject line;

    public GameObject origin;
    public GameObject destination;

    private void Start()
    {
        
    }

    private void Update()
    {
        Vector3 target = destination.transform.position - origin.transform.position;
        line.GetComponent<LineRenderer>().SetPosition(1 , origin.transform.position);

    }
}
