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
    void Start()
    {
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _colums;

        _cells = new CellScript[_rows, _colums];

        CreateGrid();

        CreateMine();
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
        _cells[r + 1, c].CellState += 1;
        _cells[r - 1, c].CellState += 1;

        _cells[r, c + 1].CellState += 1;
        _cells[r, c - 1].CellState += 1;

        _cells[r + 1, c + 1].CellState += 1;
        _cells[r - 1, c - 1].CellState += 1;

        _cells[r + 1, c - 1].CellState += 1;
        _cells[r - 1, c - 1].CellState += 1;
    }
}
