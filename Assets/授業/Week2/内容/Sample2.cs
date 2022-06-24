using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample2 : MonoBehaviour
{
    private void Start()
    {
        // 二次元配列

        // 配列は直列に同じデータを並べたもの
        // ????? <-要素数5の1次元配列

        // これをさらに1軸増やした物が二次元配列のイメージ
        // ?????
        // ????? <-要素数5*3の2次元配列
        // ?????

        // 要素を並べるだけという点は通常の配列と同じ
        // データアクセスに各次元のインデックスを分離できるので
        // 表のような構造データを表現しやすい

        // 2次元配列の変数宣言
        // 要素型[,] 変数名;
        int[,] iArray; // int型の2次元配列変数

        // 2次元配列の生成
        // new 要素型[1次元目の要素数, 2次元目の要素数];
        iArray = new int[5, 3];

        // 2次元配列の初期化
        // new[,] {{要素1, 要素2}, {要素3, 要素4}, {...}};
        int[,] iArray2 = new int[,] //要素型と要素数は初期化子から推論可能
        {
            { 0,1,2},
            { 10,11,12 },
            { 20,21,22 },
            { 30,31,32 },
            { 40,41,42 },
        };

        // Lengthは配列全体の要素数(5*3,つまり15)を返す。
        Debug.Log($"Length ={iArray2.Length}");

        // 各次元ごとの要素数は GetLength() メソッドから取得
        Debug.Log($"1次元目 ={iArray2.GetLength(0)}");
        Debug.Log($"2次元目 ={iArray2.GetLength(1)}");

        // 配列の次元数の取得
        Debug.Log($"配列の次元数 ={iArray2.Rank}");

        // 要素へのアクセスのやり方は1次元配列と同じ
        // 次元ごとにカンマ , 区切りで要素番号を指定する
        iArray[0, 0] = 0;
        iArray[0, 1] = 1;
        iArray[0, 2] = 2;
        iArray[1, 0] = 10;
        iArray[1, 1] = 11;
        iArray[1, 2] = 12;
        iArray[2, 0] = 20;
        iArray[2, 1] = 21;
        iArray[2, 2] = 22;
        iArray[3, 0] = 30;
        iArray[3, 1] = 31;
        iArray[3, 2] = 32;
        iArray[4, 0] = 40;
        iArray[4, 1] = 41;
        iArray[4, 2] = 42;


        // 当然、繰り返し分で要素番号を管理してアクセスできる
        // 2次元配列の場合、次元ごとの繰り返し処理が必要になる
        for (int i = 0; i < iArray.GetLength(0); i++)
        {
            for (int j = 0; j < iArray.GetLength(1); j++)
            {
                Debug.Log($"{i},{j} = {iArray[i, j]}");
            }
        }

    }

}
