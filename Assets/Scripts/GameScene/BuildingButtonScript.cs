using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class BuildingButtonScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{   
    // BuildingプレハブをGameObject型で取得
    [SerializeField] GameObject buildingPrefab;

    private PostProcessVolume postProcessVolume;
    // ドラッグ中のオブジェクトを格納する変数
    private GameObject building;
    // パネルのGameObjectを格納する変数
    private GameObject panelObject;

    private PostProcessVolume _postProcessVolume;

    private Bloom _bloom;

    private PostProcessEffectSettings bloomProfile;

    private MeshRenderer[] components;

    // ボタンを無効化するためのパネルを格納する変数
    [SerializeField] GameObject overlapPanel;
    // 対応する建物のコスト
    float buildingCost;

    int n;
    // コストを表示するテキスト
    [SerializeField] TextMeshProUGUI costText;

    // Awakeで初期化しないとダメだった
    void Awake()
    {
        // 対応する建物のコストを取得
        buildingCost = buildingPrefab.GetComponent<BuildingScript>().cost;
        // コストを表示
        costText.text = "$"+buildingCost.ToString();
    }

    // ドラッグを開始したときに呼び出されるメソッド
    public void OnBeginDrag(PointerEventData eventData)
    {
        // BuildingプレハブをGameObject型で取得
        building = Instantiate (buildingPrefab, Vector3.zero, Quaternion.identity) as GameObject;

        components = building.GetComponents<MeshRenderer>();
        for (n=0; n<components.Length; n++) {
            components[n].material.EnableKeyword("_EMISSION");
            components[n].material.SetColor("_EmissionColor", Color.yellow);
        }
        // Bloom効果のインスタンスの作成
        Bloom bloom = ScriptableObject.CreateInstance<Bloom>();
        bloom.enabled.Override(true);
        bloom.intensity.Override(8f);
        //　ポストプロセスボリュームに反映
        postProcessVolume = PostProcessManager.instance.QuickVolume(gameObject.layer, 0f, bloom);

        // パネル要素を取得
        panelObject = GameObject.FindWithTag("BuildingPanel");
        // パネルをスライドアウト
        panelObject.GetComponent<PanelSlider>().SlideOut();
    }

    // ドラッグ中に呼び出されるメソッド
    public void OnDrag(PointerEventData eventData)
    {
        building.GetComponent<BuildingScript>().SetPosition();
    }

    // ドラッグを終了したときに呼び出されるメソッド
    public void OnEndDrag(PointerEventData eventData)
    {
        // パネルをスライドイン
        panelObject.GetComponent<PanelSlider>().SlideIn();
        // 設置できるかを判定
        building.GetComponent<BuildingScript>().ConstructBuilding();
    }

    // オブジェクトのy座標を考慮した位置を取得
    private Vector3 calcBuildingPosition(Vector3 position) {
        Vector3 newPosition = position;
        newPosition.y =  building.transform.localScale.y/2;
        return newPosition;
    }

    // 収益に応じてボタンを無効化する関数
    public void UpdateBuildingButton(float revenue)
    {
        // 収益が0の場合
        if(revenue < buildingCost)
        {
            // ボタンを無効化
            overlapPanel.SetActive(true);
        }
        else
        {
            // ボタンを有効化
            overlapPanel.SetActive(false);
        }
    }
}
