using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelChangeScript : MonoBehaviour
{
    // パネル一覧
    public GameObject[] panels;

    // パネルを変更
    public void ChangePanel(int num)
    {
        // パネルを変更
        panels[num].transform.SetAsLastSibling();
    }
}
