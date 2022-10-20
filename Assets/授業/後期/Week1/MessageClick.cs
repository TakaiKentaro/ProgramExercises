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
    string _message1 = "Ç†Ç¢Ç§Ç¶Ç®";
    string _message2 = "Ç©Ç´Ç≠ÇØÇ±";
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

    private void Update()
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
        StopCoroutine(nameof(SendMessage)); //òAë≈ëŒçÙ
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
            Debug.Log("a");
        }
        //yield return new WaitForSeconds(_messageSendTime);
        //_messageText.text += message[count].ToString();
        
    }
}
