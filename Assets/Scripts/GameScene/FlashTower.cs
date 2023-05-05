using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FlashTower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {    
        GetComponent<MeshRenderer>().materials[1].EnableKeyword("_EMISSION");
        GetComponent<MeshRenderer>().materials[1].SetColor("_EmissionColor", Color.yellow);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
