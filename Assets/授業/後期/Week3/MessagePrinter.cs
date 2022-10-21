using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MessagePrinter : MonoBehaviour
{
    [SerializeField] TMP_Text _textUi = default;

    [SerializeField] string _message = "";

    [SerializeField] float _speed = 1.0F;

    float _elapsed = 0; // 文字を表示してからの経過時間
    float _interval; // 文字毎の待ち時間

    // _message フィールドから表示する現在の文字インデックス。
    // 何も指していない場合は -1 とする。
    int _currentIndex = -1;

    public bool IsPrinting 
    {
        get
        {
            if(_currentIndex + 1 >= _message.Length)
            {
                return true;
            }
            else
            {
                return false;
            }   
        }
    }

    void Start()
    {
        if (_textUi is null) { return; }

        ShowMessage(_message);
    }

    void Update()
    {
        if (_textUi is null || _message is null || _currentIndex + 1 >= _message.Length) { return; }

        _elapsed += Time.deltaTime;
        if (_elapsed > _interval)
        {
            _elapsed = 0;
            _currentIndex++;
            _textUi.text += _message[_currentIndex];
        }
    }

    public void ShowMessage(string message)
    {
        _textUi.text = "";
        _currentIndex = -1;
        _interval = _speed / _message.Length;
        _message = message;
        Debug.Log(_message);
    }
}
