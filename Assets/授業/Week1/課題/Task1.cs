using UnityEngine;
using UnityEngine.UI;

public class Task1 : MonoBehaviour
{
    [Header(" Ì")]
    [SerializeField, Tooltip(" Ìvf")] int Count = 5;

    [Tooltip("ImageÌzñ")] Image[] _imageArray; //ImageÌzñ

    [Tooltip("ÔÌê")] int _redPoint = 0;
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
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // ¶L[ðµ½
        {
            LeftMove();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) // EL[ðµ½
        {
            RightMove();
        }
        if (Input.GetKeyDown(KeyCode.Space))@//Spaceðµ½
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

                for (var i = 0; i < _imageArray.Length; i++) //ðêÂ¸çµÄÄzu·é
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