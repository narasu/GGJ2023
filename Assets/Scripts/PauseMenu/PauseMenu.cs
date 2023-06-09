using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public Animator DayCycle;
    public GameObject Menu;
    public GameObject EndTurnButton;
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
        Time.timeScale = 0f;
        IsPaused = true;
    }

    public void ResumeGame()
    {
        //resume game
        Menu.SetActive(false);
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

        //EndTurnButton.SetActive(false);
        DayCycle.SetFloat("DayNightCycle", 1);
        BuildDefenses.ChangeState();
    }

    public void StartNight()
    {
        global::DayCycle.Instance.isNight = true;
        //animatie om nacht te starten staat in waveSpawner.cs
    }
    public void StartBuilding()
    {
        RootEmptyState.ChangeState();
    }
}
