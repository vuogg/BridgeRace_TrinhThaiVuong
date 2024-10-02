using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay : UICanvas
{
    //public void WinButton()
    //{
    //    UIManager.Instance.OpenUI<Win>().score.text = Random.Range(100, 200).ToString();
    //    Close(0);
    //}

    //public void LoseButton()
    //{
    //    UIManager.Instance.OpenUI<Lose>().score.text = Random.Range(0, 100).ToString(); 
    //    Close(0);
    //}
    [SerializeField] Joystick joystick;

    public override void Setup()
    {
        base.Setup();
        LevelManager.Instance.player.Joystick = joystick;
    }


    public void SettingButton()
    {
        UIManager.Instance.OpenUI<Settings>();
    }
}
