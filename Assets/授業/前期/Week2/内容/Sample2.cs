using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample2 : MonoBehaviour
{
    private void Start()
    {
        // �񎟌��z��

        // �z��͒���ɓ����f�[�^����ׂ�����
        // ????? <-�v�f��5��1�����z��

        // ����������1�����₵�������񎟌��z��̃C���[�W
        // ?????
        // ????? <-�v�f��5*3��2�����z��
        // ?????

        // �v�f����ׂ邾���Ƃ����_�͒ʏ�̔z��Ɠ���
        // �f�[�^�A�N�Z�X�Ɋe�����̃C���f�b�N�X�𕪗��ł���̂�
        // �\�̂悤�ȍ\���f�[�^��\�����₷��

        // 2�����z��̕ϐ��錾
        // �v�f�^[,] �ϐ���;
        int[,] iArray; // int�^��2�����z��ϐ�

        // 2�����z��̐���
        // new �v�f�^[1�����ڂ̗v�f��, 2�����ڂ̗v�f��];
        iArray = new int[5, 3];

        // 2�����z��̏�����
        // new[,] {{�v�f1, �v�f2}, {�v�f3, �v�f4}, {...}};
        int[,] iArray2 = new int[,] //�v�f�^�Ɨv�f���͏������q���琄�_�\
        {
            { 0,1,2},
            { 10,11,12 },
            { 20,21,22 },
            { 30,31,32 },
            { 40,41,42 },
        };

        // Length�͔z��S�̗̂v�f��(5*3,�܂�15)��Ԃ��B
        Debug.Log($"Length ={iArray2.Length}");

        // �e�������Ƃ̗v�f���� GetLength() ���\�b�h����擾
        Debug.Log($"1������ ={iArray2.GetLength(0)}");
        Debug.Log($"2������ ={iArray2.GetLength(1)}");

        // �z��̎������̎擾
        Debug.Log($"�z��̎����� ={iArray2.Rank}");

        // �v�f�ւ̃A�N�Z�X�̂�����1�����z��Ɠ���
        // �������ƂɃJ���} , ��؂�ŗv�f�ԍ����w�肷��
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


        // ���R�A�J��Ԃ����ŗv�f�ԍ����Ǘ����ăA�N�Z�X�ł���
        // 2�����z��̏ꍇ�A�������Ƃ̌J��Ԃ��������K�v�ɂȂ�
        for (int i = 0; i < iArray.GetLength(0); i++)
        {
            for (int j = 0; j < iArray.GetLength(1); j++)
            {
                Debug.Log($"{i},{j} = {iArray[i, j]}");
            }
        }

    }

}
