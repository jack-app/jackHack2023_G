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
    private float frontMargin = 1100;
    private float backMargin = 1100;
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
                backBuilding.gameObject.transform.position = new Vector3(550f - frontMargin, -210, 0);
                backBuilding.gameObject.transform.SetParent(parent.transform);
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
                backBuilding.gameObject.transform.position = new Vector3(550f - backMargin, -210, 0);
                backBuilding.gameObject.transform.SetParent(parent.transform);
                backMargin -= newBuilding;
                frontBuildings.Add(backBuilding);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!moveable) return;
        float mostFrontRight = -550;
        float mostBackRight = -550;
        List<int> remove = new List<int>();
        foreach (BackBuildings b in backBuildings)
        {
            float rightEnd = b.Move();
            if (rightEnd < -550)
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
            if (rightEnd < -550)
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
        if(mostFrontRight < 550 && r.Next(1,100) < 1.55)
        {
            BackBuildings backBuilding = Instantiate(backBuildingPrefab);
            float newBuilding = backBuilding.Create(true);
            backBuilding.gameObject.transform.position = new Vector3(550f, -210, 0);
            backBuilding.gameObject.transform.SetParent(parent.transform);
            frontMargin = frontMargin - newBuilding + 10;
            frontBuildings.Add(backBuilding);
        }
        if(mostBackRight < 550 && r.Next(1,100) < 1.03)
        {
            BackBuildings backBuilding = Instantiate(backBuildingPrefab);
            float newBuilding = backBuilding.Create(false);
            backBuilding.gameObject.transform.position = new Vector3(550f, -210, 0);
            backBuilding.gameObject.transform.SetParent(parent.transform);
            backMargin -= newBuilding;
            backBuildings.Add(backBuilding);
        }
    }
}
