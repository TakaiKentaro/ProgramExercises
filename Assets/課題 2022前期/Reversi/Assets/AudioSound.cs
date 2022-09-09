using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSound : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayOneShot()
    {
        GameObject get = GameObject.Find("sound");
        audioSource = get.GetComponent<AudioSource>();
        audioSource.PlayOneShot(audioSource.clip, 1f);
    }
}
