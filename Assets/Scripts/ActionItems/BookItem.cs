using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookItem : MonoBehaviour
{
    [SerializeField] GridGenerator _gridGenerator;
    [SerializeField] WordsObject _wordsObject;

    private void Start()
    {
       _gridGenerator.CreateRightGrid();
       _gridGenerator.CreateLeftSpawnPoints();
       _gridGenerator.CreateLeftGrid();
    }
}
