using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingDragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{   
    // 画像ごとに建物の番号を割り振る
    [SerializeField] string buildingPrefabName;
    // ドラッグ中のオブジェクトを格納する変数
    private GameObject building;
    // 親要素のGameObjectを格納する変数
    private GameObject parentObject;

    // ドラッグを開始したときに呼び出されるメソッド
    public void OnBeginDrag(PointerEventData eventData)
    {
        // BuildingプレハブをGameObject型で取得
        building = Instantiate ((GameObject)Resources.Load(buildingPrefabName), Vector3.zero, Quaternion.identity) as GameObject;
        // 親要素のGameObjectを取得
        parentObject = transform.parent.gameObject;
        // パネルをスライドアウト
        parentObject.GetComponent<PanelSlider>().SlideOut();
    }

    // ドラッグ中に呼び出されるメソッド
    public void OnDrag(PointerEventData eventData)
    {
        building.GetComponent<LocateBuilding>().SetPosition();
    }

    // ドラッグを終了したときに呼び出されるメソッド
    public void OnEndDrag(PointerEventData eventData)
    {
    // パネルをスライドイン
        parentObject.GetComponent<PanelSlider>().SlideIn();
        building.GetComponent<LocateBuilding>().EndDragAndDrop();
    }

    // オブジェクトのy座標を考慮した位置を取得
    private Vector3 calcBuildingPosition(Vector3 position) {
        Vector3 newPosition = position;
        newPosition.y =  building.transform.localScale.y/2;
        return newPosition;
    }
}