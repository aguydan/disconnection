using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookItem : MonoBehaviour
{
    [SerializeField] GridGenerator _gridGenerator;

    private void Start()
    {
       _gridGenerator.CreateLeftGrid();
       _gridGenerator.CreateRightGrid();
    }
}
