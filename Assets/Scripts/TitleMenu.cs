using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{

    public void StartGame()
    {
        string name = GetComponentInChildren<TMP_InputField>().text;
        if (name != "")
        {
            MainManager.instance.playerName = name;
            SceneManager.LoadScene(1);
        }
    }
}
