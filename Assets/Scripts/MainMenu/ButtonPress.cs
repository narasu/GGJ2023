using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonPress : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("Scene");
    }
    public void PlayCredits(){
        SceneManager.LoadScene("Credits");
    }
}
