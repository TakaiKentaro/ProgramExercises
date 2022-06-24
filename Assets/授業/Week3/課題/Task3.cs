using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Task3 : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private int _row = 5;

    [SerializeField]
    private int _column = 5;

    string _objName = "Cell";

    [SerializeField] Text _timerText;

    float _timer;
    int _count;
    int _handCount = 0;
    bool clear;

    GridLayoutGroup gridLayoutGroup;
    private void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraintCount = _row;
        for (var r = 0; r < _column; r++)
        {
            for (var c = 0; c < _row; c++)
            {
                var cell = new GameObject($"{_objName}({r}, {c})");
                cell.transform.parent = transform;
                cell.AddComponent<Image>();

                var image = cell.GetComponent<Image>();
                int _rnd = UnityEngine.Random.Range(0, 10);
                if(_rnd % 2 == 0)
                {
                    image.color = Color.white;
                }
                else
                {
                    image.color = Color.black;
                }
            }
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if(!clear)_timerText.text = $"時間:{(int)_timer}秒　手数:{_handCount}";

    }

    /// <summary>
    /// 押したところを変える
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        var cell = eventData.pointerCurrentRaycast.gameObject;
        var image = cell.GetComponent<Image>();
        image.color = image.color == Color.white ? Color.black : Color.white;
        Debug.Log($"{cell}");

        string name = cell.name;

        int ver = int.Parse(name[5].ToString());
        int hor = int.Parse(name[8].ToString());

        _handCount++;

        CrossColorChange(ver, hor);

        _count = 0;

        for (int i = 0; i < _column; i++)
        {
            for (int j = 0; j < _row; j++)
            {
                var imageObj = gameObject.transform.Find($"{_objName}({i}, {j})").gameObject;
                var go = imageObj.GetComponent<Image>();

                if (go.color == Color.black)
                {
                    _count++;
                }
            }
        }

        if (_count == _column * _row)
        {
            clear = true;
        }
    }

    /// <summary>
    /// 十字の色変え
    /// </summary>
    /// <param name="vertical"></param>
    /// <param name="horizontal"></param>
    public void CrossColorChange(int vertical, int horizontal)
    {
        if (vertical - 1 > -1)
        {
            var cell = gameObject.transform.Find($"{_objName}({vertical - 1}, {horizontal})").gameObject;
            var image = cell.GetComponent<Image>();
            image.color = image.color == Color.white ? Color.black : Color.white;

        }
        if (horizontal - 1 > -1)
        {
            var cell = gameObject.transform.Find($"{_objName}({vertical}, {horizontal - 1})").gameObject;
            var image = cell.GetComponent<Image>();
            image.color = image.color == Color.white ? Color.black : Color.white;
        }
        if (vertical + 1 < _column)
        {
            var cell = gameObject.transform.Find($"{_objName}({vertical + 1}, {horizontal})").gameObject;
            var image = cell.GetComponent<Image>();
            image.color = image.color == Color.white ? Color.black : Color.white;
        }
        if (horizontal + 1 < _row)
        {
            var cell = gameObject.transform.Find($"{_objName}({vertical}, {horizontal + 1})").gameObject;
            var image = cell.GetComponent<Image>();
            image.color = image.color == Color.white ? Color.black : Color.white;
        }
    }
}