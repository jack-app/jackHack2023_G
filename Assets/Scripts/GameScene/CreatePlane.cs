using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePlane : MonoBehaviour
{
    // Planeオブジェクトを格納する変数
    public Plane plane;   
    void Start()
    {
        // Planeオブジェクトを作成する
        plane = new Plane(Vector3.up, Vector3.up);
    }
}
