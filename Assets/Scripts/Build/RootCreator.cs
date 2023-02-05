using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(LineRenderer))]
public class RootCreator : MonoBehaviour
{

    [HideInInspector] public LineRenderer lineRenderer;
    public Material[] lineMaterials;
    public LayerMask rootSpawnArea;
    public LayerMask playingField;
    public LayerMask obstacle;
    public GameObject rootPrefab;
    //sound vars
    public FMODUnity.StudioEventEmitter bgMusic;
    public FMODUnity.StudioEventEmitter place_root;
    public FMODUnity.StudioEventEmitter getting_day;
    public FMODUnity.StudioEventEmitter getting_night;
    public FMODUnity.StudioEventEmitter day_amb;
    public FMODUnity.StudioEventEmitter night_amb;

    public TMP_Text pointText;

    [Min(0)]
    public int rootPoints = 50;

    [Min(0)]
    public float maxLengthPerRoot = 25f;

    [Min(0)]
    public int rootPointCost = 10;

    [Min(0)]
    public int rootPointIncreasePerRound = 20;


    FSM<RootCreator> fsm;

    public List<GameObject> rootList = new List<GameObject>();
    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

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
        pointText.text = "RP: " + rootPoints.ToString();
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

        rootPoints -= rootPointCost;

        return newRoot;
    }

}