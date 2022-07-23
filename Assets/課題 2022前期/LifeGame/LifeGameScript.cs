using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LifeGameState
{
    Stop = 0,
    Start = 1,
}

/// <summary>
/// LifeGameScript全てを制御するクラス
/// </summary>
public class LifeGameScript : MonoBehaviour
{
    [SerializeField, Tooltip("ゲームスピード")] float _time;

    [Tooltip("行数"), SerializeField] int _rows = 0;
    [Tooltip("列数"), SerializeField] int _colums = 0;

    [SerializeField, Tooltip("Cell")] LifeGameCellScript _cellPrefab = null;
    [Tooltip("_cellPrefabの配列")] LifeGameCellScript[,] _lifeGameCells = null;
    [Tooltip("Boolの配列")] bool[,] _boolCells;

    [Tooltip("GridLayoutGroup"), SerializeField] GridLayoutGroup _gridLayoutGroup = null;

    [Header("LifeGameState番号"), Tooltip("LifeGameState番号"), SerializeField] LifeGameState _lifeGameState = LifeGameState.Stop;

    public LifeGameState GameState
    {
        get => _lifeGameState;
        set
        {
            _lifeGameState = value;
        }
    }
    void Start()
    {
        _lifeGameCells = new LifeGameCellScript[_rows, _colums];
        _boolCells = new bool[_rows, _colums];

        gameObject.GetComponent<GridLayoutGroup>().constraintCount = _colums;

        CreatGrid();
    }

    public void OnClickGameStart()
    {
        Debug.Log($"再生");
        GameState = LifeGameState.Start;
        StartCoroutine("GameCycle");
    }

    public void OnClickGameStop()
    {
        Debug.Log($"停止");
        GameState = LifeGameState.Stop;
    }

    /// <summary>
    /// 盤面を作る
    /// </summary>
    void CreatGrid()
    {
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colums; c++)
            {
                var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform);
                cell.CellState = LifeGameCellState.Live;
                _lifeGameCells[r, c] = cell;
                
                if(cell.CellState == LifeGameCellState.Die)
                {
                    _boolCells[r, c] = false;
                }
                else if(cell.CellState == LifeGameCellState.Live)
                {
                    _boolCells[r, c] = true;
                }
            }
        }
    }

    IEnumerator GameCycle()
    {
        while(GameState == LifeGameState.Start)
        {
            Debug.Log("GameCycle中");
            yield return new WaitForSeconds(_time);
        }
    }
}
