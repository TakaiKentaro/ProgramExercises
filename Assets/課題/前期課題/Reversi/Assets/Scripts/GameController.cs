using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using System.Linq;

public class GameController : MonoBehaviour
{
    public enum COLOR
    {
        EMPTY,  //空欄 = 0
        BLACK,  //黒色 = 1
        WHITE   //白色 = 2
    }

    const int WIDTH = 8, HEIGHT = 8;//ここでボードの大きさを決める(8x8)

    public COLOR player = COLOR.BLACK;

    [SerializeField] GameObject Black;//黒の駒
    [SerializeField] GameObject White;//白の駒
    [SerializeField] GameObject BoardEmpty;//盤

    [SerializeField] GameObject boardDisplay = null;//盤のGameObject

    COLOR[,] board = new COLOR[WIDTH, HEIGHT];
    void Start()
    {
        Initialize(); //盤面の初期値を設定
    }
    public void Initialize()
    {
        player = COLOR.BLACK;
        board = new COLOR[WIDTH, HEIGHT];
        board[3, 3] = COLOR.WHITE;
        board[3, 4] = COLOR.BLACK;
        board[4, 3] = COLOR.BLACK;
        board[4, 4] = COLOR.WHITE;
        ShowBoard();
    }
    void ShowBoard()
    {
        foreach (Transform child in boardDisplay.transform)
        {
            Destroy(child.gameObject);//削除
        }

        for (int v = 0; v < HEIGHT; v++)
        {
            for (int h = 0; h < WIDTH; h++)
            {
                // boardの色に合わせて適切なPrefabを取得
                GameObject piece = GetPrefab(board[h, v]);
                if (board[h, v] == COLOR.EMPTY)
                {
                    //座標を一時的に保持
                    int x = h;
                    int y = v;

                    //pieceにイベントを設定
                    piece.GetComponent<Button>().onClick.AddListener(() => { PutStone(x + "," + y); });

                }
                //取得したPrefabをboardDisplayの子オブジェクトにする
                piece.transform.SetParent(boardDisplay.transform);
            }
        }
    }
    //色によって適切なprefabを取得して返す
    GameObject GetPrefab(COLOR color)
    {
        GameObject prefab;
        switch (color)
        {
            case COLOR.EMPTY:   //空欄の時
                prefab = Instantiate(BoardEmpty);
                break;
            case COLOR.BLACK:   //黒の時
                prefab = Instantiate(Black);
                break;
            case COLOR.WHITE:   //白の時
                prefab = Instantiate(White);
                break;
            default:            //それ以外の時(ここに入ることは想定していない)
                prefab = null;
                break;
        }
        return prefab; //取得したPrefabを返す
    }
    public void PutStone(string position)
    {
        int h = int.Parse(position.Split(',')[0]);
        int v = int.Parse(position.Split(',')[1]);
        //画面を表示
        ReverseAll(h, v);
        //駒の色を変更
        if (board[h, v] == player)
        {
            player = player == COLOR.BLACK ? COLOR.WHITE : COLOR.BLACK;
            if (CheckPass())
            {
                player = player == COLOR.BLACK ? COLOR.WHITE : COLOR.BLACK;
                Debug.Log("パス");
                if (CheckPass())
                {
                    CheckGame();
                }
            }
        }
        ShowBoard();
    }
    //1方向にゆっくり返す
    void ReverseAll(int h, int v)
    {
        Reverse(h, v, 1, 0);//右
        Reverse(h, v, -1, 0);//左
        Reverse(h, v, 0, -1);//上
        Reverse(h, v, 0, 1);//下
        Reverse(h, v, 1, -1);//右上
        Reverse(h, v, -1, -1);//左上
        Reverse(h, v, 1, 1);//右下
        Reverse(h, v, -1, 1);//左下
    }
    void Reverse(int h, int v, int directionH, int directionV)
    {
        //確認する座標x,yを宣言
        int x = h + directionH, y = v + directionV;
        //はさんでいるかを確認してひっくり返す
        while (x < WIDTH && x >= 0 && y < HEIGHT && y >= 0)
        {
            if (board[x, y] == player)
            {
                //ひっくり返す処理
                int x2 = h + directionH, y2 = v + directionV;
                int count = 0;
                while (!(x2 == x && y2 == y))
                {
                    board[x2, y2] = player;
                    x2 += directionH;
                    y2 += directionV;
                    count++;
                }
                if (count > 0)
                {
                    board[h, v] = player;
                }
                break;
            }
            else if (board[x, y] == COLOR.EMPTY)
            {
                //はさんでいない時の処理
                break;
            }
            //確認座標
            x += directionH;
            y += directionV;
        }
    }
    bool CheckPass()
    {
        for (int v = 0; v < HEIGHT; v++)
        {
            for (int h = 0; h < WIDTH; h++)
            {
                if (board[h, v] == COLOR.EMPTY)
                {
                    COLOR[,] boardTemp = new COLOR[WIDTH, HEIGHT];
                    Array.Copy(board, boardTemp, board.Length);
                    ReverseAll(h, v);

                    if (board[h, v] == player)
                    {
                        board = boardTemp;
                        return false;
                    }
                }
            }
        }
        return true;
    }
    void CheckGame()
    {
        int black = 0;
        int white = 0;

        for (int v = 0; v < HEIGHT; v++)
        {
            for (int h = 0; h < WIDTH; h++)
            {
                switch (board[h, v])
                {
                    case COLOR.BLACK:
                        black++;
                        break;
                    case COLOR.WHITE:
                        white++;
                        break;
                    default:
                        break;
                }
            }
        }

        if (black > white)
        {
            Debug.Log("黒" + black + "：白" + white + "で黒の勝ち");
        }
        else if (black < white)
        {
            Debug.Log("黒" + black + "：白" + white + "で白の勝ち");
        }
        else
        {
            Debug.Log("黒" + black + "：白" + white + "で引き分け");
        }
    }
}
