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

    [Tooltip("ゲーム時間")] float _time;

    void Start()
    {
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _colums;

        _cells = new CellScript[_rows, _colums];

        _clearCount = (_rows * _colums) - _mineCount;

        CreateGrid();

        CreateMine();
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
            }
        }
    }

    /// <summary>
    /// ゲームをクリアしたとき
    /// </summary>
    public void GameClear()
    {
        _nowCount++;

        if(_nowCount >= _clearCount)
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
    void CreateMine()
    {
        int count = 0;
        while (count < _mineCount)
        {
            var r = Random.Range(0, _rows);
            var c = Random.Range(0, _colums);
            if (_cells[r, c].CellState == CellState.Mine)
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
}
