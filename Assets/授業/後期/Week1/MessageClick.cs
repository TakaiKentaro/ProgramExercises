using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageClick : MonoBehaviour
{
    [SerializeField] TMP_Text _messageText;
    [SerializeField] float _messageSendTime;

    int _messageCount = 0;
    string[] _messageArray;
    string _message1 = "‚ ‚¢‚¤‚¦‚¨";
    string _message2 = "‚©‚«‚­‚¯‚±";
    void Start()
    {
        _messageArray = new string[2];
        _messageArray[0] = _message1;
        _messageArray[1] = _message2;

        _messageText = GetComponent<TMP_Text>();
        _messageText.text = "a";
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            NextMessage();
        }
    }

    void NextMessage()
    {
        _messageCount++;
        _messageText.text = _messageArray[_messageCount % _messageArray.Length];
        StartCoroutine("SendMessage");
    }

    IEnumerator SendMessage()
    {
        int count = 0;
        string message = _messageArray[_messageCount];
        while (count < message.Length)
        {
            yield return new WaitForSeconds(_messageSendTime);
            _messageText.text = message[count].ToString();
            count++;
        }
        yield return new WaitForSeconds(_messageSendTime);
        _messageText.text = message[count].ToString();
    }
}
