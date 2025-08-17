using UnityEngine;

public class DarkSpell : Spell
{
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TryGetComponent<IDamageable>(out IDamageable damageable);

            Debug.Log("damageable: " + damageable);
            UseSpell(damageable);
        }
    }
}
