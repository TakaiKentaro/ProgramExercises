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
/// CellScriptクラス
/// </summary>
public class CellScript : MonoBehaviour
{
    [Header("CellのText"), Tooltip("CellのText"), SerializeField] Text _text = null;

    void Start()
    {
        if(!_text)
        {
            _text = GetComponentInChildren<Text>();
        }
    }
}
