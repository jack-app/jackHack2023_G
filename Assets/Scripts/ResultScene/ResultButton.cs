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
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        text.text = "Your score is " + score;
    }

    public void PushRetry()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void PushReturn()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void PushCapture()
    {

    }
}
