using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RecordsMenu : MonoBehaviour
{

    public string menuLevel;

    public void QuitRecords()
    {
        SceneManager.LoadScene(menuLevel);
    }
}
