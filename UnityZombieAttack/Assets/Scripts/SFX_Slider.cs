using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SFX_Slider : MonoBehaviour
{
    [SerializeField] AudioMixer Master_mixer;
    [SerializeField] Slider _slider;

    void Awake()
    {
        _slider.onValueChanged.AddListener(Volume);

    }

    public void Volume(float _vol)
    {
        //change this to the sfx Slider
        Master_mixer.SetFloat("SFX", Mathf.Log10(_vol) * 20);
    }
}