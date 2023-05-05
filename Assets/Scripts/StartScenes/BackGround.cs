using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackGround : MonoBehaviour
{
    public GameObject parent = null!;
    public BackBuildings backBuildingPrefab = null!;
    public bool moveable = true;
    private List<BackBuildings> backBuildings = new List<BackBuildings>();
    private List<BackBuildings> frontBuildings = new List<BackBuildings>();
    private float frontMargin = 1400;
    private float backMargin = 1400;
    private System.Random r = new System.Random();

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        while(frontMargin > 0)
        {
            if (r.Next(1,100) > 3)
            {
                frontMargin -= 2;
            }
            else
            {
                BackBuildings backBuilding = Instantiate(backBuildingPrefab);
                float newBuilding = backBuilding.Create(true);
                backBuilding.gameObject.transform.SetParent(parent.transform);
                backBuilding.gameObject.transform.localPosition = new Vector3(700f - frontMargin, -210, 0);
                backBuilding.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                frontMargin -= newBuilding;
                backBuildings.Add(backBuilding);
            }
        }
        while (backMargin > 0)
        {
            if (r.Next(1,100) > 3)
            {
                backMargin -= 5;
            }
            else
            {
                BackBuildings backBuilding = Instantiate(backBuildingPrefab);
                float newBuilding = backBuilding.Create(false);
                backBuilding.gameObject.transform.SetParent(parent.transform);
                backBuilding.gameObject.transform.localPosition = new Vector3(700f - backMargin, -210, 0);
                backBuilding.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                backMargin -= newBuilding;
                frontBuildings.Add(backBuilding);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveable) return;
        float mostFrontRight = -700;
        float mostBackRight = -700;
        List<int> remove = new List<int>();
        foreach (BackBuildings b in backBuildings)
        {
            float rightEnd = b.Move();
            if (rightEnd < -700)
            {
                b.Remove();
                remove.Insert(0, backBuildings.IndexOf(b));
            }
            else if(mostFrontRight < rightEnd) mostFrontRight = rightEnd;
        }
        foreach(int num in remove)
        {
            backBuildings.RemoveAt(num);
        }

        remove.Clear();
        foreach (BackBuildings b in frontBuildings)
        {
            float rightEnd = b.Move();
            if (rightEnd < -700)
            {
                b.Remove();
                remove.Insert(0, frontBuildings.IndexOf(b));
            }
            else if (mostBackRight < rightEnd) mostBackRight = rightEnd;
        }
        foreach(int num in remove)
        {
            frontBuildings.RemoveAt(num);
        }

        //create buildings
        if(mostFrontRight < 700 && r.Next(1,100) < 1.55)
        {
            BackBuildings backBuilding = Instantiate(backBuildingPrefab);
            float newBuilding = backBuilding.Create(true);
            backBuilding.gameObject.transform.SetParent(parent.transform);
            backBuilding.gameObject.transform.localPosition = new Vector3(700f, -210, 0);
            backBuilding.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            frontMargin = frontMargin - newBuilding + 10;
            frontBuildings.Add(backBuilding);
        }
        if(mostBackRight < 700 && r.Next(1,100) < 1.03)
        {
            BackBuildings backBuilding = Instantiate(backBuildingPrefab);
            float newBuilding = backBuilding.Create(false);
            backBuilding.gameObject.transform.SetParent(parent.transform);
            backBuilding.gameObject.transform.localPosition = new Vector3(700f, -210, 0);
            backBuilding.gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
            backMargin -= newBuilding;
            backBuildings.Add(backBuilding);
        }
    }
}
