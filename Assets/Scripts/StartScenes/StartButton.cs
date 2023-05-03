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
    public GameObject panel;
    public GameType gameType = GameType.easy;

    public void PushStart()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void PushSelect()
    {
        panel.gameObject.SetActive(true);
    }

    public void PushEasy()
    {
        panel.gameObject.SetActive(false);
        gameType = GameType.easy;
    }
}
