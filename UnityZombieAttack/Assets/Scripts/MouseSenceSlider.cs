using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSenceSlider : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public Slider _slider;

    void Awake()
    {
        _slider.onValueChanged.AddListener(Volume);
    }

    public void Start()
    {
        _slider.value = PlayerPrefs.GetFloat("Mouse sense", _slider.value);
    }

    public void OnDisable()
    {
        PlayerPrefs.SetFloat("Mouse sense", _slider.value);
    }

    public void Volume(float _vol)
    {
        //change this to the sfx Slider
        //Master_mixer.SetFloat("Music", Mathf.Log10(_vol) * 20);
        //camera test = new camera();
        //test.senseHori = _vol;
        //test.senseVert = _vol;
    }
}
