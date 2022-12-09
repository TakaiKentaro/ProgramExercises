using System.Collections;
using UnityEngine;

public class Week4Sample : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ExecuteAsync());
    }

    private IEnumerator ExecuteAsync()
    {
        Debug.Log("ExecuteAsync: Begin");

        while (true)
        {
            var t = 0F;

            // X軸の回転を2秒間
            yield return RotateAsync(Vector3.right, 2);

            //5秒待つかクリックされるのを待つ
            yield return WhenAny(new WaitForSeconds(5), StartCoroutine(WaitMouseButton(0)));

            // Y軸の回転を2秒間
            yield return RotateAsync(Vector3.up, 2);

            // 1秒間待機する
            yield return new WaitForSeconds(1);

            // Z軸の回転を2秒間
            yield return RotateAsync(Vector3.forward, 2);

            // 1秒間待機する
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator WaitClick(float timeout)
    {
        var c = StartCoroutine(WaitMouseButton(0));
        yield return WhenAny(new WaitForSeconds(timeout), c);
    }

    /// <summary>
    /// マウスの入力を待つコルーチン
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    private IEnumerator WaitMouseButton(int button)
    {
        while (!Input.GetMouseButtonDown(button)) { yield return null; }
    }

    /// <summary>
    /// 2つのコルーチンを結合（どちらかが終わったら終了するコルーチン）
    /// </summary>
    private IEnumerator WhenAny(YieldInstruction c1, YieldInstruction c2)
    {
        bool result = false;
        // c1 が終了したら result を true にする別の非同期処理を発火
        // c2 が終了したら result を true にする別の非同期処理を発火
        while (!result) { yield return null; }
    }

    private IEnumerator RotateAsync(Vector3 eulers, float duration)
    {
        Debug.Log($"RotateAsync: Begin eulers={eulers}, duration={duration}");
        var t = 0F;
        while (t < duration)
        {
            t += Time.deltaTime;
            transform.Rotate(eulers);
            yield return null;
        }
        Debug.Log("ExecuteAsync: End");
    }
}