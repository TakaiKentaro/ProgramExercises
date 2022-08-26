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
    [Tooltip("赤の場所Y")] int _YredPoint = 0;
    [Tooltip("赤の場所X")] int _XredPoint = 0;

    [SerializeField, Tooltip("Bord")] ReversiBord _bordCell = null;
    [Tooltip("BordCellの配列")] public ReversiBord[,] _bordCells = null;

    [Tooltip("GridLayoutGroup"), SerializeField] GridLayoutGroup _gridLayoutGroup = null;

    [Header("譜面入力"), SerializeField, Tooltip("譜面入力")] string _bordSheet;
    void Awake()
    {
        _bordCells = new ReversiBord[_rows, _colums];

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
                if (r == 0 && c == 0)
                {
                    bord.CellState = ReversiBordState.Select;
                }
                else if (r == 3 && c == 3 || r == 4 && c == 4)
                {
                    bord._reversiCell.CellState = ReversiCellState.White;
                }
                else if (r == 4 && c == 3 || r == 3 && c == 4)
                {
                    bord._reversiCell.CellState = ReversiCellState.Black;
                }
                else
                {
                    bord.CellState = ReversiBordState.Empty;
                }

            }
        }
    }
    private void Update()
    {
        SelectBord();
    }

    void SelectBord()
    {
        if (Input.GetKeyDown(KeyCode.A)) // 左キーを押した
        {
            if (_XredPoint > 0)
            {
                _XredPoint--;
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) // 右キーを押した
        {
            if (_XredPoint < _rows - 1)
            {
                _XredPoint++;
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) // 上キーを押した
        {
            if (_YredPoint > 0)
            {
                _YredPoint--;
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) // 下キーを押した
        {
            if (_YredPoint < _colums - 1)
            {
                _YredPoint++;
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(_bordCells[_YredPoint, _XredPoint].CellState == ReversiBordState.Placed)
            {
                _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState = ReversiCellState.White;
            }
        }
    }
}
