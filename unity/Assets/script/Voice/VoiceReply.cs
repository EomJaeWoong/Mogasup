using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceReply : MonoBehaviour
{
    public Slider progress;
    public AudioSource voice;
    private float p = 0f;

    // Start is called before the first frame update
    void Start()
    {
        p = PlayerPrefs.GetFloat("progressmax", 0f);
        progress.value = 0;
    }

    public void SoundSlider() {
        progress.value = p;

        p = voice.time / voice.clip.length;
        PlayerPrefs.SetFloat("progressmax", p);
    }

    // Update is called once per frame
    void Update()
    {
        SoundSlider();
    }

    public void Play() {
        voice.Play();
    }
}
