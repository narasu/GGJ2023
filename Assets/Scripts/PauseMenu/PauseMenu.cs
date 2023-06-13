using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public Animator DayCycle;
    public GameObject Menu, Endturn, Building;
    //rare plaats om dit te zetten maar ik kon niks beters bedenken
    public List<GameObject> towers = new List<GameObject>();

    public static bool IsPaused = false;

    void Awake() => Instance = this;

    // Start is called before the first frame update
    void Start()
    {
        //laat het menu niet zien als je 
        Menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        //pause game
        Menu.SetActive(true);
        Endturn.SetActive(false);
        Building.SetActive(false);
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        //resume game
        Menu.SetActive(false);
        Endturn.SetActive(true);
        Building.SetActive(true);
        Time.timeScale = 1f;
        IsPaused = false;
    }
    public void MainMenu()
    {
        //start scherm staat op index nr 2, scene Adam staat op nr 1 verander dit in andere scenes
        SceneManager.LoadScene("StartScreen");
    }
    public void Quit()
    {
        //kan niet getest worden in unity
        Application.Quit();
    }

    public void EndNight()
    {
        //set de light naar dag
        global::DayCycle.Instance.isNight = false;
        StartCoroutine(global::DayCycle.Instance.SwitchToDay());
        //EndTurnButton.SetActive(false);
        DayCycle.SetFloat("DayNightCycle", 1);
        //BuildDefenses.ChangeState();
        //NoFunds.ChangeState();
    }

    public void StartNight()
    {
        global::DayCycle.Instance.isNight = true;
        StartCoroutine(global::DayCycle.Instance.SwitchToNight());
        //animatie om nacht te starten staat in waveSpawner.cs
    }
    public void StartBuilding()
    {
        RootEmptyState.ChangeState();
    }
}
