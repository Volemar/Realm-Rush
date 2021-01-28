using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int hits = 5;

    void AddNonTriggerBoxCollider()
    {
        BoxCollider collider = gameObject.AddComponent<BoxCollider>();
        collider.isTrigger = false;
        collider.size = new Vector3(10,10,10);
    }

    private void OnParticleCollision(GameObject other)
    {
        hits--;
        if (hits <= 0)
        {
            StartDeathSequence();
        }
    }

    void StartDeathSequence()
    {
        Destroy(gameObject);
    }
}
