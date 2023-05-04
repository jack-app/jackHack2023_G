using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public enum GameType
{
    easy
}

public class StartButton : MonoBehaviour
{
    public GameObject settingPanel;
    public GameObject sceneChangePanel;
    private Image fadeAlpha;
    private float alpha;
    private bool fadeinFlag = true;
    private bool fadeoutFlag = false;
    public GameType gameType = GameType.easy;
    private AudioSource clickSound;

    void Start()
    {
        fadeAlpha = sceneChangePanel.GetComponent<Image>();
        alpha = fadeAlpha.color.a;
        clickSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        Fade();
    }

    private void Fade()
    {
        if(fadeinFlag)
        {
            alpha -= 0.02f;
            if (alpha <= 0)
            {
                fadeinFlag = false;
                alpha = 0;
            }
            fadeAlpha.color = new Color(0,0,0,alpha);
        }
        else if (fadeoutFlag)
        {

            alpha += 0.02f;
            if (alpha >= 1)
            {
                SceneManager.LoadScene("GameScene");
                fadeoutFlag = false;
                alpha = 1;
            }
            fadeAlpha.color = new Color(0, 0, 0, alpha);
        }
    }

    public void PushStart()
    {
        clickSound.PlayOneShot(clickSound.clip);
        fadeoutFlag = true;
    }

    public void PushSelect()
    {
        clickSound.PlayOneShot(clickSound.clip);
        settingPanel.gameObject.SetActive(true);
    }

    public void PushEasy()
    {
        settingPanel.gameObject.SetActive(false);
        gameType = GameType.easy;
    }
}
