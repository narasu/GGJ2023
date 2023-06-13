using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayCycle : MonoBehaviour
{
    public static DayCycle Instance;
    [SerializeField] private Material _skybox;
    [SerializeField] private Light _directionalLighting;
    [SerializeField] private GameObject _spotLight;
    [SerializeField] private FmodGameScene _backgroundMusic;
    [SerializeField] private FmodAmbience _ambience;
    private float _timeScale = 2.5f, _fadeTiming = 2f;
    private float currentExposure { get; set; }
    private float _elapsedTime = 0f;
    private readonly int _rotation = Shader.PropertyToID("_Rotation");
    private readonly int _exposure = Shader.PropertyToID("_Exposure");

    public bool isNight = false;

    private void Awake()
    {
        Instance = this;
        StartCoroutine(SwitchToNight());
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        _skybox.SetFloat(_rotation, _elapsedTime * _timeScale);
        //_skybox.SetFloat(_exposure, Mathf.Clamp(Mathf.Sin(_elapsedTime), 0.15f, 1f));
    }

    //logic voor het dag en nacht switchen
    public IEnumerator SwitchToDay()
    {
        //zet de fmod parameter zodat we de audio switchen naar nacht modus
        _backgroundMusic._switch = 1;
        //_ambience.Dag();
        _spotLight.SetActive(false);
        float localElapsedTime = 0f;
        while (localElapsedTime < _fadeTiming)
        {
            //dit wordt gebruikt als blend time bij alle lerps
            localElapsedTime += Time.deltaTime;
            //de current exposure is de exposure van de light box, die wordt in dit geval hoger gemaakt zodat het dag lijkt.
            currentExposure = Mathf.Lerp(0.15f, 0.6f, Mathf.Clamp01(localElapsedTime / _fadeTiming));
            //hier wordt de exposure van de skybox ook toegepast op de skybox die we hebben aangewezen in de inspector
            _skybox.SetFloat(_exposure, currentExposure);

            //de color temp is van de directional lighting in de scene. die wordt in dit geval warmer gemaakt omdat het light overdag een stuk warmer is dan s'nachts
            //om een of ondere reden hebben ze besloten om color temp in unity compleet omgekeerd te doen van het echte leven
            //bij normale camera's zou een hogere whitebalance een warmer beeld geven, maar in dit geval is een hogere waarde een kouder beeld
            _directionalLighting.colorTemperature = Mathf.Lerp(7000, 4000, Mathf.Clamp01(localElapsedTime / _fadeTiming));
            //de intesity is hoeveel light er in de scene is, word nu omhoog gedaan omdat dit natuurlijk meer is tijdens de dag
            _directionalLighting.intensity = Mathf.Lerp(.5f, 3f, Mathf.Clamp01(localElapsedTime / _fadeTiming));
            //aan het eind van iedere frame word al deze info in de game geupdate en daarna weer herhaald.
            yield return new WaitForEndOfFrame();
        }
    }
    public IEnumerator SwitchToNight()
    {
        _backgroundMusic._switch = 0;
       // _ambience.Nacht();
        float localElapsedTime = 0f;
        while (localElapsedTime < _fadeTiming)
        {
            localElapsedTime += Time.deltaTime;
            currentExposure = Mathf.Lerp(0.6f, 0.15f, Mathf.Clamp01(localElapsedTime / _fadeTiming));
            _directionalLighting.colorTemperature = Mathf.Lerp(4000, 7000, Mathf.Clamp01(localElapsedTime / _fadeTiming));
            _directionalLighting.intensity = Mathf.Lerp(3f, .5f, Mathf.Clamp01(localElapsedTime / _fadeTiming));
            _skybox.SetFloat(_exposure, currentExposure);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1);
        _spotLight.SetActive(true);
    }
}

