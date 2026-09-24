using UnityEngine;

public class SphereGrowth : MonoBehaviour
{
    public float growthAmount = 0.15f;

    private void OnCollisionEnter(Collision collision)
    {
        SphereGrowth other = collision.gameObject.GetComponent<SphereGrowth>();

        if (other == null)
            return;

        float mySize = transform.localScale.x;
        float otherSize = collision.transform.localScale.x;

        if (mySize > otherSize)
        {
            transform.localScale += Vector3.one * otherSize * growthAmount;
            Destroy(collision.gameObject);
        }
        else if (otherSize > mySize && GetComponent<PlayerController>() != null)
        {
            Debug.Log("PLAYER LOST!");
            Destroy(gameObject);
        }
    }
}