using UnityEngine;

public class SphereColor : MonoBehaviour
{
    private Renderer sphereRenderer;
    private bool isPlayer;

    void Start()
    {
        sphereRenderer = GetComponent<Renderer>();
        isPlayer = GetComponent<PlayerController>() != null;

        UpdateColor();
    }

    void Update()
    {
        UpdateColor();
    }

    void UpdateColor()
    {
        float size = transform.localScale.x;

        if (isPlayer)
        {
            sphereRenderer.material.color = Color.blue;
            return;
        }

        if (size < 0.8f)
        {
            sphereRenderer.material.color = Color.green;
        }
        else if (size < 1.2f)
        {
            sphereRenderer.material.color = Color.yellow;
        }
        else
        {
            sphereRenderer.material.color = Color.red;
        }
    }
}