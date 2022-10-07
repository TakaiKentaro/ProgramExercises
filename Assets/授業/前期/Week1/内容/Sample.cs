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
    /// �����񌋍��ɂ���
    /// </summary>
    void DebugSample()
    {
        int i = 100;

        // +�@���Z�q�ɂ�镶���񌋍�
        Debug.Log("i = " + i);

        //$����n�܂镶�����Ԃ��g�����ꍇ
        Debug.Log($"i = {i + 10}");

        //��̕������ԁA���Ԃ�string.Format()�Ɠ��`
        Debug.Log(string.Format("i = {0}", i)); //��ƈꏏ
    }
    /// <summary>
    /// �ϐ��̐錾
    /// </summary>
    void Variable()
    {
        //�ϐ��^ �ϐ����@= �����l
        int i = 100;//�����^�̕ϐ�
        string str = "������";//������^�̕ϐ�

        str = "Stand by Ready!";//�ϐ��̏㏑���\
        //str = i; //�ϐ����قȂ�l�ɂ͑���ł��Ȃ�

        //�ϐ��^���ÖٓI�ɐ��_���Ă����var���֗�
        var v = 1234;
        // var v; //�G���[�E�������͏ȗ��ł��Ȃ�
    }

    /// <summary>
    /// int�^�̔z��̑���
    /// </summary>
    void intArray()
    {
        //�z��^�ϐ��̐錾
        //�v�f�^[] �ϐ��� = �����l
        int[] intArray; //int�^�̔z��
        intArray = new int[3]; //int�^ 3�v�f�̔z��𐶐�

        //�z��v�f�փA�N�Z�X
        //�ϐ���[�v�f�ԍ�]
        intArray[0] = 10;
        intArray[1] = 20;
        intArray[2] = 30;
        //intArray[3] = 50; //�G���[

        Debug.Log($"intArray[0] = {intArray[0]}");
        Debug.Log($"intArray[1] = {intArray[1]}");
        Debug.Log($"intArray[2] = {intArray[2]}");
    }

    /// <summary>
    /// �z��̃��[�v
    /// </summary>
    void Arrayloop()
    {
        //�z�񏉊����q
        //new �v�f�^[] {�l1, �l2, �l3....}
        //var intArray = new int[3] { 10, 20, 30 };
        //�v�f����^���_�\�Ȃ�v�f�^�͏ȗ��\
        //[]���̗v�f����{}���̗v�f�����琄�_�\
        var intArray = new[] { 10, 20, 30 };

        //Lenght�v���p�e�B:�z��̒������擾����
        Debug.Log($"intArray.Length = {intArray.Length}");

        //for�������[�v
        for (int i = 0; i < intArray.Length; i++)
        {
            Debug.Log($"intArray[{i}] = {intArray[i]}");
        }
        //foreach���g�������[�v 
        foreach (var e in intArray) //�z�񉽔Ԗڂ����ׂ����ꍇ��for���g��
        {
            Debug.Log($"e = {e}");
        }
    }
}
