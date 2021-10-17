using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    public int numOfPaws = 3;
    [SerializeField]
    public Image[] paws;

    [SerializeField]
    public Text gameOverText;
    [SerializeField]
    public Text HPText;
    [SerializeField]
    public Text timerText;

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    void Start()
    {
        gameOverText.gameObject.SetActive(false);
    }

    void Update()
    {

    }

    public IEnumerator GameOverBlinkText()
    {
        while (true)
        {
            gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
