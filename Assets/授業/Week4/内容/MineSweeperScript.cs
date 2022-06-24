using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// MineSweeper全てを制御するクラス
/// </summary>
[RequireComponent(typeof(GridLayoutGroup))]
public class MineSweeperScript : MonoBehaviour
{
    [Tooltip("GridLayoutGroup"), SerializeField] GridLayoutGroup _gridLayoutGroup = null;

    [Tooltip("CellPrefab"), SerializeField] CellScript _cellPrefab = null;

    void Start()
    {
        _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        _gridLayoutGroup.constraintCount = 10;

        for (var r = 0; r < 10; r++)
        {
            for (var c = 0; c < 10; c++)
            {
                var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform);
                cell.CellState = CellState.Mine;
            }
        }
    }
}
