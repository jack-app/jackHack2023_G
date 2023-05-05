using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayBackBuildings;

public class BackBuildingManager : MonoBehaviour
{
    [SerializeField] GameObject parent = null!;
    [SerializeField] BackBuildings backBuildingPrefab = null!;
    private List<BackBuildings> backBuildings = new List<BackBuildings>();
    private List<BackBuildings> frontBuildings = new List<BackBuildings>();
    private float frontMargin = -360f;
    private float backMargin = -360f;
    private System.Random r = new System.Random();

    void Awake()
    {
        Application.targetFrameRate = 60;
    }

    void Start()
    {
        while (frontMargin < 0)
        {
            if (r.Next(1, 100) > 10)
            {
                frontMargin += 1f;
            }
            else
            {
                BackBuildings backBuilding = Instantiate(backBuildingPrefab);
                float newBuilding = backBuilding.Create(true, frontMargin);
                backBuilding.gameObject.transform.position = new Vector3(0, 0, 0);
                backBuilding.gameObject.transform.eulerAngles = new Vector3(0, frontMargin, 0);
                backBuilding.gameObject.transform.SetParent(parent.transform);
                frontMargin += newBuilding;
                backBuildings.Add(backBuilding);
                Debug.Log(newBuilding + " , " + frontMargin);
            }
        }
        while (backMargin < 0)
        {
            if (r.Next(1, 100) > 10)
            {
                backMargin += 1f;
            }
            else
            {
                BackBuildings backBuilding = Instantiate(backBuildingPrefab);
                float newBuilding = backBuilding.Create(false, backMargin);
                backBuilding.gameObject.transform.position = new Vector3(0, 0, 0);
                backBuilding.gameObject.transform.eulerAngles = new Vector3(0, backMargin, 0);
                backBuilding.gameObject.transform.SetParent(parent.transform);
                backMargin += newBuilding;
                frontBuildings.Add(backBuilding);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float mostFrontRight = -360;
        float mostBackRight = -360;
        List<int> remove = new List<int>();
        foreach (BackBuildings b in backBuildings)
        {
            float rightEnd = b.Move();
            if (rightEnd < -270)
            {
                b.Remove();
                remove.Insert(0, backBuildings.IndexOf(b));
            }
            else if (mostFrontRight < rightEnd) mostFrontRight = rightEnd;
        }
        foreach (int num in remove)
        {
            backBuildings.RemoveAt(num);
        }

        remove.Clear();
        foreach (BackBuildings b in frontBuildings)
        {
            float rightEnd = b.Move();
            if (rightEnd < -270)
            {
                b.Remove();
                remove.Insert(0, frontBuildings.IndexOf(b));
            }
            else if (mostBackRight < rightEnd) mostBackRight = rightEnd;
        }
        foreach (int num in remove)
        {
            frontBuildings.RemoveAt(num);
        }

        //create buildings
        if (mostFrontRight < 0 && r.Next(1, 100) < 1.55)
        {
            BackBuildings backBuilding = Instantiate(backBuildingPrefab);
            float newBuilding = backBuilding.Create(true, 0);
            backBuilding.gameObject.transform.position = new Vector3(0, 0, 0);
            backBuilding.gameObject.transform.SetParent(parent.transform);
            frontMargin = frontMargin - newBuilding + 10;
            frontBuildings.Add(backBuilding);
        }
        if (mostBackRight < 0 && r.Next(1, 200) < 1.03)
        {
            BackBuildings backBuilding = Instantiate(backBuildingPrefab);
            float newBuilding = backBuilding.Create(false, 0);
            backBuilding.gameObject.transform.position = new Vector3(0, 0, 0);
            backBuilding.gameObject.transform.SetParent(parent.transform);
            backMargin -= newBuilding;
            backBuildings.Add(backBuilding);
        }
    }
}
