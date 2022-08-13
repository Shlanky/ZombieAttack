using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public GameObject[] music;
    int gameModeHolder;
    [SerializeField] int scene_num;

    private void Awake()
    {
        music = GameObject.FindGameObjectsWithTag("Music");
        if (music.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Update()
    {
        leavingMainMenu();
    }

    public void leavingMainMenu()
    {
        gameModeHolder = buttonFunction.gameModeNum;

        if (gameModeHolder != scene_num)
        {
            Destroy(this.gameObject);
        }
    }
}
