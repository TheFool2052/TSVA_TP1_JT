using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoose : MonoBehaviour
{
    private bool gameEnded;
    public string nextLevelName;

    public GameObject winPanel;

    public void WinLevel()
    {
        if (!gameEnded)
        {
            winPanel.SetActive(true);
            
            gameEnded = true;
        }
    }

    public void LoadNextLevel()
    {
        if (nextLevelName != "")
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LooseLevel()
    {
        if(!gameEnded)
        {
            Debug.Log("You Loose");
            RestartLevel();
            gameEnded = true;
        } 
    }
}
