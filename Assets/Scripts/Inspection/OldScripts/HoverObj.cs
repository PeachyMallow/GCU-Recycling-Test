using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverObj : MonoBehaviour
{
    // sets up inspection canvas, making it visible 
    //public GameObject Inspection;
    // holds inspectionObj script
    public InspectionObj inspectionObj;
    // index of gameobjects (black(0), green(1), & red(2) cubes) 
    public int index;

    //[SerializeField]
    //private Camera mainCam;

    //// distance item should appear from camera
    //[SerializeField]
    //private float offset;

    private void Start()
    {
        //mainCam = Camera.main;
    }

    /// <summary>
    /// Highlights selected object
    /// activates inspection on left mouse click
    /// </summary>
    private void Update()
    {
        //transform.position = mainCam.transform.position + mainCam.transform.forward * offset;

        //Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //Color color = GetComponent<MeshRenderer>().material.color;

        //if (GetComponent<Collider>().Raycast(ray, out hit, 100f))
        //{
        //    //color.a = 0.6f;
        //    if (Input.GetMouseButtonDown(0))
        //    {
                
        //    }
        //}

        //else
        //{
        //    //color.a = 1.0f;
        //}      
        //GetComponent<MeshRenderer>().material.color = color;
    }
}

//old script
//void Update()
//{
//    if (Inspection.activeInHierarchy)
//        return;

//    Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
//    RaycastHit hit;
//    Color color = GetComponent<MeshRenderer>().material.color;

//    if (GetComponent<Collider>().Raycast(ray, out hit, 100f))
//    {
//        color.a = 0.6f;
//        if (Input.GetMouseButtonDown(0))
//        {
//            //Debug.Log(hit.transform.gameObject.name);
//            Inspection.SetActive(true);
//            inspectionObj.TurnOnInspection(index);
//        }
//    }
//    else
//    {
//        color.a = 1.0f;
//    }
//    GetComponent<MeshRenderer>().material.color = color;
//}