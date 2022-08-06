using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MasterSlider : MonoBehaviour
{
    [SerializeField] AudioMixer Master_mixer;
    [SerializeField] Slider _slider;

     void Awake()
    {
        _slider.onValueChanged.AddListener(Volume);

    }

    public void Volume(float _vol)
    {
        Master_mixer.SetFloat("_master", Mathf.Log10(_vol) * 20);
    }
}
