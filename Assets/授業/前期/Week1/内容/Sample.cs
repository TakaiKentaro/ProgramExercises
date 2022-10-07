using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{

    void Start()
    {
        foreach(var t in transform)
        {
            Debug.Log(t);
        }
    }

    /// <summary>
    /// 文字列結合について
    /// </summary>
    void DebugSample()
    {
        int i = 100;

        // +　演算子による文字列結合
        Debug.Log("i = " + i);

        //$から始まる文字列補間を使った場合
        Debug.Log($"i = {i + 10}");

        //上の文字列補間、実態はstring.Format()と同義
        Debug.Log(string.Format("i = {0}", i)); //上と一緒
    }
    /// <summary>
    /// 変数の宣言
    /// </summary>
    void Variable()
    {
        //変数型 変数名　= 初期値
        int i = 100;//整数型の変数
        string str = "文字列";//文字列型の変数

        str = "Stand by Ready!";//変数の上書き可能
        //str = i; //変数が異なる値には代入できない

        //変数型を暗黙的に推論してくれるvarが便利
        var v = 1234;
        // var v; //エラー・初期化は省略できない
    }

    /// <summary>
    /// int型の配列の操作
    /// </summary>
    void intArray()
    {
        //配列型変数の宣言
        //要素型[] 変数名 = 初期値
        int[] intArray; //int型の配列
        intArray = new int[3]; //int型 3要素の配列を生成

        //配列要素へアクセス
        //変数名[要素番号]
        intArray[0] = 10;
        intArray[1] = 20;
        intArray[2] = 30;
        //intArray[3] = 50; //エラー

        Debug.Log($"intArray[0] = {intArray[0]}");
        Debug.Log($"intArray[1] = {intArray[1]}");
        Debug.Log($"intArray[2] = {intArray[2]}");
    }

    /// <summary>
    /// 配列のループ
    /// </summary>
    void Arrayloop()
    {
        //配列初期化子
        //new 要素型[] {値1, 値2, 値3....}
        //var intArray = new int[3] { 10, 20, 30 };
        //要素から型推論可能なら要素型は省略可能
        //[]内の要素数も{}内の要素数から推論可能
        var intArray = new[] { 10, 20, 30 };

        //Lenghtプロパティ:配列の長さを取得する
        Debug.Log($"intArray.Length = {intArray.Length}");

        //for文をループ
        for (int i = 0; i < intArray.Length; i++)
        {
            Debug.Log($"intArray[{i}] = {intArray[i]}");
        }
        //foreachを使ったループ 
        foreach (var e in intArray) //配列何番目か調べたい場合はforを使う
        {
            Debug.Log($"e = {e}");
        }
    }
}
