using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{
    // フィールドのサイズ
    private float fieldSize;
    // フィールドの辺の長さ
    private float fieldLength;
    // フィールドの最大の大きさ
    private float fieldMaxSize;

    // フィールドのゲームオブジェクト
    public GameObject fieldTile;
    // フィールドの外側のタイル
    public GameObject outFieldTile1, outFieldTile2, outFieldTile3, outFieldTile4;

    void Start() 
    {
        // フィールドのサイズを設定
        fieldSize = 1;
        // フィールドの辺の長さを設定
        fieldLength = fieldSize * 10;
        // フィールドの最大の大きさを設定
        fieldMaxSize = 10;
        // フィールドを取得
        fieldTile = GameObject.FindWithTag("FieldTile");
    }

    public void ExtensionFieldSize()
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
    }
}
