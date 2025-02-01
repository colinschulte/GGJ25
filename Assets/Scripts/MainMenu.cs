using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource popB;
    public Image ControlScreen;
    public Image CreditsScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        popB.Play();
        SceneManager.LoadSceneAsync(1);
    }

    public void Controls()
    {
        popB.Play();
        ControlScreen.gameObject.SetActive(true);
    }

    public void Credits()
    {
        popB.Play();
        CreditsScreen.gameObject.SetActive(true);
    }

    public void QuitGame() 
    {
        popB.Play();
        Application.Quit();
    }

    public void Back()
    {
        popB.Play();
        ControlScreen.gameObject.SetActive(false);
        CreditsScreen.gameObject.SetActive(false);
    }

    public void BackToMain()
    {
        popB.Play();
        SceneManager.LoadSceneAsync(0);
    }
}
