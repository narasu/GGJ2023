using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RootCreator : MonoBehaviour
{
    LineRenderer lineRenderer;
    public LayerMask mask;
    public GameObject rootPrefab;
    int currentNode;

    List<GameObject> rootList = new List<GameObject>();

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        currentNode = 0;
        


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
                    rootList.Add(root);
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
        GameObject newRoot = Instantiate(rootPrefab);
        Vector3 p1 = lineRenderer.GetPosition(0);
        Vector3 p2 = lineRenderer.GetPosition(1);
        Vector3 delta = p2 - p1;

        newRoot.transform.rotation = Quaternion.LookRotation(delta);
        newRoot.transform.Rotate(-90, 0, 0);
        newRoot.transform.position = (p1 + p2) / 2f;
        Vector3 scale = newRoot.transform.localScale;
        scale.y = delta.magnitude / 2f;
        newRoot.transform.localScale = scale;

        return newRoot;
    }
}
