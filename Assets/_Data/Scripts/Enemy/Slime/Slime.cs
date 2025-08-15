using UnityEngine;

public class Slime : Character
{
    protected override void Die()
    {
        Destroy(gameObject);
    }

}
