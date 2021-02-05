using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class pausemenu : MonoBehaviour
{
     void Start()
    {
        Time.timeScale = 0;
    }

 
    public void ResumeButton()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("pause");
    }


    public void QuitButton()
    {
        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        SceneManager.LoadScene("menu");

    }
}
