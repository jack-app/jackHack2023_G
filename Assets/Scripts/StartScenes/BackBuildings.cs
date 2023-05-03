using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class BackBuildings : MonoBehaviour
{
    public MeshFilter meshFilter = null!;
    private System.Random r = new System.Random();
    private bool front; //Žè‘O‚È‚çtrue,‰œ‚È‚çfalse
    private int position;
    private float height;
    private float width;
    private Vector3[] meshArr = new Vector3[4];
    private int[] triangle = new int[6] { 0, 1, 2, 2, 1, 3 };

    public float Create()
    {
        height = r.Next(100, 400);
        width = r.Next(50, 300);
        meshArr[0] = new Vector3(0, 0, 0);
        meshArr[1] = new Vector3(0, height, 0);
        meshArr[2] = new Vector3(width, 0, 0);
        meshArr[3] = new Vector3(width, height, 0);
        Debug.Log(meshArr[0] + " , " + meshArr[1] + " , " + meshArr[2] + " , " + meshArr[3] + " ,   " + triangle[0] + " , " + triangle[1] + " , " + triangle[2] + " , " + triangle[3] + " , " + triangle[4] + " , " + triangle[5]);
        Mesh mesh = new Mesh();
        mesh.vertices = meshArr;
        mesh.triangles = triangle;

        return width;
    }

    public bool Move()
    {
        float x = transform.position.x;
        if (front) x = x - 2;
        else x = x - 1;
        transform.position = new Vector3(x, -100f, 0f);
        if (x + width < -500) return false;
        else return true;
    }

    public void Remove()
    {
        Destroy(this.gameObject.transform.GetChild(0).gameObject);
        Destroy(this.gameObject);
    }
}