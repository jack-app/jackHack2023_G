using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFx.Outline;

public class BuildingScript : MonoBehaviour
{   
    // 透明なPlaneオブジェクト
    private GameObject transparentPlane;
    // 透明なPlaneオブジェクトを作成するスクリプト
    private CreatePlane createPlane;

    // 建物の建設費用
    public float cost;
    // 建物の収益
    public float revenue;
    // 建物の電気代
    public float electricityBill;

    // Planeオブジェクトを格納する変数
    Plane plane;
    // オブジェクトを掴んでいるかどうかを判定する変数
    bool isGrabbing = true;
    // オブジェクトが重なっているかどうか
    int overlapCount = 0;
    // オブジェクトがフィールドの上に完全に乗ったかどうか
    int onOutField = 0;
    // フィールドの上に完全に乗ったかどうか
    bool isOnField = false;

    void Start()
    {
        transparentPlane = GameObject.Find("TransparentPlane");
        // Planeオブジェクトを作成する
        plane = transparentPlane.GetComponent<CreatePlane>().plane;
        transform.SetParent(GameObject.FindWithTag("Field").transform);
    }

    // ドラッグアンドドロップ終了時に呼び出される関数
    public void ConstructBuilding()
    {
        // オブジェクトを離す
        isGrabbing = false;
        // アウトラインを消す
        GetComponent<OutlineBehaviour>().enabled = false;
        //　オブジェクトが重なっているかとフィールド上に存在するかを判定
        if(overlapCount > 0 || onOutField > 0 || !isOnField || !GameManager.Instance.CanBuild(cost)){
            // 建設できないので削除
            gameObject.SetActive(false);
        }
        else
        {
            // 建設できるので建設費を支払う
            GameManager.Instance.Build(cost, revenue, electricityBill);
        }
    }
    
    // マウスの位置にオブジェクトを移動させる関数
    public void SetPosition()
    {
        // マウスの位置からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;
        plane.Raycast(ray, out rayDistance);
        transform.position = calcBuildingPosition(ray.GetPoint(rayDistance));
    }

    // オブジェクトが重なったとき
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Building"))
        {
            overlapCount++;
        }
        else if(other.CompareTag("FieldTile"))
        {
            isOnField = true;
        }
        else if(            
            other.CompareTag("OutFieldTile1") || 
            other.CompareTag("OutFieldTile2") || 
            other.CompareTag("OutFieldTile3") || 
            other.CompareTag("OutFieldTile4")
        )
        {
            onOutField++;
        }
        // アウトラインの色を変える
        changeOutlineColor();
    }

    // オブジェクトが離れた時
    void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Building")){
            overlapCount--;
            // print(overlapCount);
        }
        else if(other.CompareTag("FieldTile"))
        {
            isOnField = false;
        }
        else if(
            other.CompareTag("OutFieldTile1") || 
            other.CompareTag("OutFieldTile2") || 
            other.CompareTag("OutFieldTile3") || 
            other.CompareTag("OutFieldTile4")
        )
        {
            onOutField--;
        }
        // アウトラインの色を変える
        changeOutlineColor();
    }

    // オブジェクトのy座標を考慮した位置を取得
    private Vector3 calcBuildingPosition(Vector3 position) {
        // print(transform.localScale);
        Vector3 newPosition = position;
        newPosition.y = -0.1f;
        return newPosition;
    }

    // アウトラインの色を変える関数
    private void changeOutlineColor()
    {
        // アウトラインの色を
        if(overlapCount > 0 || onOutField > 0 || !isOnField)
        {
            // 赤色にする
            GetComponent<OutlineBehaviour>().OutlineColor = Color.red;
        }
        else
        {
            // 緑色にする
            GetComponent<OutlineBehaviour>().OutlineColor = Color.green;
        }
    }
}
