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
    [Tooltip("赤の場所Y")] int r = 0;
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
        PlacedCheck();
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

    /// <summary>
    /// 置ける場所を判定する
    /// </summary>
    void PlacedCheck()
    {
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colums; c++)
            {
                var bord = _bordCells[r, c];

                if (bord._reversiCell.CellState == ReversiCellState.Empty) continue;

                if (!_turnCheck && bord._reversiCell.CellState == ReversiCellState.White) //黒の置ける判定
                {
                    if (c + 1 < _colums && _bordCells[r, c + 1]._reversiCell.CellState == ReversiCellState.Empty)//右
                    {
                        RightCheck(r, c);
                    }
                    if (c - 1 >= 0 && _bordCells[r, c - 1]._reversiCell.CellState == ReversiCellState.Empty) //左
                    {
                        LeftCheck(r, c);
                    }
                    if (r + 1 < _rows && _bordCells[r + 1, c]._reversiCell.CellState == ReversiCellState.Empty) //下 
                    {
                        UnderCheck(r, c);
                    }
                    if (r - 1 >= 0 && _bordCells[r - 1, c]._reversiCell.CellState == ReversiCellState.Empty) //上
                    {
                        UpperCheck(r, c);
                    }
                    if (c + 1 < _colums && r + 1 < _rows && _bordCells[r + 1, c + 1]._reversiCell.CellState == ReversiCellState.Empty) //右下
                    {
                        UnderRightOpen(r, c);
                    }
                    if (c - 1 >= 0 && r - 1 >= 0 && _bordCells[r - 1, c - 1]._reversiCell.CellState == ReversiCellState.Empty) //左上
                    {
                        UpperLeftOpen(r, c);
                    }
                    if (c + 1 < _colums && r - 1 >= 0 && _bordCells[r - 1, c + 1]._reversiCell.CellState == ReversiCellState.Empty) //左下
                    {
                        UnderLeftOpen(r, c);
                    }
                    if (c - 1 >= 0 && r + 1 < _rows && _bordCells[r + 1, c - 1]._reversiCell.CellState == ReversiCellState.Empty) //右上
                    {
                        UpperRightOpen(r, c);
                    }
                }
                else if (_turnCheck && bord._reversiCell.CellState == ReversiCellState.Black) //白の置ける判定
                {
                    if (c + 1 < _colums && _bordCells[r, c + 1]._reversiCell.CellState == ReversiCellState.Empty)//右
                    {
                        RightCheck(r, c);
                    }
                    if (c - 1 >= 0 && _bordCells[r, c - 1]._reversiCell.CellState == ReversiCellState.Empty) //左
                    {
                        LeftCheck(r, c);
                    }
                    if (r + 1 < _rows && _bordCells[r + 1, c]._reversiCell.CellState == ReversiCellState.Empty) //下 
                    {
                        UnderCheck(r, c);
                    }
                    if (r - 1 >= 0 && _bordCells[r - 1, c]._reversiCell.CellState == ReversiCellState.Empty) //上
                    {
                        UpperCheck(r, c);
                    }
                    if (c + 1 < _colums && r + 1 < _rows && _bordCells[r + 1, c + 1]._reversiCell.CellState == ReversiCellState.Empty) //右下
                    {
                        UnderRightOpen(r, c);
                    }
                    if (c - 1 >= 0 && r - 1 >= 0 && _bordCells[r - 1, c - 1]._reversiCell.CellState == ReversiCellState.Empty) //左上
                    {
                        UpperLeftOpen(r, c);
                    }
                    if (c + 1 < _colums && r - 1 >= 0 && _bordCells[r - 1, c + 1]._reversiCell.CellState == ReversiCellState.Empty) //左下
                    {
                        UnderLeftOpen(r, c);
                    }
                    if (c - 1 >= 0 && r + 1 < _rows && _bordCells[r + 1, c - 1]._reversiCell.CellState == ReversiCellState.Empty) //右上
                    {
                        UpperRightOpen(r, c);
                    }
                }
            }
        }
    }

    void RightCheck(int r, int c)
    {
        for (int x = c; x >= 0; x--)
        {
            if (_bordCells[r, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[r, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r, c + 1].CellState = ReversiBordState.Placed;
                break;
            }
            else if (!_turnCheck && _bordCells[r, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r, c + 1].CellState = ReversiBordState.Placed;
                break;
            }
        }
    }
    void LeftCheck(int r, int c)
    {
        for (int x = c; x < _colums; x++)
        {
            if (_bordCells[r, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[r, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r, c - 1].CellState = ReversiBordState.Placed;
                break;
            }
            else if (!_turnCheck && _bordCells[r, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r, c - 1].CellState = ReversiBordState.Placed;
                break;
            }
        }
    }
    void UnderCheck(int r, int c)
    {
        for (int y = r; y >= 0; y--)
        {
            if (_bordCells[y, c]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, c]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r + 1, c].CellState = ReversiBordState.Placed;
                break;
            }
            else if (!_turnCheck && _bordCells[y, c]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r + 1, c].CellState = ReversiBordState.Placed;
                break;
            }
        }
    }
    void UpperCheck(int r, int c)
    {
        for (int y = r; y < _rows; y++)
        {
            if (_bordCells[y, c]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, c]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r - 1, c].CellState = ReversiBordState.Placed;
                break;
            }
            else if (!_turnCheck && _bordCells[y, c]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r - 1, c].CellState = ReversiBordState.Placed;
                break;
            }
        }
    }
    void UnderRightOpen(int r, int c)
    {
        for (int y = r, x = c; y >= 0 || x >= 0; y--, x--)
        {
            if (_bordCells[y, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r + 1, c + 1].CellState = ReversiBordState.Placed;
                break;
            }
            else if (!_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r + 1, c + 1].CellState = ReversiBordState.Placed;
                break;
            }
        }
    }
    void UpperLeftOpen(int r, int c)
    {
        for (int y = r, x = c; y < _rows || x < _colums; y++, x++)
        {
            if (_bordCells[y, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r - 1, c - 1].CellState = ReversiBordState.Placed;
                break;
            }
            else if (!_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r - 1, c - 1].CellState = ReversiBordState.Placed;
                break;
            }
        }
    }
    void UnderLeftOpen(int r, int c)
    {
        for (int y = r, x = c; y < _rows || x >= 0; y++, x--)
        {
            if (_bordCells[y, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r - 1, c + 1].CellState = ReversiBordState.Placed;
                break;
            }
            else if (!_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r - 1, c + 1].CellState = ReversiBordState.Placed;
                break;
            }
        }
    }
    void UpperRightOpen(int r, int c)
    {
        for (int y = r, x = c; y >= 0 || x < _colums; y--, x++)
        {
            if (_bordCells[y, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r + 1, c - 1].CellState = ReversiBordState.Placed;
                break;
            }
            else if (!_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r + 1, c - 1].CellState = ReversiBordState.Placed;
                break;
            }

        }
    }

    void SelectBord()
    {
        if (Input.GetKeyDown(KeyCode.A)) // 左キーを押した
        {
            if (_XredPoint > 0)
            {
                _bordCells[r, _XredPoint].CellState = ReversiBordState.Empty;
                _XredPoint--;
                PlacedCheck();
                _bordCells[r, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) // 右キーを押した
        {
            if (_XredPoint < _rows - 1)
            {
                _bordCells[r, _XredPoint].CellState = ReversiBordState.Empty;
                _XredPoint++;
                PlacedCheck();
                _bordCells[r, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) // 上キーを押した
        {
            if (r > 0)
            {
                _bordCells[r, _XredPoint].CellState = ReversiBordState.Empty;
                r--;
                PlacedCheck();
                _bordCells[r, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) // 下キーを押した
        {
            if (r < _colums - 1)
            {
                _bordCells[r, _XredPoint].CellState = ReversiBordState.Empty;
                r++;
                PlacedCheck();
                _bordCells[r, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) // Cellを配置
        {
            if (_bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.Empty)
            {
                if (!_turnCheck) _bordCells[r, _XredPoint]._reversiCell.CellState = ReversiCellState.Black;
                else _bordCells[r, _XredPoint]._reversiCell.CellState = ReversiCellState.White;
                _turnCheck = true;
                BordCheck(_XredPoint, r);
                PlacedCheck();
            }
        }
    }

    /// <summary>
    /// 盤面を処理
    /// </summary>
    /// <returns></returns>
    void BordCheck(int x, int y)
    {
        if (!_turnCheck) // 黒の盤面生成
        {
            if (x + 1 < _rows && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.White) { } //右
            if (x - 1 >= 0 && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.White) { } //左
            if (y + 1 < _colums && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.White) { } //下
            if (y - 1 >= 0 && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.White) { } //上
            if (x + 1 < _rows && y + 1 < _colums && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.White) { } //右下
            if (x - 1 >= 0 && y - 1 >= 0 && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.White) { }//左上
            if (x + 1 < _rows && y - 1 >= 0 && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.White) { } //左下
            if (x - 1 >= 0 && y + 1 < _colums && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.White) { } //右上
        }
        else // 白の盤面生成
        {
            if (x + 1 < _rows && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //右
            if (x - 1 >= 0 && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //左
            if (y + 1 < _colums && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //下
            if (y - 1 >= 0 && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //上
            if (x + 1 < _rows && y + 1 < _colums && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //右下
            if (x - 1 >= 0 && y - 1 >= 0 && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { }//左上
            if (x + 1 < _rows && y - 1 >= 0 && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //左下
            if (x - 1 >= 0 && y + 1 < _colums && _bordCells[r, _XredPoint]._reversiCell.CellState == ReversiCellState.Black) { } //右上
        }
    }
}
