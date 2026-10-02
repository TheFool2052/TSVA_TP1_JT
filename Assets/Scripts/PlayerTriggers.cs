using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    public WinLoose winLooseScript;
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -15.0f)
        {
            winLooseScript.LooseLevel();
        }
    }
}
