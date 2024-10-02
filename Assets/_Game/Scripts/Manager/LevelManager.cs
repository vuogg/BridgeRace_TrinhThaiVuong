using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    readonly List<ColorType> colorTypes = new List<ColorType>() { ColorType.Red, ColorType.Blue, ColorType.Green, ColorType.Pink, ColorType.Purple, ColorType.Yellow, ColorType.Orange, ColorType.Black };
    
    public Level[] levelPrefabs;
    public Bot botPrefab;
    public Player player;
    public int CharacterAmount => currentLevel.botAmount + 1;
    private List<Bot> bots = new List<Bot>();
    private Level currentLevel;

    [SerializeField]private int levelIndex;

    private void Awake()
    {
        levelIndex = PlayerPrefs.GetInt("Level", 0);
    }

    public Vector3 FinishPoint => currentLevel.finishPoint.position;

    private void Start()
    {
        LoadLevel(levelIndex - 1);
        OnInit();
        UIManager.Instance.OpenUI<MainMenu>();
    }

    public void OnInit()
    {
        //Init vi tri bat dau game
        Vector3 index = currentLevel.startPoint.position;
        float space = 3f;
        Vector3 leftPoint = ((CharacterAmount / 2) + (CharacterAmount % 2) * 0.5f - 0.5f) * space * Vector3.left + index;

        List<Vector3> startPoints = new List<Vector3>();

        for (int i = 0; i < CharacterAmount; i++)
        {
            startPoints.Add(leftPoint + space * Vector3.right * i);
        }

        //Init random mau` cho character
        List<ColorType> colorDatas = Utilities.SortOrder(colorTypes, CharacterAmount);

        //set ngau nhien vi tri cho player
        int rand = Random.Range(0, CharacterAmount);
        player.TF.position = startPoints[rand];
        player.TF.rotation = Quaternion.identity;
        startPoints.RemoveAt(rand);

        //set mau` random cho player
        player.ChangeColor(colorDatas[rand]);
        colorDatas.RemoveAt(rand);

        player.OnInit();

        //set vi tri va mau` cho bot
        for (int i = 0; i < CharacterAmount - 1; i++)
        {
            //Bot bot = Instantiate(botPrefab, startPoints[i], Quaternion.identity);
            Bot bot = SimplePool.Spawn<Bot>(PoolType.Bot, startPoints[i], Quaternion.identity);
            bot.ChangeColor(colorDatas[i]);
            bot.OnInit();
            bots.Add(bot);
        }
    }

    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (level < levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[level]);
            currentLevel.OnInit();
        }
        else
        {
            //TODO: level vuot qua limit
        }
    }

    public void OnStartGame()
    {
        GameManager.Instance.ChangeState(GameState.Gameplay);
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(new PatrolState());
        }
    }

    public void OnFinishGame()
    {
        for (int i = 0; i < bots.Count; i++)
        {
            bots[i].ChangeState(null);
            bots[i].StopMoving();
        }
    }

    public void OnReset()
    {
        SimplePool.CollectAll();
        //for (int i = 0; i < bots.Count; i++)
        //{
        //    Destroy(bots[i].gameObject);
        //}
        bots.Clear();
        player.ClearBrick();
        player.OnInit();
    }

    internal void OnRetry()
    {
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<MainMenu>();
        GameManager.Instance.ChangeState(GameState.MainMenu);
    }

    internal void OnNextLevel()
    {
        levelIndex++;
        PlayerPrefs.SetInt("Level", levelIndex);
        OnReset();
        LoadLevel(levelIndex);
        OnInit();
        UIManager.Instance.OpenUI<MainMenu>();
    }
}
