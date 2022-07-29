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

    [SerializeField, Tooltip("スキップ回数")] int _skipCount;

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

    void OnValidate()
    {
        if(_rows <= 0) { _rows = 1; }
        if(_rows > 50) { _rows = 50; }
        if(_colums <= 0) { _colums = 1; }
        if(_colums > 100) { _colums = 100; }
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
                _lifeGameCells[r, c] = cell;

                if (cell.CellState == LifeGameCellState.Die)
                {
                    _boolCells[r, c] = false;
                }
                else if (cell.CellState == LifeGameCellState.Live)
                {
                    _boolCells[r, c] = true;
                }
            }
        }
    }

    public void OnClickGameStart()
    {
        if (GameState == LifeGameState.Stop)
        {
            Debug.Log($"再生");
            GameState = LifeGameState.Start;
            StartCoroutine("GameCycle");
        }
    }

    public void OnClickGameStop()
    {
        if (GameState == LifeGameState.Start)
        {
            Debug.Log($"停止");
            GameState = LifeGameState.Stop;
        }     
    }

    public void OnClickRandom()
    {
        if(GameState == LifeGameState.Stop)
        {
            int random;

            for (var r = 0; r < _rows; r++)
            {
                for (var c = 0; c < _colums; c++)
                {
                    random = Random.Range(0,2);
                    Debug.Log(random);
                    if (random == 0) { _lifeGameCells[r, c].CellState = LifeGameCellState.Die; }
                    else if(random == 1) { _lifeGameCells[r, c].CellState = LifeGameCellState.Live; }
                    
                }
            }
        }
    }

    public void OnClickSkip(int SkipNum)
    {
        if (GameState == LifeGameState.Stop)
        {
            for(int i = 0; i < SkipNum; i++)
            {
                CellMove();
            }  
        }
    }

    public void OnClickReset()
    {
        if (GameState == LifeGameState.Stop)
        {
            for (var r = 0; r < _rows; r++)
            {
                for (var c = 0; c < _colums; c++)
                {
                    _lifeGameCells[r, c].CellState = LifeGameCellState.Die;
                }
            }
        }
    }

    IEnumerator GameCycle()
    {
        while (GameState == LifeGameState.Start)
        {
            Debug.Log("GameCycle中");
            CellMove();
            yield return new WaitForSeconds(_time);
        }
    }

    void CellMove()
    {
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colums; c++)
            {
                CheckAround(r, c);
            }
        }

        ChangeAround();
    }

    /// <summary>
    /// Cellの周囲8近傍を確認
    /// </summary>
    void CheckAround(int x, int y)
    {
        int count = 0;

        if (_lifeGameCells[x, y].CellState == LifeGameCellState.Die)
        {
            _boolCells[x, y] = false;
        }
        else if (_lifeGameCells[x, y].CellState == LifeGameCellState.Live)
        {
            _boolCells[x, y] = true;
        }

        if (x + 1 < _rows && _lifeGameCells[x + 1, y].CellState == LifeGameCellState.Live) { count++; }//右
        if (x - 1 >= 0 && _lifeGameCells[x - 1, y].CellState == LifeGameCellState.Live) { count++; } //左
        if (y + 1 < _colums && _lifeGameCells[x, y + 1].CellState == LifeGameCellState.Live) { count++; } //下
        if (y - 1 >= 0 && _lifeGameCells[x, y - 1].CellState == LifeGameCellState.Live) { count++; } //上
        if (x + 1 < _rows && y + 1 < _colums && _lifeGameCells[x + 1, y + 1].CellState == LifeGameCellState.Live) { count++; } //右下
        if (x - 1 >= 0 && y - 1 >= 0 && _lifeGameCells[x - 1, y - 1].CellState == LifeGameCellState.Live) { count++; }//左上
        if (x + 1 < _rows && y - 1 >= 0 && _lifeGameCells[x + 1, y - 1].CellState == LifeGameCellState.Live) { count++; } //左下
        if (x - 1 >= 0 && y + 1 < _colums && _lifeGameCells[x - 1, y + 1].CellState == LifeGameCellState.Live) { count++; } //右上

        if (count == 3 && _lifeGameCells[x, y].CellState == LifeGameCellState.Die) //誕生
        {
            _boolCells[x, y] = true;
        }
        else if (count <= 1 && _lifeGameCells[x, y].CellState == LifeGameCellState.Live) //過疎
        {
            _boolCells[x, y] = false;
        }
        else if (count >= 4 && _lifeGameCells[x, y].CellState == LifeGameCellState.Live) //過密
        {
            _boolCells[x, y] = false;
        }
        //--ここまで来たら生存--//
    }

    /// <summary>
    /// 盤面を変更する
    /// </summary>
    void ChangeAround()
    {
        for (var r = 0; r < _rows; r++)
        {
            for (var c = 0; c < _colums; c++)
            {
                if (_boolCells[r, c] == true)
                {
                    _lifeGameCells[r, c].CellState = LifeGameCellState.Live;
                }
                else if (_boolCells[r, c] == false)
                {
                    _lifeGameCells[r, c].CellState = LifeGameCellState.Die;
                }
            }
        }
    }
}
