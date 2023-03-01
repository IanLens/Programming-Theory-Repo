using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour
{
    public TMP_InputField player1InputField;
    public TMP_InputField player2InputField;


    public void StartGame()
    {
        string player1Name = player1InputField.text;
        string player2Name = player2InputField.text;
        if (player1Name != "" && player2Name != "" && player1Name != player2Name)
        {
            MainManager.instance.player1Name = player1Name;
            MainManager.instance.player2Name = player2Name;
            SceneManager.LoadScene(1);
        }
        else
        {
            GameObject errorMessage = transform.Find("ErrorMessage").gameObject;
            errorMessage.SetActive(true);
            if (player1Name != "" && player1Name == player2Name)
            {
                errorMessage.GetComponent<TMP_Text>().text = "Cannot use identical names";
            }
            else
            {
                errorMessage.GetComponent<TMP_Text>().text = "Please enter names for both players";
            }
            
        }
    }
}
