using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.ComponentModel;

public class ResultButton : MonoBehaviour
{
    public Camera camera;
    public Canvas resultCanvas;
    public Canvas photoCanvas;
    public Canvas backCanvas;
    public Canvas backViewCanvas;
    public TextMeshProUGUI text;
    public GameObject sceneChangePanel;
    public GameType gameType = GameType.easy;
    public BackGround backGround;
    private int score;
    private Image fadeAlpha;
    private float alpha;
    private bool fadeinFlag = true;
    private bool fadeoutFlag = false;
    private string nextScene;


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
        backGround.moveable = false;
        backGround.parent.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(false);
        backCanvas.gameObject.SetActive(false);
        backViewCanvas.gameObject.SetActive(false);
        photoCanvas.gameObject.SetActive(true);
        camera.depth = -2;
    }
    public void finishCapture()
    {
        backGround.moveable = true;
        backGround.parent.gameObject.SetActive(true);
        photoCanvas.gameObject.SetActive(false);
        resultCanvas.gameObject.SetActive(true);
        backCanvas.gameObject.SetActive(true);
        backViewCanvas.gameObject.SetActive(true);
        camera.depth = 0;
    }
}
