using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private bool checkedWin = false;

    void Update()
    {
        if (checkedWin)
            return;

        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        if (npcs.Length == 0)
        {
            checkedWin = true;

            GameManager manager = FindObjectOfType<GameManager>();

            if (manager != null)
            {
                manager.PlayerWon();
            }
        }
    }
}