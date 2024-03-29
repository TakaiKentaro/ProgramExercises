using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageClick : MonoBehaviour
{
    [SerializeField,Tooltip("メッセージを表示するTMP_Text")] TMP_Text _messageText;
    [SerializeField,Tooltip("メッセージが流れるスピード")] float _messageSendTime;

    int _messageCount = 0;
    string[] _messageArray;
    string _message1 = "あいうえお";
    string _message2 = "かきくけこ";
    void Start()
    {
        if(_messageText == null)
        {
            _messageText = transform.Find("MessageWindow/MessageText (TMP)").GetComponent<TMP_Text>();
        }

        _messageArray = new string[2];
        _messageArray[0] = _message1;
        _messageArray[1] = _message2;
        _messageText.text = _messageArray[0];
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            NextMessage();
        }
    }

    void NextMessage()
    {
        _messageCount = (_messageCount + 1) % _messageArray.Length;
        _messageText.text = "";
        StopCoroutine(nameof(SendMessage)); //連打対策
        StartCoroutine(nameof(SendMessage));
    }

    IEnumerator SendMessage()
    {
        int count = 0;
        string message = _messageArray[_messageCount];
        
        while (count < message.Length)
        {
            yield return new WaitForSeconds(_messageSendTime);
            _messageText.text += message[count].ToString();
            count++;
        }
    }
}
