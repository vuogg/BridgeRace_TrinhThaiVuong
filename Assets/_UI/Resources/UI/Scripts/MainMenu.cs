using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : UICanvas
{
    public void PlayButton()
    {
        LevelManager.Instance.OnStartGame();

        UIManager.Instance.OpenUI<GamePlay>();
        //UIManager.Instance.CloseUI<MainMenu>();
        Close(0);
    }
}
