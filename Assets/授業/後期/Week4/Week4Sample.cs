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

            // X���̉�]��2�b��
            yield return RotateAsync(Vector3.right, 2);

            //5�b�҂��N���b�N�����̂�҂�
            yield return WhenAny(new WaitForSeconds(5), StartCoroutine(WaitMouseButton(0)));

            // Y���̉�]��2�b��
            yield return RotateAsync(Vector3.up, 2);

            // 1�b�ԑҋ@����
            yield return new WaitForSeconds(1);

            // Z���̉�]��2�b��
            yield return RotateAsync(Vector3.forward, 2);

            // 1�b�ԑҋ@����
            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator WaitClick(float timeout)
    {
        var c = StartCoroutine(WaitMouseButton(0));
        yield return WhenAny(new WaitForSeconds(timeout), c);
    }

    /// <summary>
    /// �}�E�X�̓��͂�҂R���[�`��
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    private IEnumerator WaitMouseButton(int button)
    {
        while (!Input.GetMouseButtonDown(button)) { yield return null; }
    }

    /// <summary>
    /// 2�̃R���[�`���������i�ǂ��炩���I�������I������R���[�`���j
    /// </summary>
    private IEnumerator WhenAny(YieldInstruction c1, YieldInstruction c2)
    {
        bool result = false;
        // c1 ���I�������� result �� true �ɂ���ʂ̔񓯊������𔭉�
        // c2 ���I�������� result �� true �ɂ���ʂ̔񓯊������𔭉�
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