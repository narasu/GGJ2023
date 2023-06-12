using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    public float fadeDuration = 2f; // Duur van de fade (in seconden)
    public Transform player;
    public float currency = 1;
    public TextMeshProUGUI currencyAmount, noFundsWarning;


    void Awake() => Instance = this;
    private void Start()
    {
    }

    private void Update()
    {
        currencyAmount.text = currency.ToString();
        print(noFundsWarning.color.a);
    }

    public void NoFund()
    {
        //noFundsWarning.gameObject.SetActive(true);
        StartCoroutine(FadeTextToFullAlpha(.5f, noFundsWarning));
        StartCoroutine(Wait(noFundsWarning));
    }

    public IEnumerator Wait(TMP_Text i)
    {
        print("we faden weer out");
        yield return new WaitForSeconds(.5f);
        StartCoroutine(FadeTextToZeroAlpha(1, i));
    }
    public IEnumerator FadeTextToFullAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TMP_Text i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
}
