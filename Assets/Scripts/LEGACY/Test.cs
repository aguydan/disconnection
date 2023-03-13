using System.Collections;
using System.Collections.Generic;
using Daniel.Utils;
using UnityEngine;

public class Test : MonoBehaviour
{
    Grid grid;
    
    void Start()
    {
        grid = new Grid(4, 2, 10f, new Vector3(20, 0));
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            grid.SetValue(Utils.GetMouseWorldPosition(), 56);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetValue(Utils.GetMouseWorldPosition()));
        }
    }
}