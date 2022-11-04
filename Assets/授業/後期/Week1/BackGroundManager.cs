using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField]
    private Image _imageA;
    [SerializeField]
    private Image _imageB;

    float red, green, blue, alpha;
    float _time;
    bool check = false;

    private void Start()
    {
        _time = 0;
        check = false;
    }

    private void Update()
    {
        if (!check) { return; }

        _imageA.color = new Color(red, green, blue, alpha);
        alpha += _time;

        if (alpha >= 1)
        {
            check = true;
        }
    }

    public void ChangeBackGround(byte r, byte g, byte b, float time)
    {
        red = r; green = g; blue = b; alpha = 0;
        _time = time;
        check = true;
    }
}
