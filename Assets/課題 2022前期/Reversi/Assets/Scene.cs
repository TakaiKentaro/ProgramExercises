using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(Change), 0.1f);
    }
    private void Change()
    {
        SceneManager.LoadScene("title");
    }

}
