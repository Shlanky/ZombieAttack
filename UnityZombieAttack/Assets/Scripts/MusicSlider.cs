using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.Audio;
using UnityEngine.UI;
public class MusicSlider : MonoBehaviour
{

    [SerializeField] AudioMixer Master_mixer;
    [SerializeField] public Slider _slider;

    void Awake()
    {
        _slider.onValueChanged.AddListener(Volume);
    }

    public void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("Music Vol", _slider.value);
    }

    public void OnDisable()
    {
        PlayerPrefs.SetFloat("Music Vol", _slider.value);
    }

    public void Volume(float _vol)
    {
        //change this to the sfx Slider
        Master_mixer.SetFloat("Music", Mathf.Log10(_vol) * 20);
    }
}
