using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPlayerText : MonoBehaviour
{
    GameController m_gameController;
    Text m_text;
    void Start()
    {
        m_gameController = GameObject.Find("GameManager").GetComponent<GameController>();
        m_text = this.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string colorText = "";
        switch (m_gameController.player)
        {
            case GameController.COLOR.BLACK:

                colorText = "黒";
                m_text.color = Color.black;
                break;
            case GameController.COLOR.WHITE:
                colorText = "白";
                m_text.color = Color.white;
                break;
            default:
                break;
        }
        m_text.text =colorText;
    }
}
