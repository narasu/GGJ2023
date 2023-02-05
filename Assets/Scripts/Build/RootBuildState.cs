using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootEmptyState : State<RootCreator>
{
    protected FSM<RootCreator> owner;
    LineRenderer lineRenderer;
    public RootEmptyState(FSM<RootCreator> _owner)
    {
        owner = _owner;
        lineRenderer = owner.pOwner.lineRenderer;
    }

    public override void OnEnter()
    {
        owner.pOwner.lineRenderer.positionCount = 0;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (!DayCycle.Instance.isNight)
        {
            
            owner.SwitchState(typeof(RootFightState));
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, owner.pOwner.rootSpawnArea))
        {
            if (Input.GetMouseButtonDown(0))
            {
                lineRenderer.positionCount = 2;
                Vector3 spawnPoint = new Vector3(ray.GetPoint(hit.distance).x, 0f, ray.GetPoint(hit.distance).z);
                lineRenderer.SetPosition(0, spawnPoint);
                owner.SwitchState(typeof(RootEditState));
            }

        }
    }
}

public class RootEditState : State<RootCreator>
{
    protected FSM<RootCreator> owner;
    LineRenderer lineRenderer;

    bool canPlace;

    public RootEditState(FSM<RootCreator> _owner)
    {
        owner = _owner;
        lineRenderer = owner.pOwner.lineRenderer;
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        

        if (Physics.Raycast(ray, out hit, 100f, owner.pOwner.playingField))
        {
            Vector3 p1 = lineRenderer.GetPosition(0);
            Vector3 p2 = ray.GetPoint(hit.distance);
            Vector3 dir = (p2 - p1).normalized;

            Vector3 placePoint = new Vector3(ray.GetPoint(hit.distance).x, p1.y, ray.GetPoint(hit.distance).z);

            lineRenderer.sharedMaterial = owner.pOwner.lineMaterials[0];
            canPlace = true;

            RaycastHit obstacleHit;
            if (Physics.Raycast(ray, out obstacleHit, 100f, owner.pOwner.obstacle))
            {
                //Debug.Log(obstacleHit.collider.gameObject);
                lineRenderer.sharedMaterial = owner.pOwner.lineMaterials[1];
                canPlace = false;
            }
            if (Physics.Raycast(p1, dir, (p2-p1).magnitude, owner.pOwner.obstacle))
            {
                lineRenderer.sharedMaterial = owner.pOwner.lineMaterials[1];
                canPlace = false;
            }
            if (owner.pOwner.rootPoints < owner.pOwner.rootPointCost)
            {
                lineRenderer.sharedMaterial = owner.pOwner.lineMaterials[1];
                canPlace = false;
            }


            if ((p2 - p1).magnitude > owner.pOwner.maxLengthPerRoot)
            {

                Vector3 correctedPosition = p1 + (dir * owner.pOwner.maxLengthPerRoot);
                correctedPosition.y = 0f;

                lineRenderer.SetPosition(1, correctedPosition);
            }
            else
            {

                lineRenderer.SetPosition(1, placePoint);
            }
            
            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                GameObject root = owner.pOwner.PlaceRoot();
                owner.pOwner.rootList.Add(root);
                owner.SwitchState(typeof(RootEmptyState));

            }
            if (Input.GetMouseButtonDown(1))
            {
                owner.SwitchState(typeof(RootEmptyState));
            }

        }

    }
}

public class RootFightState : State<RootCreator>
{
    protected FSM<RootCreator> owner;
    public RootFightState(FSM<RootCreator> _owner)
    {
        owner = _owner;
    }

    public override void OnEnter()
    {
        Debug.Log("daytime has arrived");
    }

    public override void OnUpdate()
    {

        if (DayCycle.Instance.isNight)
        {
            Debug.Log("night comes");
            
            owner.SwitchState(typeof(RootEmptyState));
        }

        
    }

    public override void OnExit()
    {
        owner.pOwner.rootPoints += owner.pOwner.rootPoints += owner.pOwner.rootPointIncreasePerRound;
    }
}