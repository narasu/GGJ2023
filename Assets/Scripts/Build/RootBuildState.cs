using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootEmptyState : State<RootCreator>
{
    protected FSM<RootCreator> owner;
    static public bool SwitchState = false;
    LineRenderer lineRenderer;
    public RootEmptyState(FSM<RootCreator> _owner)
    {
        owner = _owner;
        lineRenderer = owner.pOwner.lineRenderer;
    }

    public override void OnEnter()
    {
        owner.pOwner.lineRenderer.positionCount = 0;
        Debug.Log("emptystate");
        if (PlayerController.Instance == null)
        {
            //do nothing
        }
        else if (PlayerController.Instance.currency == 0)
        {
            owner.SwitchState(typeof(NoFunds));
        }
    }

    public override void OnUpdate()
    {

        base.OnUpdate();

        if (SwitchState == true)
        {
            owner.SwitchState(typeof(BuildDefenses));
            SwitchState = false;
        }

        if (!DayCycle.Instance.isNight)
        {
            owner.SwitchState(typeof(RootFightState));
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        //op de roots zitten belzier curves die onder de rootspawnarea layermask vallen. bij deze raycast wordt gecheckts of de muis op die layer mask zit en zo ja dan kan je een
        // root spawnen op dat oppervlak
        if (Physics.Raycast(ray, out hit, 100f, owner.pOwner.rootSpawnArea))
        {
            //"0" staat voor primairy mouse button, 1 is je secondairy en 2 is je middle mouse button
            if (Input.GetMouseButtonDown(0))
            {
                lineRenderer.positionCount = 2;
                Vector3 spawnPoint = new Vector3(ray.GetPoint(hit.distance).x, 0f, ray.GetPoint(hit.distance).z);
                lineRenderer.SetPosition(0, spawnPoint);
                owner.SwitchState(typeof(RootEditState));
            }

        }
    }
    static public void ChangeState()
    {
        RootEmptyState.SwitchState = true;
    }
    private void OnDestroy()
    {
        lineRenderer = null;
        owner = null;
    }
}

public class BuildDefenses : State<RootCreator>
{
    protected FSM<RootCreator> owner;
    static public bool SwitchState = false;

    public override void OnEnter()
    {
        Debug.Log("builddefenses");
    }
    //dit is hetzelfde als een awake funciton.
    public BuildDefenses(FSM<RootCreator> _owner)
    {
        owner = _owner;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        //Debug.Log("were building defenses baby");
        if (PlayerController.Instance.currency <= 0)
        {
            owner.SwitchState(typeof(NoFunds));
        }

        if (!DayCycle.Instance.isNight)
        {
            owner.SwitchState(typeof(RootFightState));
        }

        //maak een var die de positie van de mouse trackt.
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //maak een raycasthit var die hit heet
        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit, 100f, owner.pOwner.Tower))
        {
            if (Physics.Raycast(ray, out hit, 100f, owner.pOwner.rootSpawnArea))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //als de player de muisknop indrukt,
                    //gaat er een bepaalde hoeveelheid punten van zn currency af
                    ///plaatst het script een building
                    /// wordt deze building aan de lijst toe
                    PlayerController.Instance.currency -= 1;
                    //geef bij 1 het nr in van het gebouw dat je gaat bouwen.
                    GameObject building = owner.pOwner.PlaceBuilding(1, hit.point);
                    owner.pOwner.buildingsList.Add(building);
                    //                Debug.Log("NU MAAK JE EEN GEBOUW DIE KAN DEFENDEN");
                }
            }
        }
    }

    static public void ChangeState()
    {
        BuildDefenses.SwitchState = true;
    }
    private void OnDestroy()
    {
        owner = null;
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

    public override void OnExit()
    {
        base.OnExit();
        PlayerController.Instance.currency -= 1;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        //logic voor mouse input
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
            if (Physics.Raycast(p1, dir, (p2 - p1).magnitude, owner.pOwner.obstacle))
            {
                lineRenderer.sharedMaterial = owner.pOwner.lineMaterials[1];
                canPlace = false;
            }



            if ((p2 - p1).magnitude > owner.pOwner.maxLength)
            {

                Vector3 correctedPosition = p1 + (dir * owner.pOwner.maxLength);
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
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    lineRenderer.SetPosition(0, placePoint);
                }
                else
                {
                    owner.SwitchState(typeof(RootEmptyState));
                }
            }

        }

    }
    private void OnDestroy()
    {
        lineRenderer = null;
        owner = null;
    }
}

public class RootFightState : State<RootCreator>
{
    protected FSM<RootCreator> owner;
    static public bool switchState = false;
    public RootFightState(FSM<RootCreator> _owner)
    {
        owner = _owner;
    }

    public override void OnUpdate()
    {
        if (Building1.instance != null)
            for (int i = 0; i < PauseMenu.Instance.towers.Count; i++)
            {
                PauseMenu.Instance.towers[i].gameObject.GetComponent<Buildings>().CheckForEnemies();
            }

        if (DayCycle.Instance.isNight)
        {
            //Debug.Log("night comes");
            owner.SwitchState(typeof(RootEmptyState));
        }


    }
    private void OnDestroy()
    {
        owner = null;
    }
}

public class NoFunds : State<RootCreator>
{
    protected FSM<RootCreator> owner;

    public NoFunds(FSM<RootCreator> _owner)
    {
        owner = _owner;
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //maak een raycasthit var die hit heet
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f, owner.pOwner.playingField))
        {
            if (Input.GetMouseButtonDown(0))
            {
                PlayerController.Instance.NoFund();
            }
        }
        if (!DayCycle.Instance.isNight)
        {
            owner.SwitchState(typeof(RootFightState));
        }
    }
    private void OnDestroy()
    {
        owner = null;
    }
}