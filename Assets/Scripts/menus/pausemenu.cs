using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class pausemenu : MonoBehaviour
{
    public void Start()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);

    }


    public void ResumeButton()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);

    }


    public void QuitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("menu");

    }
}
