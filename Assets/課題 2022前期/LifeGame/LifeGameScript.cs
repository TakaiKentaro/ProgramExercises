using UnityEngine;
using UnityEngine.UI;

public enum LineGameState
{
    Start = 1,
    Update = 0,
    Stop = -1,
}

/// <summary>
/// LifeGameScript全てを制御するクラス
/// </summary>
public class LifeGameScript : MonoBehaviour
{
    [Tooltip("行数"), SerializeField] int _rows = 0;
    [Tooltip("列数"), SerializeField] int _colums = 0;

    [SerializeField, Tooltip("Cell")] LifeGameCellScript _cellPrefab = null;
    [Tooltip("_cellPrefabの配列")] LifeGameCellScript[,] _lifeGameCells = null;

    [Tooltip("GridLayoutGroup"), SerializeField] GridLayoutGroup _gridLayoutGroup = null;

    [Header("LifeGameState番号"), Tooltip("LifeGameState番号"), SerializeField] LineGameState _lifeGameState = LineGameState.Stop;
    void Start()
    {
        _gridLayoutGroup = gameObject.GetComponent<GridLayoutGroup>();
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = _colums;

        _lifeGameCells = new LifeGameCellScript[_rows, _colums];

        CreatGrid();
    }

    void CreatGrid()
    {
        for(var r = 0; r < _rows; r++)
        {
            for(var c = 0; c < _colums; c++)
            {
                var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform);
            }
        }
    }
}
