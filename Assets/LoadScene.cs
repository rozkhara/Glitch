using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    
    public void LoadSceneA(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene("MapTestScene");
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

}
