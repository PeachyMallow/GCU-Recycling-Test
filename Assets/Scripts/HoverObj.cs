using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverObj : MonoBehaviour
{
    public GameObject Inspection;
    public InspectionObj inspectionObj;
    public int index;

    // Update is called once per frame
    void Update()
    {
        if (Inspection.active)
            return;
        Ray ray = Camera.main.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Color color = GetComponent<MeshRenderer>().material.color;
        if (GetComponent<Collider>().Raycast(ray, out hit, 100f))
        {
            color.a = 0.6f;
            if (Input.GetMouseButtonDown(0))
            {
                Inspection.SetActive(true);
                inspectionObj.TurnOnInspection(index);
            }
        }
        else
        {
            color.a = 1.0f;
        }      
        GetComponent<MeshRenderer>().material.color = color;
    }
}
