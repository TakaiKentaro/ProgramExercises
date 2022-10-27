using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MessageSequencer : MonoBehaviour
{
    [SerializeField] MessagePrinter _printer = default;

    [SerializeField] string[] _messages = default;

    int _currentIndex = -1;

    void Start()
    {
        MoveNext();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_printer.IsPrinting)
            {
                MoveNext();
            }
            else
            {
                _printer?.Skip();
            }
            Debug.Log(_printer.IsPrinting);
        }
    }

    /// <summary>
    /// ���̃y�[�W�ɐi��
    /// ���̃y�[�W�����݂��Ȃ��ꍇ�͖�������
    /// </summary>
    void MoveNext()
    {
        //�p�^�[���}�b�`���O���g��������@if(_massage == null || _message.Length == 0) {return 0;}�Ɠ��`
        if (_messages is null or { Length: 0 }) { return; }

        if (_currentIndex + 1 < _messages.Length)
        {
            _currentIndex++;
            _printer?.ShowMessage(_messages[_currentIndex]);
        }
    }
}
