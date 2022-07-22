using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum LifeGameCellState
{
    Die = 0,
    Live = 1,
}

/// <summary>
/// LifeGameCellScriptクラス
/// </summary>
public class LifeGameCellScript : MonoBehaviour
{

    [Header("LifeGameCellCellState番号"), Tooltip("LifeGameCellCellState番号"), SerializeField] LifeGameCellState _lifeGameCellState = LifeGameCellState.Die;

    [Tooltip("LifeGameScript")] LifeGameScript _lifeGameScript;
    [Tooltip("Image")] Image _lifeGameCellImage;
    public LifeGameCellState CellState
    {
        get => _lifeGameCellState;
        set
        {
            _lifeGameCellState = value;
            StateChanged();
        }
    }

    void Start()
    {
        _lifeGameCellImage = GetComponent<Image>();
        _lifeGameScript = transform.parent.GetComponent<LifeGameScript>();
    }

    private void OnValidate()
    {
        StateChanged();
    }

    void Update()
    {
        StateChanged();    
    }

    void StateChanged()
    {
        if (_lifeGameCellImage == null) return;
        if (_lifeGameCellState == LifeGameCellState.Die)
        {
            _lifeGameCellImage.color = Color.black;
        }
        else if (_lifeGameCellState == LifeGameCellState.Live)
        {
            _lifeGameCellImage.color = Color.green;
        }
    }
}
