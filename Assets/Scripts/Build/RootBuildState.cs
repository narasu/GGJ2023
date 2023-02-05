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



            if ((p2 - p1).magnitude > owner.pOwner.maxLength)
            {
                
                lineRenderer.SetPosition(1, p1 + (dir * owner.pOwner.maxLength));
            }
            else
            {

                lineRenderer.SetPosition(1, ray.GetPoint(hit.distance));
            }
            
            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                GameObject root = owner.pOwner.PlaceRoot();
                owner.pOwner.rootList.Add(root);
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    lineRenderer.SetPosition(0, ray.GetPoint(hit.distance));
                }
                else
                {
                    owner.SwitchState(typeof(RootEmptyState));
                }

            }

        }

    }
}