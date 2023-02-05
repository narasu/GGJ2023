using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootEmptyState : State<RootCreator>
{
    protected FSM<RootCreator> owner;
    LineRenderer lineRenderer;
    Transform tree;
    public RootEmptyState(FSM<RootCreator> _owner)
    {
        owner = _owner;
        lineRenderer = owner.pOwner.lineRenderer;
        tree = owner.pOwner.tree;
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
        if (Physics.Raycast(ray, out hit, 100f, owner.pOwner.playingField))
        {
            if (Input.GetMouseButtonDown(0) && Vector3.Distance(ray.GetPoint(hit.distance), tree.position) < 5f)
            {
                lineRenderer.positionCount = 2;
                lineRenderer.SetPosition(0, ray.GetPoint(hit.distance));
                owner.SwitchState(typeof(RootEditState));
            }

        }
    }
}

public class RootEditState : State<RootCreator>
{
    protected FSM<RootCreator> owner;
    LineRenderer lineRenderer;
    Transform tree;

    public RootEditState(FSM<RootCreator> _owner)
    {
        owner = _owner;
        lineRenderer = owner.pOwner.lineRenderer;
        tree = owner.pOwner.tree;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log(owner.pOwner.maxLength);
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
            //Debug.Log((p2 - p1).magnitude);

            if ((p2 - p1).magnitude > owner.pOwner.maxLength)
            {
                
                lineRenderer.SetPosition(1, p1 + (dir * owner.pOwner.maxLength));
            }
            else
            {

                lineRenderer.SetPosition(1, ray.GetPoint(hit.distance));
            }
            
            if (Input.GetMouseButtonDown(0))
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