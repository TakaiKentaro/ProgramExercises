using UnityEngine;
using UnityEngine.UI;

public class ColorTransitioner : MonoBehaviour
{
    [SerializeField]
    private Image _image = default; // 背景画像

    [SerializeField]
    private Color _to = default; // この色に遷移する

    [SerializeField]
    private float _duration = 1; // 遷移時間（秒）

    private Color _from;
    private float _elapsed = 0;

    /// <summary>
    /// 背景色の遷移が完了しているかどうか
    /// </summary>
    public bool IsCompleated
        => _image == null ? false : _image.color == _to;

    void Start()
    {
        if (_image == null) { return; }
        _from = _image.color;
    }

    void Update()
    {
        if (_image == null) { return; }
        _elapsed += Time.deltaTime;
        if (_elapsed < _duration)
        {
            _image.color = Color.Lerp(_from, _to, _elapsed / _duration);
        }
        else
        {
            _image.color = _to;
        }
    }

    /// <summary>
    /// 背景色の遷移処理を開始する
    /// </summary>
    /// <param name="color"></param>
    public void Play(Color color)
    {
        if (_image == null) { return; }

        _from = _image.color;
        _to = color;
        _elapsed = 0;
    }

    /// <summary>
    /// 現在の背景色遷移処理をスキップする
    /// </summary>
    public void Skip()
    {
        _elapsed = _duration;
    }
}