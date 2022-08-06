using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public GameObject music;
    //have dif music w dif tags so we can run if statements and play it based on where it is
    int gameModeHolder;

    private void Awake()
    {
       music = GameObject.FindGameObjectWithTag("Music");
        DontDestroyOnLoad(this.gameObject);
    }

    public void Update()
    {
        leavingMainMenu();
    }

    public void leavingMainMenu()
    {
        gameModeHolder = buttonFunction.gameModeNum;

        if (gameModeHolder != 0)
        {
            Destroy(this.gameObject);
        }
    }
}
