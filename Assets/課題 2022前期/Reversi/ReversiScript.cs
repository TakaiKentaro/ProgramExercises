using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Reversi用クラス
/// </summary>
public class ReversiScript : MonoBehaviour
{
    [Tooltip("行数")] int _rows = 8;
    [Tooltip("列数")] int _colums = 8;

    [SerializeField,Tooltip("Bord")] GameObject _bordCell = null;
    [Tooltip("BordCellの配列")] GameObject[,] _bordCells = null;

    [Tooltip("GridLayoutGroup"), SerializeField] GridLayoutGroup _gridLayoutGroup = null;
    void Start()
    {
        _bordCells = new GameObject[_rows, _colums];

        gameObject.GetComponent<GridLayoutGroup>().constraintCount = _colums;

        CreatGrid();
    }
    void CreatGrid()
    {
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colums; c++)
            {
                var bord = Instantiate(_bordCell, _gridLayoutGroup.transform);
                _bordCells[r, c] = bord;    
            }
        }
    }
}
