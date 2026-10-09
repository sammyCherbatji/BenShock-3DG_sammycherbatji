
using UnityEngine;

public class CircularInvader : MonoBehaviour
{
    public float radius = 1.5f;
    public float rotationSpeed = 1f;

    private Vector3 centre;
    private float angle;

    void Start()
    {
        centre = transform.position;
        angle = Random.Range(0f, 360f);
    }

    void Update()
    {
        angle += rotationSpeed * 60f * Time.deltaTime;

        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

        transform.position = centre + new Vector3(x, y, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health =
                collision.gameObject.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.LoseLife();
            }
        }
    }
}
