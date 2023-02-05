using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RootCreator : MonoBehaviour
{

    [HideInInspector] public LineRenderer lineRenderer;
    public LayerMask rootSpawnArea;
    public LayerMask playingField;
    public GameObject rootPrefab;
    public Transform tree;

    public float pRootPoints
    {
        get
        {
            return rootPoints;
        }

        set
        {
            rootPoints = value;

            SetMaxLength();
        }
    }
    private float rootPoints = 50f;

    [HideInInspector] public float maxLength;

    private void SetMaxLength()
    {
        maxLength = Mathf.Floor(rootPoints / 10);
    }



    FSM<RootCreator> fsm;

    public List<GameObject> rootList = new List<GameObject>();
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        SetMaxLength();

        fsm = new FSM<RootCreator>();
        fsm.Initialize(this);
        fsm.AddState(new RootEmptyState(fsm));
        fsm.AddState(new RootEditState(fsm));

        
        fsm.SwitchState(typeof(RootEmptyState));
    }


    void Start()
    {
        


        //cylinderList.Add() 
    }

    void Update()
    {
        fsm.Update();

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

    public GameObject PlaceRoot()
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