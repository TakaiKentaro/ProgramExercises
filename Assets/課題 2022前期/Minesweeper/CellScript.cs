using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum OpenState
{
    Open = 0,
    Close = 1,
    Flag = 2,
}

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
public class CellScript : MonoBehaviour, IPointerClickHandler
{
    [Header("CellのText"), Tooltip("CellのText"), SerializeField] Text _text = null;
    [Header("OpenState番号"), Tooltip("OpenStateの番号"), SerializeField] OpenState _openState = OpenState.Close;
    [Header("CellState番号"), Tooltip("CellState番号"), SerializeField] CellState _cellState = CellState.None;
    [Tooltip("Cell")] Image _cellImage;
    [Tooltip("Cellの名前")] string _name;

    [Tooltip("MineSweeperScript")] public MineSweeperScript _mineSweeperScript;

    public OpenState OpenState
    {
        get => _openState;
        set
        {
            _openState = value;
            OnOpenStateChanged();
        }
    }

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
        _name = gameObject.name;
        _cellImage = GetComponent<Image>();
        _mineSweeperScript = transform.parent.GetComponent<MineSweeperScript>();
        OnOpenStateChanged();
    }

    /// <summary>
    /// 押した判定
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.pointerId)
        {
            case -1:
                Debug.Log("Left Click");
                if (_openState == OpenState.Close)
                {
                    _openState = OpenState.Open;
                    OnOpenStateChanged();
                    _mineSweeperScript.CreateMine(_name);

                    if(CellState == CellState.None)
                    {
                        int x = int.Parse(_name[1].ToString());
                        int y = int.Parse(_name[3].ToString());
                        _mineSweeperScript.Open(x, y);
                    }
                }
                else if (_openState == OpenState.Flag)
                {
                    _openState = OpenState.Open;
                    OnOpenStateChanged();
                }
                break;
            case -2:
                Debug.Log("Right Click");
                if (_openState == OpenState.Close)
                {
                    _openState = OpenState.Flag;
                    OnOpenStateChanged();
                }
                break;
        }
    }

    /// <summary>
    /// OpenStateが更新された時に書き換える
    /// </summary>
    void OnOpenStateChanged()
    {
        if (_openState == OpenState.Open)
        {
            _cellImage.color = Color.white;
            //_mineSweeperScript.Open(_name);
            OnCellStateChanged();
        }
        else if (_openState == OpenState.Flag)
        {
            _text.text = "F";
            _text.color = Color.yellow;
        }
    }

    /// <summary>
    /// CellStateが更新された時に書き換える
    /// </summary>
    void OnCellStateChanged()
    {
        if (_openState != OpenState.Open) return;

        if (_cellState == CellState.Mine)
        {
            _text.text = "☠";
            _text.color = Color.red;
            _mineSweeperScript.GameOver();
        }
        else if (_cellState == CellState.None)
        {
            _text.text = "";
            _mineSweeperScript.GameClear();
        }
        else
        {
            _text.text = ((int)_cellState).ToString();
            _text.color = Color.blue;
            _mineSweeperScript.GameClear();
        }
    }
}
