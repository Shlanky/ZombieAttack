using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public GameObject[] music;
    //have dif music w dif tags so we can run if statements and play it based on where it is
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
