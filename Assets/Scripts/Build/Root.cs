using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
    public List<Mesh> meshList = new List<Mesh>();
    int lifetime;
    public float growSpeed = 0.4f;

    Material[] mats;

    private void Start()
    {
        Renderer[] r = GetComponentsInChildren<Renderer>();
        mats = new Material[r.Length];
        for(int i = 0; i < mats.Length; i++)
        {
            mats[i] = r[i].material;
            Debug.Log(mats[i].GetFloat("_grow"));
            mats[i].SetFloat("_grow", 0);
        }
    }

    private void Update()
    {
        for (int i = 0; i < mats.Length; i++)
        {
            if(mats[i].GetFloat("_grow") < 1)
            {
                mats[i].SetFloat("_grow", mats[i].GetFloat("_grow") + growSpeed * Time.deltaTime);
            }
        }
    }

    public void SelectMesh(float _length)
    {
    }
}
