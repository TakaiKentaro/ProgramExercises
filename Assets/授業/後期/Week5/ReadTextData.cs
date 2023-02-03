using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ReadTextData : MonoBehaviour
{
    [SerializeField] private Text _textLabel;
    [SerializeField] private TextAsset _textFile;

    private string textData;
    private string[] splitText;

    // 改良
    private int currentNum = 0;

    void Start()
    {
        textData = _textFile.text;
        splitText = textData.Split(char.Parse("\n"));

        // 最初は「あいうえお」を表示
        _textLabel.text = splitText[currentNum];
    }

    // 改良
    // スペースキーを押すたびごとに表示される文字列を切り替える。
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            currentNum = (currentNum + 1) % splitText.Length;

            _textLabel.text = splitText[currentNum];
        }
    }
}