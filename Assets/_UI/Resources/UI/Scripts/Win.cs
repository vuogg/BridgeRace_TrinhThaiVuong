using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : UICanvas
{
    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        Close(1f);
    }

    public void NextButton()
    {
        LevelManager.Instance.OnNextLevel();
        Close(1f);
    }

    //public Text score;

    //public void MainMenuButton()
    //{
    //    UIManager.Instance.OpenUI<MainMenu>();
    //    Close(0);
    //}
}
