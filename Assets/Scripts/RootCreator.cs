using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RootCreator : MonoBehaviour
{
    LineRenderer lineRenderer;
    public LayerMask mask;
    int currentNode;

    List<GameObject> cylinderList = new List<GameObject>();

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        currentNode = 1;
        


        //cylinderList.Add() 
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("PlayingField"))
            {
                lineRenderer.SetPosition(1, ray.GetPoint(hit.distance));
                if (Input.GetMouseButtonDown(0))
                {
                    GameObject root = PlaceRoot();
                    cylinderList.Add(root);
                    lineRenderer.SetPosition(0, ray.GetPoint(hit.distance));
                    currentNode++;
                }
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);

        //    RaycastHit hit2;
        //    if (Physics.Raycast(ray, out hit2))
        //    {
        //        if (hit2.collider.CompareTag("PlayingField"))
        //        {
        //            GameObject o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //            o.transform.position = ray2.GetPoint(hit2.distance);
        //        }
        //    }
        //}
    }

    void StartLine(Vector3 _startingPoint)
    {

    }

    GameObject PlaceRoot()
    {
        GameObject newCylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Vector3 p1 = lineRenderer.GetPosition(0);
        Vector3 p2 = lineRenderer.GetPosition(1);
        Vector3 delta = p2 - p1;
        newCylinder.transform.rotation = Quaternion.LookRotation(delta);
        newCylinder.transform.Rotate(-90, 0, 0);
        newCylinder.transform.position = (p1 + p2) / 2f;
        Vector3 scale = newCylinder.transform.localScale;
        scale.y = delta.magnitude / 2f;
        newCylinder.transform.localScale = scale;

        return newCylinder;
    }
}