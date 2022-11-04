using UnityEngine;

public class BackgroundSequencer : MonoBehaviour
{
    [SerializeField]
    private ColorTransitioner _colorTransitioner = default;

    [SerializeField]
    private Color[] _colors;

    private int _currentIndex = -1;

    void Start()
    {
        MoveNext();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_colorTransitioner.IsCompleated)
            {
                _colorTransitioner.Skip();
            }
            else { MoveNext(); }
        }
    }

    private void MoveNext()
    {
        if (_colors == null || _colors.Length == 0) { return; }

        if (_currentIndex + 1 < _colors.Length)
        {
            _currentIndex++;
            _colorTransitioner?.Play(_colors[_currentIndex]);
        }
    }
}