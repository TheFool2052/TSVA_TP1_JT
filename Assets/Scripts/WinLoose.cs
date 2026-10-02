using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLoose : MonoBehaviour
{
    private bool gameEnded;
    public string nextLevelName;

    public void WinLevel()
    {
        if(!gameEnded)
        {
            // llevo 5 horas intentando hacer funcionar una ui con botones y lo botones no registran nada asi que vuelvo para atras
            Debug.Log("You Win");
            if (nextLevelName != "")
            {
                SceneManager.LoadScene(nextLevelName);
            }
            gameEnded = true;
        }
    }

    public void LooseLevel()
    {
        if (!gameEnded)
        {
            // Ese mensaje es 100% intencional...
            Debug.Log("You are Goose");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            gameEnded = true;
        }
    }
}
