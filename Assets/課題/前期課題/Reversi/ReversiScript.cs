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
        //-----置ける判定をリセット-----
        foreach (var i in _bordCells)
        {
            i.CellState = ReversiBordState.Empty;
            i._placedCheck = false;
        }
        //------------------------------

        //-----置けるか判定-----
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
                        RightPlacedCheck(r, c);
                    }
                    if (c - 1 >= 0 && _bordCells[r, c - 1]._reversiCell.CellState == ReversiCellState.Empty) //左
                    {
                        LeftPlacedCheck(r, c);
                    }
                    if (r + 1 < _rows && _bordCells[r + 1, c]._reversiCell.CellState == ReversiCellState.Empty) //下 
                    {
                        UnderPlacedCheck(r, c);
                    }
                    if (r - 1 >= 0 && _bordCells[r - 1, c]._reversiCell.CellState == ReversiCellState.Empty) //上
                    {
                        UpperPlacedCheck(r, c);
                    }
                    if (c + 1 < _colums && r + 1 < _rows && _bordCells[r + 1, c + 1]._reversiCell.CellState == ReversiCellState.Empty) //右下
                    {
                        UnderRightPlacedOpen(r, c);
                    }
                    if (c - 1 >= 0 && r - 1 >= 0 && _bordCells[r - 1, c - 1]._reversiCell.CellState == ReversiCellState.Empty) //左上
                    {
                        UpperLeftPlacedOpen(r, c);
                    }
                    if (c + 1 < _colums && r - 1 >= 0 && _bordCells[r - 1, c + 1]._reversiCell.CellState == ReversiCellState.Empty) //左下
                    {
                        UnderLeftPlacedOpen(r, c);
                    }
                    if (c - 1 >= 0 && r + 1 < _rows && _bordCells[r + 1, c - 1]._reversiCell.CellState == ReversiCellState.Empty) //右上
                    {
                        UpperRightPlacedOpen(r, c);
                    }
                }
                else if (_turnCheck && bord._reversiCell.CellState == ReversiCellState.Black) //白の置ける判定
                {
                    if (c + 1 < _colums && _bordCells[r, c + 1]._reversiCell.CellState == ReversiCellState.Empty)//右
                    {
                        RightPlacedCheck(r, c);
                    }
                    if (c - 1 >= 0 && _bordCells[r, c - 1]._reversiCell.CellState == ReversiCellState.Empty) //左
                    {
                        LeftPlacedCheck(r, c);
                    }
                    if (r + 1 < _rows && _bordCells[r + 1, c]._reversiCell.CellState == ReversiCellState.Empty) //下 
                    {
                        UnderPlacedCheck(r, c);
                    }
                    if (r - 1 >= 0 && _bordCells[r - 1, c]._reversiCell.CellState == ReversiCellState.Empty) //上
                    {
                        UpperPlacedCheck(r, c);
                    }
                    if (c + 1 < _colums && r + 1 < _rows && _bordCells[r + 1, c + 1]._reversiCell.CellState == ReversiCellState.Empty) //右下
                    {
                        UnderRightPlacedOpen(r, c);
                    }
                    if (c - 1 >= 0 && r - 1 >= 0 && _bordCells[r - 1, c - 1]._reversiCell.CellState == ReversiCellState.Empty) //左上
                    {
                        UpperLeftPlacedOpen(r, c);
                    }
                    if (c + 1 < _colums && r - 1 >= 0 && _bordCells[r - 1, c + 1]._reversiCell.CellState == ReversiCellState.Empty) //左下
                    {
                        UnderLeftPlacedOpen(r, c);
                    }
                    if (c - 1 >= 0 && r + 1 < _rows && _bordCells[r + 1, c - 1]._reversiCell.CellState == ReversiCellState.Empty) //右上
                    {
                        UpperRightPlacedOpen(r, c);
                    }
                }
            }
        }
        //-----------------------


    }
    void RightPlacedCheck(int r, int c)
    {
        for (int x = c; x >= 0; x--)
        {
            if (_bordCells[r, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[r, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r, c + 1].CellState = ReversiBordState.Placed;
                _bordCells[r, c + 1]._placedCheck = true;
                break;
            }
            else if (!_turnCheck && _bordCells[r, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r, c + 1].CellState = ReversiBordState.Placed;
                _bordCells[r, c + 1]._placedCheck = true;
                break;
            }
        }
    }
    void LeftPlacedCheck(int r, int c)
    {
        for (int x = c; x < _colums; x++)
        {
            if (_bordCells[r, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[r, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r, c - 1].CellState = ReversiBordState.Placed;
                _bordCells[r, c - 1]._placedCheck = true;
                break;
            }
            else if (!_turnCheck && _bordCells[r, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r, c - 1].CellState = ReversiBordState.Placed;
                _bordCells[r, c - 1]._placedCheck = true;
                break;
            }
        }
    }
    void UnderPlacedCheck(int r, int c)
    {
        for (int y = r; y >= 0; y--)
        {
            if (_bordCells[y, c]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, c]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r + 1, c].CellState = ReversiBordState.Placed;
                _bordCells[r + 1, c]._placedCheck = true;
                break;
            }
            else if (!_turnCheck && _bordCells[y, c]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r + 1, c].CellState = ReversiBordState.Placed;
                _bordCells[r + 1, c]._placedCheck = true;
                break;
            }
        }
    }
    void UpperPlacedCheck(int r, int c)
    {
        for (int y = r; y < _rows; y++)
        {
            if (_bordCells[y, c]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, c]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r - 1, c].CellState = ReversiBordState.Placed;
                _bordCells[r - 1, c]._placedCheck = true;
                break;
            }
            else if (!_turnCheck && _bordCells[y, c]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r - 1, c].CellState = ReversiBordState.Placed;
                _bordCells[r - 1, c]._placedCheck = true;
                break;
            }
        }
    }
    void UnderRightPlacedOpen(int r, int c)
    {
        for (int y = r, x = c; y >= 0 || x >= 0; y--, x--)
        {
            if (_bordCells[y, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r + 1, c + 1].CellState = ReversiBordState.Placed;
                _bordCells[r, c + 1]._placedCheck = true;
                break;
            }
            else if (!_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r + 1, c + 1].CellState = ReversiBordState.Placed;
                _bordCells[r, c + 1]._placedCheck = true;
                break;
            }
        }
    }
    void UpperLeftPlacedOpen(int r, int c)
    {
        for (int y = r, x = c; y < _rows || x < _colums; y++, x++)
        {
            if (_bordCells[y, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r - 1, c - 1].CellState = ReversiBordState.Placed;
                _bordCells[r - 1, c - 1]._placedCheck = true;
                break;
            }
            else if (!_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r - 1, c - 1].CellState = ReversiBordState.Placed;
                _bordCells[r - 1, c - 1]._placedCheck = true;
                break;
            }
        }
    }
    void UnderLeftPlacedOpen(int r, int c)
    {
        for (int y = r, x = c; y < _rows || x >= 0; y++, x--)
        {
            if (_bordCells[y, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r - 1, c + 1].CellState = ReversiBordState.Placed;
                _bordCells[r - 1, c + 1]._placedCheck = true;
                break;
            }
            else if (!_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r - 1, c + 1].CellState = ReversiBordState.Placed;
                _bordCells[r - 1, c + 1]._placedCheck = true;
                break;
            }
        }
    }
    void UpperRightPlacedOpen(int r, int c)
    {
        for (int y = r, x = c; y >= 0 || x < _colums; y--, x++)
        {
            if (_bordCells[y, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[r + 1, c - 1].CellState = ReversiBordState.Placed;
                _bordCells[r + 1, c - 1]._placedCheck = true;
                break;
            }
            else if (!_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[r + 1, c - 1].CellState = ReversiBordState.Placed;
                _bordCells[r + 1, c - 1]._placedCheck = true;
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
                _XredPoint--;
                PlacedCheck();
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.D)) // 右キーを押した
        {
            if (_XredPoint < _rows - 1)
            {
                _XredPoint++;
                PlacedCheck();
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.W)) // 上キーを押した
        {
            if (_YredPoint > 0)
            {
                _YredPoint--;
                PlacedCheck();
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.S)) // 下キーを押した
        {
            if (_YredPoint < _colums - 1)
            {
                _YredPoint++;
                PlacedCheck();
                _bordCells[_YredPoint, _XredPoint].CellState = ReversiBordState.Select;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space)) // Cellを配置
        {
            if (_bordCells[_YredPoint, _XredPoint]._reversiCell.CellState == ReversiCellState.Empty && _bordCells[_YredPoint, _XredPoint]._placedCheck)
            {
                if (!_turnCheck) _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState = ReversiCellState.Black;
                else _bordCells[_YredPoint, _XredPoint]._reversiCell.CellState = ReversiCellState.White;
                BordCheck(_XredPoint, _YredPoint);
                PlacedCheck();
                TurnCheck();
            }
        }
    }

    void TurnCheck()
    {
        //-----次が白か黒か判断-----
        bool turn = false;
        foreach (var i in _bordCells)
        {
            if (i._placedCheck)
            {
                turn = true;
                break;
            }
        }

        if (turn)
        {
            if (_turnCheck) _turnCheck = false;
            else _turnCheck = true;
        }
        //---------------------------

        PlacedCheck();
    }

    /// <summary>
    /// 盤面を処理
    /// </summary>
    /// <returns></returns>
    void BordCheck(int x, int y)
    {
        RightChangeCheck(x, y);//右
        LeftChangeCheck(x, y);//左
        UnderChangeCheck(x, y); ;//下
        UpperChangeCheck(x, y);//上
        UnderRightChangeOpen(x, y);//右下
        UpperLeftChangeOpen(x, y);//左上
        UnderLeftChangeOpen(x, y);//左下
        UpperRightChangeOpen(x, y);//右上
    }
    void RightChangeCheck(int c, int r)
    {
        for (int x = c; x >= 0; x--)
        {
            Debug.Log(x);
            if (_bordCells[r, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[r, x]._reversiCell.CellState == ReversiCellState.White)
            {
                while (x < c)
                {
                    _bordCells[r, x]._reversiCell.CellState = ReversiCellState.White;
                    x++;
                }
                break;
            }
            else if (!_turnCheck && _bordCells[r, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                while (x < c)
                {
                    _bordCells[r, x]._reversiCell.CellState = ReversiCellState.Black;
                    x++;
                }
                break;
            }
        }
    }
    void LeftChangeCheck(int c, int r)
    {
        for (int x = c; x < _colums; x++)
        {
            if (_bordCells[r, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[r, x]._reversiCell.CellState == ReversiCellState.White)
            {
                while (x > c)
                {
                    _bordCells[r, x]._reversiCell.CellState = ReversiCellState.White;
                    x--;
                }
                break;
            }
            else if (!_turnCheck && _bordCells[r, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                while (x > c)
                {
                    _bordCells[r, x]._reversiCell.CellState = ReversiCellState.Black;
                    x--;
                }
                break;
            }
        }
    }
    void UnderChangeCheck(int c, int r)
    {
        for (int y = r; y >= 0; y--)
        {
            if (_bordCells[y, c]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, c]._reversiCell.CellState == ReversiCellState.White)
            {
                while (y < r)
                {
                    _bordCells[y, c]._reversiCell.CellState = ReversiCellState.White;
                    y++;
                }
                break;

            }
            else if (!_turnCheck && _bordCells[y, c]._reversiCell.CellState == ReversiCellState.Black)
            {
                while (y < r)
                {
                    _bordCells[y, c]._reversiCell.CellState = ReversiCellState.Black;
                    y++;
                }
                break;
            }
        }
    }
    void UpperChangeCheck(int c, int r)
    {
        for (int y = r; y < _rows; y++)
        {
            if (_bordCells[y, c]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, c]._reversiCell.CellState == ReversiCellState.White)
            {
                while (y > r)
                {
                    _bordCells[y, c]._reversiCell.CellState = ReversiCellState.White;
                    y--;
                }
                break;
            }
            else if (!_turnCheck && _bordCells[y, c]._reversiCell.CellState == ReversiCellState.Black)
            {
                while (y > r)
                {
                    _bordCells[y, c]._reversiCell.CellState = ReversiCellState.Black;
                    y--;
                }
                break;
            }
        }
    }
    void UnderRightChangeOpen(int c, int r)
    {
        for (int y = r, x = c; y >= 0 || x >= 0; y--, x--)
        {
            if (_bordCells[y, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[y, x]._reversiCell.CellState = ReversiCellState.White;
                y++; x++;
                break;
            }
            else if (!_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[y, x]._reversiCell.CellState = ReversiCellState.Black;
                y++; x++;
                break;
            }
        }
    }
    void UpperLeftChangeOpen(int c, int r)
    {
        for (int y = r, x = c; y < _rows || x < _colums; y++, x++)
        {
            if (_bordCells[y, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[y, x]._reversiCell.CellState = ReversiCellState.White;
                y--; x--;
                break;
            }
            else if (!_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[y, x]._reversiCell.CellState = ReversiCellState.Black;
                y--; x--;
                break;
            }
        }
    }
    void UnderLeftChangeOpen(int c, int r)
    {
        for (int y = r, x = c; y < _rows || x >= 0; y++, x--)
        {
            if (_bordCells[y, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[y, x]._reversiCell.CellState = ReversiCellState.White;
                y--; x++;
                break;
            }
            else if (!_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[y, x]._reversiCell.CellState = ReversiCellState.Black;
                y--; x++;
                break;
            }
        }
    }
    void UpperRightChangeOpen(int c, int r)
    {
        for (int y = r, x = c; y >= 0 || x < _colums; y--, x++)
        {
            if (_bordCells[y, x]._reversiCell.CellState == ReversiCellState.Empty) break;

            if (_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.White)
            {
                _bordCells[y, x]._reversiCell.CellState = ReversiCellState.White;
                y++; x--;
                break;
            }
            else if (!_turnCheck && _bordCells[y, x]._reversiCell.CellState == ReversiCellState.Black)
            {
                _bordCells[y, x]._reversiCell.CellState = ReversiCellState.Black;
                y++; x--;
                break;
            }
        }
    }
}
