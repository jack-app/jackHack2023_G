using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExtensionField : MonoBehaviour, IPointerClickHandler
{
    // フィールドのサイズ
    public float fieldSize;
    // フィールドの辺の長さ
    public float fieldLength;
    // フィールドの最大の大きさ
    public float fieldMaxSize;

    // フィールドのゲームオブジェクト
    public GameObject fieldTile;
    // フィールドの外側のタイル
    public GameObject outFieldTile1, outFieldTile2, outFieldTile3, outFieldTile4;

    // クリックされたときの処理
    public void OnPointerClick(PointerEventData eventData)
    {
        // フィールドが最大の大きさだったらリターン
        if (fieldSize == fieldMaxSize)
        {
            return;
        }
        // フィールドのサイズを拡張
        fieldSize++;
        // フィールドの辺の長さを拡張
        fieldLength = fieldSize * 10;
        // フィールドの大きさを変更
        fieldTile.transform.localScale = new Vector3(fieldSize, 1, fieldSize);

        // 外側のタイルを拡張
        outFieldTile1.transform.localScale = new Vector3(fieldSize+1, 1, 1);
        outFieldTile1.transform.Translate (0.0f, 0.0f, -5.0f);

        outFieldTile2.transform.localScale = new Vector3(fieldSize+1, 1, 1);
        outFieldTile2.transform.Translate (0.0f, 0.0f, 5.0f);

        outFieldTile3.transform.localScale = new Vector3(1, 1, fieldSize+1);
        outFieldTile3.transform.Translate (5.0f, 0.0f, 0.0f);

        outFieldTile4.transform.localScale = new Vector3(1, 1, fieldSize+1);
        outFieldTile4.transform.Translate (-5.0f, 0.0f, 0.0f);

        
        // カメラの位置を変更
        Camera.main.gameObject.transform.position += new Vector3(5.0f,5.0f,5.0f);
    }
}
