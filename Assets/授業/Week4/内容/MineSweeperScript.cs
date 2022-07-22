using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MineSweeper全てを制御するクラス
/// </summary>
[RequireComponent(typeof(GridLayoutGroup))]
public class MineSweeperScript : MonoBehaviour
{
    [Tooltip("行数"), SerializeField] int _rows = 0;
    [Tooltip("列数"), SerializeField] int _colums = 0;
    [Tooltip("爆弾の数"), SerializeField] int _mineCount = 0;

    [Tooltip("GridLayoutGroup"), SerializeField] GridLayoutGroup _gridLayoutGroup = null;

    [Tooltip("CellPrefab"), SerializeField] CellScript _cellPrefab = null;
    [Tooltip("CellPrefabsの配列")] CellScript[,] _cells = null;

    [Tooltip("現在の数")] int _nowCount;
    [Tooltip("ゲームクリアに必要な数")] int _clearCount;

    [Tooltip("判定を取得用")] bool _check;

    [Tooltip("ゲーム時間")] float _time;

    void Start()
    {
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _colums;

        _cells = new CellScript[_rows, _colums];

        _clearCount = (_rows * _colums) - _mineCount;

        CreateGrid();
    }

    private void Update()
    {
        _time += Time.deltaTime;
    }

    private void CreateGrid()
    {
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colums; c++)
            {
                var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform);
                cell.CellState = CellState.None;
                _cells[r, c] = cell;
                cell.name = $"[{r},{c}]";
            }
        }
    }

    /// <summary>
    /// ゲームをクリアしたとき
    /// </summary>
    public void GameClear()
    {
        _nowCount++;

        if (_nowCount > _clearCount)
        {
            Debug.Log($"ゲームクリア");
            Debug.Log($"クリアタイム　{(int)_time}秒");
        }
    }

    /// <summary>
    /// 爆弾が出たとき
    /// </summary>
    public void GameOver()
    {
        Debug.Log($"ゲームオーバー");
    }

    /// <summary>
    /// 指定した個数爆弾を生成する
    /// </summary>
    public void CreateMine(string name)
    {
        if (!_check)
        {
            int count = 0;
            int x = int.Parse(name[1].ToString());
            int y = int.Parse(name[3].ToString());
            Debug.Log($"{x},{y}");
            while (count < _mineCount)
            {
                var r = UnityEngine.Random.Range(0, _rows);
                var c = UnityEngine.Random.Range(0, _colums);
                if (_cells[r, c].CellState == CellState.Mine)
                {
                    continue;
                }
                else if (r == x && c == y)
                {
                    continue;
                }
                else
                {
                    _cells[r, c].CellState = CellState.Mine;
                    CountUp(r, c);
                    count++;
                }
            }
        }
        _check = true;
    }

    /// <summary>
    /// 爆弾の周りに+1する
    /// </summary>
    /// <param name="r"></param>
    /// <param name="c"></param>
    void CountUp(int r, int c)
    {
        if (r + 1 < _rows && _cells[r + 1, c].CellState != CellState.Mine) { _cells[r + 1, c].CellState += 1; } //右
        if (r - 1 >= 0 && _cells[r - 1, c].CellState != CellState.Mine) { _cells[r - 1, c].CellState += 1; } //左
        if (c + 1 < _colums && _cells[r, c + 1].CellState != CellState.Mine) { _cells[r, c + 1].CellState += 1; } //下
        if (c - 1 >= 0 && _cells[r, c - 1].CellState != CellState.Mine) { _cells[r, c - 1].CellState += 1; } //上

        if (r + 1 < _rows && c + 1 < _colums && _cells[r + 1, c + 1].CellState != CellState.Mine) { _cells[r + 1, c + 1].CellState += 1; } //右下
        if (r - 1 >= 0 && c - 1 >= 0 && _cells[r - 1, c - 1].CellState != CellState.Mine) { _cells[r - 1, c - 1].CellState += 1; }//左上
        if (r + 1 < _rows && c - 1 >= 0 && _cells[r + 1, c - 1].CellState != CellState.Mine) { _cells[r + 1, c - 1].CellState += 1; } //左下
        if (r - 1 >= 0 && c + 1 < _colums && _cells[r - 1, c + 1].CellState != CellState.Mine) { _cells[r - 1, c + 1].CellState += 1; } //右上
    }

    /// <summary>
    /// Cellから周囲8方向を確認
    /// </summary>
    /// <param name="name"></param>
    public void Open(int x, int y)
    {
            RightOpen(x, y);
            LeftOpen(x, y);
            UnderOpen(x, y);
            UpperOpen(x, y);
            UnderRightOpen(x, y);
            UpperLeftOpen(x, y);
            UnderLeftOpen(x, y);
            UpperRightOpen(x, y);
    }

    /// <summary>
    /// 右展開
    /// </summary>
    /// <param name="name"></param>
    void RightOpen(int x, int y)
    {
        if (x + 1 < _rows && _cells[x + 1, y].CellState != CellState.Mine && _cells[x + 1, y].OpenState == OpenState.Close) //右
        {
            Debug.Log("右");
            if (_cells[x + 1, y].CellState == CellState.None)
            {
                _cells[x + 1, y].OpenState = OpenState.Open;

                Open(x + 1,y);
            }
            else
            {
                _cells[x + 1, y].OpenState = OpenState.Open;
            }
        }
    }

    /// <summary>
    /// 左展開
    /// </summary>
    /// <param name="name"></param>
    void LeftOpen(int x, int y)
    {
        if (x - 1 >= 0 && _cells[x - 1, y].CellState != CellState.Mine && _cells[x - 1, y].OpenState == OpenState.Close) //左
        {
            Debug.Log("左");
            if (_cells[x - 1, y].CellState == CellState.None)
            {
                _cells[x - 1, y].OpenState = OpenState.Open;

                Open(x - 1, y);
            }
            else
            {
                _cells[x - 1, y].OpenState = OpenState.Open;
            }
        }
    }

    /// <summary>
    /// 下展開
    /// </summary>
    /// <param name="name"></param>
    void UnderOpen(int x, int y)
    {
        if (y + 1 < _colums && _cells[x, y + 1].CellState != CellState.Mine && _cells[x, y + 1].OpenState == OpenState.Close) //下
        {
            Debug.Log("下");
            if (_cells[x, y + 1].CellState == CellState.None)
            {
                _cells[x, y + 1].OpenState = OpenState.Open;

                Open(x , y + 1);
            }
            else
            {
                _cells[x, y + 1].OpenState = OpenState.Open;
            }
        }
    }

    /// <summary>
    /// 上展開
    /// </summary>
    /// <param name="name"></param>
    void UpperOpen(int x, int y)
    {
        if (y - 1 >= 0 && _cells[x, y - 1].CellState != CellState.Mine && _cells[x, y - 1].OpenState == OpenState.Close) //上
        {
            Debug.Log("上");
            if (_cells[x, y - 1].CellState == CellState.None)
            {
                _cells[x, y - 1].OpenState = OpenState.Open;

                Open(x , y - 1);
            }
            else
            {
                _cells[x, y - 1].OpenState = OpenState.Open;
            }
        }
    }

    /// <summary>
    /// 右下展開
    /// </summary>
    /// <param name="name"></param>
    void UnderRightOpen(int x, int y)
    {
        if (x + 1 < _rows && y + 1 < _colums && _cells[x + 1, y + 1].CellState != CellState.Mine && _cells[x + 1, y + 1].OpenState == OpenState.Close)//右下
        {
            Debug.Log("右下");
            if (_cells[x + 1, y + 1].CellState == CellState.None)
            {
                _cells[x + 1, y + 1].OpenState = OpenState.Open;

                Open(x + 1, y + 1);
            }
            else
            {
                _cells[x + 1, y + 1].OpenState = OpenState.Open;
            }
        }
    }

    /// <summary>
    /// 左上
    /// </summary>
    /// <param name="name"></param>
    void UpperLeftOpen(int x, int y)
    {
        if (x - 1 >= 0 && y - 1 >= 0 && _cells[x - 1, y - 1].CellState != CellState.Mine && _cells[x - 1, y - 1].OpenState == OpenState.Close) //左上
        {
            Debug.Log("左上");
            if (_cells[x - 1, y - 1].CellState == CellState.None)
            {
                _cells[x - 1, y - 1].OpenState = OpenState.Open;

                Open(x - 1, y - 1);
            }
            else
            {
                _cells[x - 1, y - 1].OpenState = OpenState.Open;
            }
        }
    }

    /// <summary>
    /// 左下展開
    /// </summary>
    /// <param name="name"></param>
    void UnderLeftOpen(int x, int y)
    {
        if (x + 1 < _rows && y - 1 >= 0 && _cells[x + 1, y - 1].CellState != CellState.Mine && _cells[x + 1, y - 1].OpenState == OpenState.Close) //左下
        {
            Debug.Log("左下");
            if (_cells[x + 1, y - 1].CellState == CellState.None)
            {
                _cells[x + 1, y - 1].OpenState = OpenState.Open;

                Open(x + 1, y - 1);
            }
            else
            {
                _cells[x + 1, y - 1].OpenState = OpenState.Open;
            }
        }
    }

    /// <summary>
    /// 右上展開
    /// </summary>
    /// <param name="name"></param>
    void UpperRightOpen(int x, int y)
    {
        if (x - 1 >= 0 && y + 1 < _colums && _cells[x - 1, y + 1].CellState != CellState.Mine && _cells[x - 1, y + 1].OpenState == OpenState.Close) //右上
        {
            Debug.Log("右上");
            if (_cells[x - 1, y + 1].CellState == CellState.None)
            {
                _cells[x - 1, y + 1].OpenState = OpenState.Open;

                Open(x - 1, y + 1);
            }
            else
            {
                _cells[x - 1, y + 1].OpenState = OpenState.Open;
            }
        }
    }
}

