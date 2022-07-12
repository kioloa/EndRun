using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestionMenu : MonoBehaviour
{
    public string menuLevel;

    public void ContinueGame()
    {
        SceneManager.LoadScene(menuLevel);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
