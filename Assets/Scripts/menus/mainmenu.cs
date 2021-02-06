using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{




    public void PlayButton()
    {

        SceneManager.UnloadSceneAsync("menu");
        SceneManager.LoadScene("gamescene");
        SceneManager.LoadScene("scores", LoadSceneMode.Additive);

    }


    public void QuitButton()
    {
        Application.Quit();
    }
}
