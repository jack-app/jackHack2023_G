using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    private int score;
    public TextMeshProUGUI text;
    public GameObject sceneChangePanel;
    private Image fadeAlpha;
    private float alpha;
    private bool fadeinFlag = true;
    private bool fadeoutFlag = false;
    private string nextScene;
    public GameType gameType = GameType.easy;


    void Start()
    {
        fadeAlpha = sceneChangePanel.GetComponent<Image>();
        alpha = fadeAlpha.color.a;
        score = 0;
        text.text = score.ToString();
    }

    void Update()
    {
        Fade();
    }

    private void Fade()
    {
        if (fadeinFlag)
        {
            alpha -= 0.02f;
            if (alpha <= 0)
            {
                fadeinFlag = false;
                alpha = 0;
            }
            fadeAlpha.color = new Color(0, 0, 0, alpha);
        }
        else if (fadeoutFlag)
        {

            alpha += 0.02f;
            if (alpha >= 1)
            {
                SceneManager.LoadScene(nextScene);
                fadeoutFlag = false;
                alpha = 1;
            }
            fadeAlpha.color = new Color(0, 0, 0, alpha);
        }
    }

    public void PushRetry()
    {
        fadeoutFlag = true;
        nextScene = "GameScene";
    }
    public void PushReturn()
    {
        fadeoutFlag = true;
        nextScene = "StartScene";
    }
    public void PushCapture()
    {

    }
}
