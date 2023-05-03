using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartDrag : MonoBehaviour, IPointerDownHandler
{   
    // 画像ごとに建物の番号を割り振る
    [SerializeField] int buildingNumber;

    // クリックされたときに呼び出されるメソッド
    public void OnPointerDown(PointerEventData eventData)
    {
        // BuildingプレハブをGameObject型で取得
        GameObject building = (GameObject)Resources.Load ("Building");
        // Buildingプレハブを元に、インスタンスを生成
        Instantiate (building, Vector3.zero, Quaternion.identity);
    }
}
