using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// スキップ要求の受信側 Actor クラスは SkipRequestToken を受ける。
/// </summary>
public class Actor : MonoBehaviour
{
    [SerializeField]
    private Image _image = default;

    public IEnumerator FadeIn(float time, SkipRequestToken skip)
    {
        Debug.Log($"Actor FadeIn: time={time}", this);
        var color = _image.color;

        // color のアルファ値を徐々に 1 に近づける処理
        var elapsed = 0F;
        while (!skip.IsSkipRequested && elapsed < time)
        {
            elapsed += Time.deltaTime;
            color.a = elapsed / time;
            _image.color = color;
            yield return null;
        }

        color.a = 1;
        _image.color = color;
        yield return null;
    }

    public IEnumerator FadeOut(float time, SkipRequestToken skip)
    {
        Debug.Log($"Actor FadeOut: time={time}", this);
        var color = _image.color;

        // color のアルファ値を徐々に 0 に近づける処理
        var elapsed = 0F;
        while (!skip.IsSkipRequested && elapsed < time)
        {
            elapsed += Time.deltaTime;
            color.a = 1 - elapsed / time;
            _image.color = color;
            yield return null;
        }

        color.a = 0;
        _image.color = color;
        yield return null;
    }
}