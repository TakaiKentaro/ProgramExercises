using UnityEngine;

/// <summary>
/// メソッドについて
/// </summary>
public class Sample5 : MonoBehaviour
{
    // メソッド
    // クラス（型）に紐づいた関数（実行可能なコードを束ねたもの）

    // メソッド宣言
    // 修飾子 戻り値型 メソッド名（パラメーターリスト）{本体}

    // パラメータ・戻り値なしの最も単純なメソッド
    private void M() { Debug.Log("M"); }

    void Start1()
    {
        // メソッド呼び出し
        // メソッドは、外部からメソッド名で呼び出して実行できる
        Debug.Log("Start 1");
        M(); // メソッド呼び出し・終了するとここに制御が戻る
        Debug.Log("Start 2");
        M(); // 何回でも呼び出せる
    }

    // メソッド・パラメータ
    // メソッドは呼び出し元から任意の値を受け取れる
    // (型1 パラメータ名1,型2 パラメータ名2, ...)
    private void M(int x, int y)
    {
        // メソッド内において、パラメータはローカル変数と同じ
        Debug.Log($"X = {x}, Y = {y}");

        // 基本的には推奨されないが、書き換えも可能
        // x = 123;
        // y = 456;
    }

    // メソッドの戻り値
    // メソッドは処理結果を呼び出し元に返すことができる
    int Add(int x, int y)
    {
        Debug.Log($"Add: X = {x}, Y = {y}");
        return x + y; //戻り値型の値を返さなければならない
    }

    void Start2()
    {
        // メソッドを呼び出すには、パラメータに対応した値を設定する
        // 一般的に、パラメータに渡す値を引数と呼ぶ
        M(10,20);

        var r = Add(123, 456); // メソッドから戻り値を受け取る
        Debug.Log($"Add Return: {r}");

        Add(1, 2); //呼び出し元は戻り値を無視することもできる
    }

    void M(string str)
    {
        // 参照型のパラメータはnullが格納される可能性を考慮しなければならない

        //方針はnullチェックして、無視 or 例外 or 既定値

        //方針１　無視
        //if(str == null) { return; }

        //方針２　例外
        //if (str == null) throw new System.ArgumentException(nameof(str));

        //方針３　既定値
        str ??= ""; //if(str == null) { str = ""; } と同じ


        Debug.Log($"M: len = {str.Length} str = {str}");
    }

    void Start3()
    {
        M("Stand by Ready");//OK
        M(null);//死
    }

    void M(int count)
    {
        if (count < 0){ return; }

        //メソッドの再起処理
        Debug.Log($"N");

        //メソッドは内部で自分自身を呼び出せる
        M(count-1);//無条件呼び出しは、自己を呼び出しの無限ループになる
    }

    void Start4()
    {
        M(10);
    }
}
