using UnityEngine;

/// <summary>
/// クラス説明
/// </summary>
public class Sample6 : MonoBehaviour
{
    //参照渡し3パターン
    private void Ref(ref int value) //読み書き可能参照
    {
        value *= 2; //書き換えOK
        Debug.Log($"Ref valie={value}"); //読み取りOK
    }

    private void In(in int value) //読み取り専用参照
    {
        //value *= 2; //書き開けError
        Debug.Log($"Ref valie={value}"); //読み取りOK
    }

    private void Out(out int value) //出力専用参照
    {
        // value *= 2; //初期化前の参照Error
        value = 1234; //初期化必須
        Debug.Log($"Ref valie={value}"); //読み取りOK
    }

    private void Start()
    {
        var x = 10;
        Ref(ref x); // x は変わるかもしれない
        Debug.Log($"Start:Ref x={x}");

        In(in x); // x の結果は変わらない
        Debug.Log($"Start:In x={x}");

        Out(out x); // x は必ず書き換えられる(出力専用)
        //Out(out var x); //OK
        Debug.Log($"Start:Out x={x}");


        //よくあるGetXXX()系メソッドの対応
        //Transformが存在するなら取得できる
        //存在しなければNullになる
        //var t = GetComponent<Transform>();

        //TryGetXXX()のパターン
        //有効な戻り値が存在するならtrueが返り、out パラメータから結果を取得できる
        //存在しなければfalseが返る
        if (TryGetComponent<Transform>(out var t)) { }
    }

    // 2次元配列を使ったマス目管理で out パラメータが有効なパターン

    /// <summary>
    /// 指定の行番号・列番号のセルを返す。
    /// </summary>
    /// <param name="row">行番号。</param>
    /// <param name="column">列番号。</param>
    /// <param name="cell">指定座標のセル。</param>
    /// <returns>セルが取得できれば true。そうでなければ false。</returns>
    //private bool TryGetCell(int row, int column, out Cell cell)
    //{
    //    var rows = _cells.GetLength(0);
    //    var columns = _cells.GetLength(1);

    //    cell = default; // 仮初期化（早期 return 対応）
    //    if (row < 0 || row >= rows) { return false; }
    //    if (column < 0 || column >= columns) { return false; }

    //    cell = _cells[row, column];
    //    return true;
    //}

    //private void M(params int[] ary) // 受け取る側は配列
    //{
    //    for (var i = 0; i < ary.Length; i++)
    //    {
    //        Debug.Log($"ary[{i}]={ary[i]}");
    //    }
    //}

    //private void Start()
    //{
    //    // 可変引数（渡す側は配列の要素を個別に渡せる）
    //    M(10, 20, 30, 40, 50);
    //    M(new int[] { 10, 20, 30, 40, 50 }); // と実質同じ
    //}
}
