using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FmodGameScene : MonoBehaviour
{
    public static FmodGameScene instance;
    private FMOD.Studio.EventInstance fmodInstance;
    public FMODUnity.EventReference backgroundMusic;
    [SerializeField] private Slider slider;
    [Range(0,1)]
    public float vol;

    [SerializeField][Range(0,1)]
    public float _switch = 0;

    private void Start() {
        instance = this;
        fmodInstance = FMODUnity.RuntimeManager.CreateInstance(backgroundMusic);
        fmodInstance.start();
    }
    void Update()
    {
        fmodInstance.setVolume(slider.value);
        fmodInstance.setParameterByName("day_night_switch", _switch);
     }
    public void StopMusic(){
        fmodInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    private void OnDestroy() {
        fmodInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        fmodInstance.release();
    }
}
