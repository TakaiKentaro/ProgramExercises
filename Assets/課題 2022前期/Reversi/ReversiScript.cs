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

    [Tooltip("falseが黒・trueが白を置ける")] public bool _turnCheck = false;
    void Awake()
    {
        _bordCells = new ReversiBord[_rows, _colums];

        gameObject.GetComponent<GridLayoutGroup>().constraintCount = _colums;

        CreatGrid();
    }
    private void Start()
    {
        _turnCheck = false;
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
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Empty;
                _XredPoint--;
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) // 右キーを押した
        {
            if (_XredPoint < _rows - 1)
            {
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Empty;
                _XredPoint++;
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) // 上キーを押した
        {
            if (_YredPoint > 0)
            {
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Empty;
                _YredPoint--;
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) // 下キーを押した
        {
            if (_YredPoint < _colums - 1)
            {
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Empty;
                _YredPoint++;
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) // Cellを配置
        {
            if (_bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.Empty)
            {
                if (!_turnCheck) _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState = ReversiCellState.Black;
                else _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState = ReversiCellState.White;
                BordCheck(_XredPoint, _YredPoint);
            }
        }
    }

    /// <summary>
    /// 盤面を処理
    /// </summary>
    /// <returns></returns>
    void BordCheck(int x, int y)
    {
        if(!_turnCheck) // 黒の盤面生成
        {
            if (x + 1 < _rows && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.White) {  } //右
            if (x - 1 >= 0 && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.White) {  } //左
            if (y + 1 < _colums && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.White) {  } //下
            if (y - 1 >= 0 && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.White) {  } //上
            if (x + 1 < _rows && y + 1 < _colums && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.White) {  } //右下
            if (x - 1 >= 0 && y - 1 >= 0 && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.White) {  }//左上
            if (x + 1 < _rows && y - 1 >= 0 && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.White) {  } //左下
            if (x - 1 >= 0 && y + 1 < _colums && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.White) {  } //右上
        }
        else // 白の盤面生成
        {
            if (x + 1 < _rows && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //右
            if (x - 1 >= 0 && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //左
            if (y + 1 < _colums && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //下
            if (y - 1 >= 0 && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //上
            if (x + 1 < _rows && y + 1 < _colums && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //右下
            if (x - 1 >= 0 && y - 1 >= 0 && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { }//左上
            if (x + 1 < _rows && y - 1 >= 0 && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //左下
            if (x - 1 >= 0 && y + 1 < _colums && _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //右上
        }
    }
}
