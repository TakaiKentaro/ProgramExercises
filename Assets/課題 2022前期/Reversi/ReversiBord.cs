using UnityEngine;
using UnityEngine.UI;

public enum ReversiBordState
{
    Empty = 0,
    Select = 1,
    Placed = 2,
}

/// <summary>
/// クラス説明
/// </summary>
public class ReversiBord : MonoBehaviour
{
    [Header("LifeGameCellCellState番号"), Tooltip("LifeGameCellCellState番号"), SerializeField] ReversiBordState _reversiBordState = ReversiBordState.Empty;

    [Tooltip("Cell")] public ReversiCellScript _reversiCell;

    [SerializeField, Tooltip("Color")] Image _image;

    [Tooltip("Reversi")] ReversiScript _reversi;
    [Tooltip("置けるか判定")]public bool _placedCheck = false;

    public ReversiBordState CellState
    {
        get => _reversiBordState;
        set
        {
            _reversiBordState = value;
            StateChanged();
        }
    }
    void Start()
    {
        _image = gameObject.GetComponent<Image>();

        _reversi = transform.parent.GetComponent<ReversiScript>();
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
        if (_reversiBordState == ReversiBordState.Empty)
        {
            _image.color = Color.green;
        }
        else if (_reversiBordState == ReversiBordState.Select)
        {
            _image.color = Color.red;
        }
        else if (_reversiBordState == ReversiBordState.Placed)
        {
            _image.color = Color.yellow;
        }
    }
}
