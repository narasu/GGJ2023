using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FmodAmbience : MonoBehaviour
{
    private FMOD.Studio.EventInstance fmodInstance;
    public FMODUnity.EventReference dag;
    public FMODUnity.EventReference nacht;
    [SerializeField] private Slider slider;


    private void Start()
    {
        fmodInstance = FMODUnity.RuntimeManager.CreateInstance(nacht);
        fmodInstance.start();
    }
    void Update()
    {
        fmodInstance.setVolume(slider.value);
    }
    public void Dag()
    {
        fmodInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        fmodInstance.release();
        fmodInstance = FMODUnity.RuntimeManager.CreateInstance(dag);
        fmodInstance.start();
    }

    public void Nacht()
    {
        fmodInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        fmodInstance.release();
        fmodInstance = FMODUnity.RuntimeManager.CreateInstance(nacht);
        fmodInstance.start();
    }
    private void OnDestroy()
    {
        fmodInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        fmodInstance.release();
    }
}
