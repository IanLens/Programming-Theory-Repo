using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager instance { get; private set; }
    public string playerLightName { get; private set; }
    public string playerDarkName { get; private set; }

    public int playerLightScore { get; private set; } = 0;
    public int playerDarkScore { get; private set; } = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerNames(string pLightName, string pDarkName)
    {
        playerLightName = pLightName;
        playerDarkName = pDarkName;
        ResetPlayerScore();
    }

    public void AddToPlayerScore(PlayerColor playerColor)
    {
        switch (playerColor)
        {
            case PlayerColor.Dark:
                playerDarkScore++;
                break;
            case PlayerColor.Light:
                playerLightScore++;
                break;
            default:
                break;
        }
    }

    public void ResetPlayerScore()
    {
        playerLightScore = 0;
        playerDarkScore = 0;
    }
}
