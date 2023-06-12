using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public static FOV instance;
    [Range(0, 360)]
    public float viewAngle;
    public float viewRadius;
    public LayerMask targetMask;

    public List<Transform> visibleTarget = new List<Transform>();
    public bool addedTolist = false;

    private void Start()
    {
        instance = this;
        StartCoroutine("FindTargetsWithDelay", .2f);
    }
    private void Update() {

    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }
    void FindVisibleTargets()
    {
        visibleTarget.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        {
            //als de tower een enemy ziet binnen zijn fov, voegt hij de transform daarvan toe aan een lijst
            Transform Target = targetInViewRadius[i].transform;
            Vector3 dirToTarget = (Target.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, Target.position);
                if(!addedTolist){
                    addedTolist = true;
                }
                visibleTarget.Add(Target);
            }
        }
    }
    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    
}
