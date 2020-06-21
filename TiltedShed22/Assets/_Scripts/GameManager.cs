using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public void Awake() {
        //quick and dirty singleton
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadLevel() {
        SceneManager.LoadScene(2); //whatever the play scene is numbered
    }

    public void Quit() {
        Application.Quit();
    }
}
