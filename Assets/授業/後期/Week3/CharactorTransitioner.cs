using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorTransitioner : MonoBehaviour
{
    [SerializeField] private Image _imagePositionLeft; // 左画像
    [SerializeField] private Image _imagePositionSenter; // 中央画像
    [SerializeField] private Image _imagePositionRight; // 右画像

    [SerializeField] private Sprite _toLeft = default;
    [SerializeField] private Sprite _toSenter = default;
    [SerializeField] private Sprite _toRight = default;

    [SerializeField] private float _duration = 1;

    private Sprite _from;
    private float _elapsed = 0;

    public bool IsCompleated1
        => _imagePositionLeft == null ? false : _imagePositionLeft.sprite == _toLeft;

    public bool IsCompleated2
        => _imagePositionSenter == null ? false : _imagePositionSenter.sprite == _toSenter;

    public bool IsCompleated3
        => _imagePositionRight == null ? false : _imagePositionRight.sprite == _toRight;
}