using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ReversiCellState
{
    Empty = 0,
    Black = 1,
    White = 2,
}

/// <summary>
/// ReversiCell用クラス
/// </summary>
public class ReversiCellScript : MonoBehaviour
{
    [Header("LifeGameCellCellState番号"), Tooltip("LifeGameCellCellState番号"), SerializeField] ReversiCellState _reverciCellState = ReversiCellState.Empty;

    [SerializeField,Tooltip("Animator")] Animator _anim = null;

    public ReversiCellState CellState
    {
        get => _reverciCellState;
        set
        {
            _reverciCellState = value;
            StateChanged();
        }
    }
    void Start()
    {

    }

    private void OnValidate()
    {
        StateChanged();
    }

    void Update()
    {
        
    }

    void StateChanged()
    {
        Debug.Log("StateAnim");
        if(_reverciCellState == ReversiCellState.White)
        {
            _anim.Play("Black->White");
        }
        else if(_reverciCellState == ReversiCellState.Black)
        {
            _anim.Play("White->Black");
        }
    }
}
