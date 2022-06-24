using UnityEngine;
using UnityEngine.UI;

public enum CellState
{
    None = 0, //空のCell

    One = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,

    Mine = -1, //地雷
}

/// <summary>
/// Cellの挙動を制御するクラス
/// </summary>
public class CellScript : MonoBehaviour
{
    [Header("CellのText"), Tooltip("CellのText"), SerializeField] Text _text = null;

    [Header("CellState番号"), Tooltip("CellState番号"), SerializeField] CellState _cellState = CellState.None;

    public CellState CellState
    {
        get => _cellState;
        set
        {
            _cellState = value;
            OnCellStateChanged();
        }
    }

    void Awake()
    {
        if (!_text)
        {
            _text = GetComponentInChildren<Text>();
        }
    }

    void Start()
    {
        OnCellStateChanged();
    }

    /// <summary>
    /// Inspector上で変更が起きた時に呼び出される
    /// </summary>
    void OnValidate()
    {
        OnCellStateChanged();
    }

    /// <summary>
    /// CellStateが更新された時に書き換える
    /// </summary>
    void OnCellStateChanged()
    {
        if (_cellState == CellState.Mine)
        {
            _text.text = "☠";
            _text.color = Color.red;
        }
        else if (_cellState == CellState.None)
        {
            _text.text = "";
        }
        else
        {
            _text.text = ((int)_cellState).ToString();
            _text.color = Color.blue;
        }
    }
}
