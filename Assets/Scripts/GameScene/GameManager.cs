using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro; //TextMeshProを扱う際に必要

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("必要なコンポーネントを登録")]
    // 中心に表示するテキスト
    [SerializeField] TextMeshProUGUI centerText;
    // 収益を表示するテキスト
    [SerializeField] TextMeshProUGUI revenueText;
    // 電気代の進捗を表示するテキスト
    [SerializeField] TextMeshProUGUI electricityBillText;
    // 電気代進捗をを表示するプログレスバー
    [SerializeField] Slider electricityBillSlider;
    // 時間を表示するテキスト
    [SerializeField] TextMeshProUGUI timerText;

    // 増設に必要な建物数の進捗を表示するテキスト
    [SerializeField] TextMeshProUGUI ExtensionCountText;
    // 増設に必要な建物数の進捗を表示するプログレスバー
    [SerializeField] Slider ExtensionCountSlider;
    // 増設にかかるお金の進捗を表示するテキスト
    [SerializeField] TextMeshProUGUI ExtensionCostText;
    // 増設にかかるお金の進捗を表示するプログレスバー
    [SerializeField] Slider ExtensionCostSlider;
    // カウントダウン中に操作を受け付けないようにするためのパネル
    [SerializeField] GameObject panel;

    


    [Header("ゲーム設定")]
    // 電気代
    float totalElectricityBill = 100;
    // 収益
    float totalRevenue = 0;
    // 時間
    float timer = 0;
    // 一定時間ごとに増える収益（初期値は名古屋テレビ塔の収益）
    [SerializeField] float revenuePerSecond = 100;

    // 増設に必要なコストの一覧
    private float[] extensionCostList = {1000, 10000, 100000, 1000000, 10000000, 100000000};
    // 増設に必要な建物数の一覧
    private int[] extensionCountList = {10, 15, 21, 28, 36, 45};

    // 現在の増設に必要な建物数のインデックス
    int extensionCountIndex = 0;
    // 現在の増設にかかるお金のインデックス
    int extensionCostIndex = 0;
    // 建物の数
    int buildingCount = 1;

    // クリアに必要な電気代
    [SerializeField] float clearElectricityBill = 1000000;




    void Awake(){
        Instance = this;
    }


    void Start()
    {
        InitGame();
    }

    // 一番最初に呼ばれる関数
    void InitGame()
    {
        // 表示を更新
        UpdateValue();
        // ゲームスタートのコルーチンを呼び出す
        StartCoroutine(GameStart());
    }


    // ゲームスタートのカウントダウンとかのコルーチン
    public IEnumerator GameStart()
    {
        centerText.enabled = true;
        centerText.text = "3";
        yield return new WaitForSeconds(1);
        centerText.text = "2";
        yield return new WaitForSeconds(1);
        centerText.text = "1";
        yield return new WaitForSeconds(1);
        centerText.text = "GO!!";
        yield return new WaitForSeconds(1);
        centerText.text = "";
        centerText.enabled = false;
        panel.SetActive(false);
        // タイマー用のコルーチンを呼び出す
        StartCoroutine(UpdateTimerAndRevenue());
    }

    // タイマーと収益を更新するコルーチン
    public IEnumerator UpdateTimerAndRevenue()
    {
        // クリアするまでループ
        while(totalElectricityBill < clearElectricityBill)
        {
            yield return new WaitForSeconds(1);
            timer++;
            totalRevenue += revenuePerSecond;
            // 表示を更新
            UpdateValue();
        }
        StartCoroutine(GameClear());
    }

    // ゲームオーバーのコルーチン
    public IEnumerator GameOver()
    {
        centerText.enabled = true;
        centerText.text = "Game Over";
        yield return new WaitForSeconds(1);
        centerText.text = "";
        centerText.enabled = false;
    }

    public IEnumerator GameClear()
    {
        panel.SetActive(true);
        centerText.enabled = true;
        centerText.text = "Game Clear!!";
        yield return new WaitForSeconds(1);
        centerText.text = "";
        centerText.enabled = false;

        ///
        // リザルト表示
        ///
    }

    // 費用的に建設が可能かどうかの判定
    public bool CanBuild(float cost)
    {
        return totalRevenue >= cost;
    }

    // 建設
    public void Build(float cost, float revenue, float electricityBill)
    {
        // 収益と電気代を更新
        totalRevenue -= cost;
        revenuePerSecond += revenue;
        totalElectricityBill += electricityBill;
        buildingCount++;
        // 表示を更新
        UpdateValue();

    }

    // 増設できるかどうかの判定
    public bool CanExtension()
    {
        return buildingCount >= extensionCountList[extensionCountIndex] && totalRevenue >= extensionCostList[extensionCostIndex];
    }
    // 増設
    public void Extension()
    {
        // 収益を更新
        totalRevenue -= extensionCostList[extensionCostIndex];
        // 増設に必要な建物数とお金を更新
        extensionCountIndex++;
        extensionCostIndex++;

        // 表示を更新
        UpdateValue();
    }

    // 表示を更新する関数
    public void UpdateValue()
    {
        // 収益
        revenueText.text =  totalRevenue.ToString() + " doll";
        // 電気代
        electricityBillText.text = Mathf.Min(100, (Mathf.Floor(100*totalElectricityBill / clearElectricityBill))).ToString() + " %";
        electricityBillSlider.value = Mathf.Min(1.0f, totalElectricityBill / clearElectricityBill);
        // タイマー
        timerText.text =   timer.ToString() + " sec.";
        // 増設に必要な建物数
        ExtensionCountText.text = Mathf.Min(100, (Mathf.Floor(100*(float)buildingCount/ extensionCountList[extensionCountIndex]))).ToString() + " %";
        ExtensionCountSlider.value = Mathf.Min(1.0f, (float)buildingCount / extensionCountList[extensionCountIndex]);
        // 増設に必要なお金
        ExtensionCostText.text = Mathf.Min(100, (Mathf.Floor(100*totalRevenue / extensionCostList[extensionCostIndex]))).ToString() + " %";
        ExtensionCostSlider.value = Mathf.Min(1.0f, totalRevenue / extensionCostList[extensionCostIndex]);
    }
}