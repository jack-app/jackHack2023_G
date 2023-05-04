using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class PanelChangeButtonScript : MonoBehaviour, IPointerClickHandler
{
    // パネル名を表示するテキスト
    [SerializeField] TextMeshProUGUI panelNameText;
    // パネルをいくつ進めるか
    [SerializeField] int panelNum;

    // クリックされたときの処理
    public void OnPointerClick(PointerEventData eventData)
    {
        // パネルを変更
        transform.parent.GetComponent<PanelChangeScript>().ChangePanel(panelNum);
    }
}
