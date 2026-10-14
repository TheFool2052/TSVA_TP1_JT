using UnityEngine;

public class Goal : MonoBehaviour
{
    public WinLoose winLooseScript;
    public GameObject Canvas;

    private void OnTriggerEnter(Collider other)
    {
        Canvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
