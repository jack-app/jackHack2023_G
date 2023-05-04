using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PanelChangeScript : MonoBehaviour, IPointerClickHandler
{
    // パネル一覧
    public GameObject[] panels;
    // 表示しているパネルの番号
    private int panelNum;

    void Start()
    {
        // パネルの番号を初期化
        panelNum = 0;
    }

    // クリックされたときの処理
    public void OnPointerClick(PointerEventData eventData)
    {
        // パネルの番号を変更
        panelNum = (panelNum + 1) % panels.Length;
        // パネルを変更
        panels[panelNum].transform.SetAsLastSibling();
    }
}
