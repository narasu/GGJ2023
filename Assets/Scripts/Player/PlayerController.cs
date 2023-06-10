using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float fadeDuration = 2f; // Duur van de fade (in seconden)
    public Transform player;
    public float currency = 10;
    public TextMeshProUGUI currencyAmount, noFundsWarning;


    void Awake() => Instance = this;
    private void Start()
    {
    }

    private void Update()
    {
        currencyAmount.text = currency.ToString();
    }

    public void NoFund()
    {
        noFundsWarning.gameObject.SetActive(true);
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        Color startColor = noFundsWarning.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);
  
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            print(t);
            noFundsWarning.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }
        noFundsWarning.gameObject.SetActive(false);
    }

}
