using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{
    private IEnumerator Run()
    {
        var selection = new string[]
        {
            "選択肢1",
            "選択肢2",
            "選択肢3",
        };
        Debug.Log("選択肢を表示して入力を待ちます。");
        yield return WaitForSelection(selection, out var awaiter);
        Debug.Log($"選択肢結果は {selection[awaiter.Result]} でした。"); ;
    }

    public IEnumerator WaitForSelection(string[] messages, out IAwaiter<int> awaiter)
    {
        var result = new Awaiter<int>();
        var e = WaitForSelection(messages, result);
        awaiter = result;
        return e;
    }

    private IEnumerator WaitForSelection(string[] messages, Awaiter<int> awaiter)
    {
        // 選択肢を表示して、押されるの待つ処理
        yield return null;
    }
}
