using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    private ScoreKeeper scoreKeeper;
    void Start()
    {

    }
    public void loadLevel(string name)
    {
        Debug.Log("Level Load requested for: " + name);
        SceneManager.LoadScene(name);
    }

    public void quitGame()
    {
        Debug.Log("Quit Level Selected");
        Application.Quit();
    }
}
