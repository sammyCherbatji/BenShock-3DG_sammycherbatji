using UnityEngine;

public class WinCondition : MonoBehaviour
{
    void Update()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NPC");

        if (npcs.Length == 0)
        {
            Debug.Log("YOU WIN!");
        }
    }
}