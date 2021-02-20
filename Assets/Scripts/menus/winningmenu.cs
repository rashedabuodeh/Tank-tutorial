using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class winningmenu : MonoBehaviour
{

    public void Start()
    {

    }


    public void playButton()
    {
        gameObject.SetActive(false);
       
        SceneManager.LoadScene("gamescene");
        
        SceneManager.LoadScene("scores",LoadSceneMode.Additive);


    }


    public void QuitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menu");

    }
}
