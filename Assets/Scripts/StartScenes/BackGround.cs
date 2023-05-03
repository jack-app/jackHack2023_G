using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;


public class BackGround : MonoBehaviour
{
    [SerializeField] private BackBuildings backBuildingPrefab = null!;
    private List<BackBuildings> backBuildings = new List<BackBuildings>();
    private float margin = 1000;
    // Start is called before the first frame update
    void Start()
    {
        while(margin > 0)
        {
            BackBuildings backBuilding = Instantiate(backBuildingPrefab);
            float newBuilding = backBuilding.Create();
            backBuilding.gameObject.transform.position = new Vector3(500f - margin, 0, 0);
            margin -= newBuilding;
            backBuildings.Add(backBuilding);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Update");
        List<int> remove = new List<int>();
        foreach (BackBuildings b in backBuildings)
        {
            bool lookable = b.Move();
            if (!lookable)
            {
                b.Remove();
                remove.Insert(0, backBuildings.IndexOf(b));
            }
        }
        foreach(int num in remove)
        {
            backBuildings.RemoveAt(num);
        }
    }
}
