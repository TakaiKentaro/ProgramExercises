using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class Week4Sample : MonoBehaviour
{
    private void Update()
    {
        RotateAsync().MoveNext();
    }

    private IEnumerator RotateAsync()
    {
        Debug.Log("RotateAsync");
        while (true)
        {
            transform.Rotate(0, 1, 0);
            yield return null;
        }
    }
}
