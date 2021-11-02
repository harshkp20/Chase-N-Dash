using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteInstantiate : MonoBehaviour
{
    public GameObject Plane1,Plane2,Plane3;
    public int is_On_Plane;
    public GameObject Player;
    public float Distance;
    public GameObject presentPlane;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        is_On_Plane = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var ray = new Ray(this.transform.position,-transform.up);
        RaycastHit GroundDetection;
        if((Physics.Raycast(ray,out GroundDetection,Distance)))
        {
            presentPlane = GroundDetection.transform.gameObject;
        }
        if(is_On_Plane==0)
        {
            if(presentPlane.transform.position !=Plane1.transform.position)
            {
                is_On_Plane=2;
            }
        }
        if((presentPlane.transform.position!= Plane1.transform.position) && (is_On_Plane ==1))
        {
            Plane3.transform.Translate(59.76f,0,0);
            is_On_Plane = 2;
        }
        else if((presentPlane.transform.position!= Plane2.transform.position) && (is_On_Plane ==2))
        {
            Plane1.transform.Translate(59.76f,0,0);
            is_On_Plane = 3;
        }
        else if((presentPlane.transform.position!= Plane3.transform.position) && (is_On_Plane ==3))
        {
            Plane2.transform.Translate(59.76f,0,0);
            is_On_Plane = 1;
        }
    }
}
