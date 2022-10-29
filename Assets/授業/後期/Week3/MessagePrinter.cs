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

    float _elapsed = 0; // ������\�����Ă���̌o�ߎ���
    float _interval; // �������̑҂�����

    // _message �t�B�[���h����\�����錻�݂̕����C���f�b�N�X�B
    // �����w���Ă��Ȃ��ꍇ�� -1 �Ƃ���B
    int _currentIndex = -1;

    public bool IsPrinting
    {
        get
        {
            if (_currentIndex == _message.Length - 1)
            {
                return false;
            }
            else
            {
                return true;
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
        if (_textUi == null) { return; }
        _textUi.text = "";
        _currentIndex = -1;
        _elapsed = 0;
        _interval = _speed / _message.Length;
        _message = message;
        Debug.Log(_message);
    }

    /// <summary>
    /// ���ݍĐ����̕����o�͂��ȗ�����B
    /// </summary>
    public void Skip()
    {
        _textUi.text = _message;
        _currentIndex = _message.Length - 1;
    }
}
