using UnityEngine;
using UnityEngine.UI;

public class Task2 : MonoBehaviour
{
    [Header("箱の数")]
    [SerializeField, Tooltip("箱の要素数Y")] int YCount = 5;
    [SerializeField, Tooltip("箱の要素数X")] int XCount = 5;

    [Tooltip("Imageの配列")] Image[,] _imageArray;

    [Tooltip("中身の確認配列")] bool[,] _checkArray;

    [Tooltip("赤の場所Y")] int _YredPoint = 0;
    [Tooltip("赤の場所X")] int _XredPoint = 0;

    [Tooltip("GridLayoutGroup")] GridLayoutGroup _gridLayoutGroup;
    private void Start()
    {
        _gridLayoutGroup = this.gameObject.GetComponent<GridLayoutGroup>();
        _gridLayoutGroup.constraintCount = XCount;

        _imageArray = new Image[YCount, XCount];
        _checkArray = new bool[YCount, XCount];

        for (var r = 0; r < _imageArray.GetLength(0); r++)
        {
            for (var c = 0; c < _imageArray.GetLength(1); c++)
            {
                var obj = new GameObject($"Cell({r}, {c})");
                obj.transform.parent = transform;

                var image = obj.AddComponent<Image>();
                if (r == 0 && c == 0)
                {
                    _imageArray[r, c] = image;
                    image.color = Color.red;
                }
                else
                {
                    _imageArray[r, c] = image;
                    image.color = Color.white;
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // 左キーを押した
        {
            if (_XredPoint > 0)
            {
                if(!_checkArray[_YredPoint, _XredPoint - 1])
                {
                    _XredPoint--;
                    OnSelectedChanged();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // 右キーを押した
        {
            if (_XredPoint < XCount - 1)
            {
                if (!_checkArray[_YredPoint, _XredPoint + 1])
                {
                    _XredPoint++;
                    OnSelectedChanged();
                }  
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow)) // 上キーを押した
        {
            if (_YredPoint > 0)
            {
                if (!_checkArray[_YredPoint - 1, _XredPoint])
                {
                    _YredPoint--;
                    OnSelectedChanged();
                }  
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) // 下キーを押した
        {
            if (_YredPoint < YCount - 1)
            {
                if (!_checkArray[_YredPoint + 1, _XredPoint])
                {
                    _YredPoint++;
                    OnSelectedChanged();
                } 
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnSelectedDestroy();
        }

        void OnSelectedChanged()
        {
            for (int i = 0; i < YCount; i++)
            {
                for (int j = 0; j < XCount; j++)
                {
                    if (_checkArray[i, j]) { continue; }

                    if (i == _YredPoint && j == _XredPoint)
                    {
                        _imageArray[i, j].color = Color.red;
                    }
                    else
                    {
                        _imageArray[i, j].color = Color.white;
                    }

                }
            }
        }

        void OnSelectedDestroy()
        {
            Debug.Log(_YredPoint + " " + _XredPoint);

            _imageArray[_YredPoint, _XredPoint].color = Color.clear;
            _checkArray[_YredPoint, _XredPoint] = true;
        }
    }
}