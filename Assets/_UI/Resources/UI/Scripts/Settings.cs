using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : UICanvas
{
    public override void Open()
    {
        Time.timeScale = 0;
        base.Open();
    }

    public override void Close(float delayTime)
    {
        base.Close(delayTime);
    }

    public void ContinueButton()
    {
        Time.timeScale = 1;
        Close(0);
    }

    public void RetryButton()
    {
        
        Time.timeScale = 1; // Resume game time
        LevelManager.Instance.OnRetry();
        Close(1f);
    }
}
