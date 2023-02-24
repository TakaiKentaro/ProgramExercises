using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharactorSequencer : MonoBehaviour
{
    [SerializeField] private CharactorTransitioner _charactorTransitioner = default;

    [SerializeField] private Sprite[] _charactorSprites;

    int _currentIndex = -1;
    private void Start()
    {
        MoveNext();
    }

    private void Update()
    {
        throw new NotImplementedException();
    }

    void MoveNext()
    {
        if (_charactorSprites == null || _charactorSprites.Length == 0)
        {
            return;
        }

        if (_currentIndex + 1 > _charactorSprites.Length)
        {
            _currentIndex++;
            
        }
    }
}