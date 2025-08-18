using UnityEngine;

public class DarkSpell : Spell
{
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
        Invoke(nameof(Destroy), 1.5f);
    }

    public void ActiveBoxCollision()
    {
        boxCollider.enabled = true;
    }
    public void InactiveBoxCollision()
    {
        boxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable == null) return;
            Debug.Log("spell damageable: " + damageable);
            UseSpell(damageable);
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
