using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RootCreator : MonoBehaviour
{

    [HideInInspector] public LineRenderer lineRenderer;
    public Material[] lineMaterials;
    public LayerMask rootSpawnArea;
    public LayerMask playingField;
    public LayerMask obstacle;
    public GameObject rootPrefab;

    [HideInInspector] public GameObject currentNode;

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

    [SerializeField]
    private float rootPoints = 50f;

    [SerializeField][Range(0, 25)]
    private float pointsToRootLengthDivider = 10f;

    [HideInInspector] public float maxLength;




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
        fsm.AddState(new RootFightState(fsm));


        fsm.SwitchState(typeof(RootEmptyState));
    }


    void Start()
    {
    }

    void Update()
    {
        fsm.Update();

    }

    private void SetMaxLength()
    {
        maxLength = Mathf.Floor(rootPoints / pointsToRootLengthDivider);
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