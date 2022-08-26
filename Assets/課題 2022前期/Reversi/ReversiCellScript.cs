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

    [SerializeField, Tooltip("Animator")] Animator _anim = null;

    [SerializeField,Tooltip("Color")] Image _image = null;
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
        _image = gameObject.GetComponent<Image>();
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
        if (_reverciCellState == ReversiCellState.White)
        {
            _image.color = Color.white;
        }
        else if (_reverciCellState == ReversiCellState.Black)
        {
            _image.color = Color.black;
        }
        else if (_reverciCellState == ReversiCellState.Empty)
        {
            _image.color = Color.clear;
        }
    }
}
