using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class BuildingButtonScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{   
    // BuildingプレハブをGameObject型で取得
    [SerializeField] GameObject buildingPrefab;
    // ドラッグ中のオブジェクトを格納する変数
    private GameObject building;
    // パネルのGameObjectを格納する変数
    private GameObject panelObject;
    // ボタンを無効化するためのパネルを格納する変数
    [SerializeField] GameObject overlapPanel;
    // 対応する建物のコスト
    float buildingCost;
    // コストを表示するテキスト
    [SerializeField] TextMeshProUGUI costText;

    void Start()
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
