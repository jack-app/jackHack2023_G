using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityFx.Outline;

public class MoveBuilding : MonoBehaviour
{   
    private GameObject transparentPlane;
    private CreatePlane createPlane;
    // Planeオブジェクトを格納する変数
    Plane plane;   
    // オブジェクトを掴んでいるかどうかを判定する変数
    bool isGrabbing = true;
    // オブジェクトが重なっているかどうか
    bool isOverlap = false;
    // フィールドの上に完全に乗ったかどうか
    bool isOnField = false;
    bool isOutField = true;

 
    void Start()
    {
        transparentPlane = GameObject.Find("TransparentPlane");
        // Planeオブジェクトを作成する
        plane = transparentPlane.GetComponent<CreatePlane>().plane;

        // マウスの位置からRayを飛ばす
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float rayDistance;
        plane.Raycast(ray, out rayDistance);

        // マウスの位置にオブジェクトを移動させる、ただしy座標は固定
        this.transform.position = calcBuildingPosition(ray.GetPoint(rayDistance));
        StartCoroutine("MoveBuildingOnDrag");
        this.transform.SetParent(GameObject.Find("Field").transform);
    }

    IEnumerator MoveBuildingOnDrag()
    {
        print(isGrabbing);
        while(isGrabbing){
            // マウスの位置からRayを飛ばす
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float rayDistance;
            plane.Raycast(ray, out rayDistance);


            // マウスの位置にオブジェクトを移動させる、ただしy座標は固定
             this.transform.position = calcBuildingPosition(ray.GetPoint(rayDistance));

            // アウトラインの色を
            if(isOverlap || !isOnField || isOutField)
            {
                // 赤色にする
                GetComponent<OutlineBehaviour>().OutlineColor = Color.red;
            }
            else
            {
                GetComponent<OutlineBehaviour>().OutlineColor = Color.green;
            }

            // マウスの左ボタンを離したら
            if (!Input.GetMouseButton(0))
            {   
                // オブジェクトを離す
                isGrabbing = false;
                // アウトラインを消す
                GetComponent<OutlineBehaviour>().enabled = false;
                //　オブジェクトが重なっているかと、フィールド上に存在するかどうか判定
                if(isOverlap || !isOnField || isOutField){
                    Destroy(this.gameObject);
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
  
    }

    // オブジェクトが重なったとき
    void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Building"))
        {
            print("重なった");
            isOverlap = true;
        }
        else if(other.CompareTag("FieldTile"))
        {
            print("フィールドに乗った");
            isOnField = true;
        }
        else if(other.CompareTag("OutFieldTile"))
        {
            print("フィールドから外れた");
            isOutField = true;
        }
    }

    // 重なっているオブジェクトが移動したとき
    void OnTriggerStay(Collider other) {
        if(other.CompareTag("Building"))
        {
            print("重なっている");
            isOverlap = true;
        }
        else if(other.CompareTag("FieldTile"))
        {
            print("フィールドに乗っている");
            isOnField = true;
        }
        else if(other.CompareTag("OutFieldTile"))
        {
            print("フィールドから外れている");
            isOutField = true;
        }
    }

    // オブジェクトが離れた時
    void OnTriggerExit(Collider other) {
        if(other.CompareTag("Building")){
            print("離れた");
            isOverlap = false;
        }
        else if(other.CompareTag("FieldTile"))
        {
            print("フィールドから離れた");
            isOnField = false;
        }
        else if(other.CompareTag("OutFieldTile"))
        {
            print("外に行ったか、フィールドに戻ったか");
            isOutField = false;
        }
    }

    // // オブジェクトが重なっている間
    // void OnTriggerStay(Collider other) {
        
    // }

    /// <summary>
    /// オブジェクトのy座標を考慮した位置を取得
    /// </summary>
    /// <returns></returns> 
    private Vector3 calcBuildingPosition(Vector3 position) {
        Vector3 newPosition = position;
        newPosition.y =  this.transform.transform.localScale.y/2;
        return newPosition;
    }
}
