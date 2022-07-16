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
    /// 自動展開用関数
    /// </summary>
    /// <param name="name"></param>
    public void AutoOpen(string name)
    {
        int x = int.Parse(name[1].ToString());
        int y = int.Parse(name[3].ToString());

        if (_cells[x, y].CellState != CellState.Mine && _cells[x, y].CellState != CellState.None) return;

        int count1 = 1, count2 = 1, count3 = 1, count4 = 1, count5 = 1, count6 = 1, count7 = 1, count8 = 1;

        while (true)//右
        {
            if (x + count1 < _rows && _cells[x + count1, y].CellState != CellState.Mine)
            {
                if (_cells[x + count1, y].CellState == CellState.None)
                {
                    _cells[x + count1, y].OpenState = OpenState.Open;
                    count1++;
                    continue;
                }
                _cells[x + count1, y].OpenState = OpenState.Open;
            }
            break;
        }
        while (true)//左
        {
            if (x - count2 >= 0 && _cells[x - count2, y].CellState != CellState.Mine)
            {
                if (_cells[x - count2, y].CellState == CellState.None)
                {
                    _cells[x - count2, y].OpenState = OpenState.Open;
                    count2++;
                    continue;
                }
                _cells[x - count2, y].OpenState = OpenState.Open;
            }
            break;
        }
        while (true)//下
        {
            if (y + count3 < _colums && _cells[x, y + count3].CellState != CellState.Mine)
            {
                if (_cells[x, y + count3].CellState == CellState.None)
                {
                    _cells[x, y + count3].OpenState = OpenState.Open;
                    count3++;
                    continue;
                }
                _cells[x, y + count3].OpenState = OpenState.Open;
            }
            break;
        }
        while (true)//上
        {
            if (y - count4 >= 0 && _cells[x, y - count4].CellState != CellState.Mine)
            {
                if (_cells[x, y - count4].CellState == CellState.None)
                {
                    _cells[x, y - count4].OpenState = OpenState.Open;
                    count4++;
                    continue;
                }
                _cells[x, y - count4].OpenState = OpenState.Open;
            }
            break;
        }
        while (true)//右下
        {
            if (x + count5 < _rows && y + count5 < _colums && _cells[x + count5, y + count5].CellState != CellState.Mine)
            {
                if (_cells[x + count5, y + count5].CellState == CellState.None)
                {
                    _cells[x + count5, y + count5].OpenState = OpenState.Open;
                    count5++;
                    continue;
                }
                _cells[x + count5, y + count5].OpenState = OpenState.Open;
            }
            break;
        }
        while (true)//左上
        {
            if (x - count6 >= 0 && y - count6 >= 0 && _cells[x - count6, y - count6].CellState != CellState.Mine)
            {
                if (_cells[x - count6, y - count6].CellState == CellState.None)
                {
                    _cells[x - count6, y - count6].OpenState = OpenState.Open;
                    count6++;
                    continue;
                }
                _cells[x - count6, y - count6].OpenState = OpenState.Open;
            }
            break;
        }
        while (true)//左下
        {
            if (x + count7 < _rows && y - count7 >= 0 && _cells[x + count7, y - count7].CellState != CellState.Mine)
            {
                if (_cells[x + count7, y - count7].CellState == CellState.None)
                {
                    _cells[x + count7, y - count7].OpenState = OpenState.Open;
                    count7++;
                    continue;
                }
                _cells[x + count7, y - count7].OpenState = OpenState.Open;
            }
            break;
        }
        while (true)//右上
        {
            if (x - count8 >= 0 && y + count8 < _colums && _cells[x - count8, y + count8].CellState != CellState.Mine)
            {
                if (_cells[x - count8, y + count8].CellState == CellState.None)
                {
                    _cells[x - count8, y + count8].OpenState = OpenState.Open;
                    count1++;
                    continue;
                }
                _cells[x - count8, y + count8].OpenState = OpenState.Open;
            }
            break;
        }
    }
}
