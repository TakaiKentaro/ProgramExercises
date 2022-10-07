using UnityEngine;
using UnityEngine.UI;

public class Task1 : MonoBehaviour
{
    [Header("���̐�")]
    [SerializeField, Tooltip("���̗v�f��")] int Count = 5;

    [Tooltip("Image�̔z��")] Image[] _imageArray; //Image�̔z��

    [Tooltip("�Ԃ̏ꏊ")] int _redPoint = 0;
    void Start()
    {
        _imageArray = new Image[Count];

        for (var i = 0; i < _imageArray.Length; i++)
        {
            var obj = new GameObject($"Cell{i}");
            obj.transform.parent = transform;

            var image = obj.AddComponent<Image>();
            if (i == 0)
            {
                _imageArray[i] = image;
                image.color = Color.red;
                _redPoint = i;
            }
            else
            {
                _imageArray[i] = image;
                image.color = Color.white;
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // ���L�[��������
        {
            LeftMove();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // �E�L�[��������
        {
            RightMove();
        }
        if (Input.GetKeyDown(KeyCode.Space))�@//Space��������
        {
            ImageDestry();
        }
        
    }
    void LeftMove()
        {
            if (_redPoint > 0)
            {
                _imageArray[_redPoint - 1].color = Color.red;
                _imageArray[_redPoint].color = Color.white;
                _redPoint--;
            }
        }

        void RightMove()
        {
            if (_redPoint < Count - 1)
            {
                _imageArray[_redPoint + 1].color = Color.red;
                _imageArray[_redPoint].color = Color.white;
                _redPoint++;
            }
        }

        void ImageDestry()
        {
            if (Count > 0)
            {
                Count--;
                _redPoint--;

            foreach (var box in _imageArray)
            {
                Destroy(box.gameObject);
            }

            _imageArray = new Image[Count];

                for (var i = 0; i < _imageArray.Length; i++) //��������炵�čĔz�u����
                {
                    var obj = new GameObject($"Cell{i}");
                    obj.transform.parent = transform;

                    var image = obj.AddComponent<Image>();
                    if (i == 0)
                    {
                        _imageArray[i] = image;
                        image.color = Color.red;
                        _redPoint = i;
                    }
                    else
                    {
                        _imageArray[i] = image;
                        image.color = Color.white;
                    }
                }
            }
        }
}