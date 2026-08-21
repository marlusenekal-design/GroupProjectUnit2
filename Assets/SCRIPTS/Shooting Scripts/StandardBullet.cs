using UnityEngine;

public class StandardBullet : Projectile
{
    protected override void OnImpact(Collider2D hitCollider)
    {
        base.OnImpact(hitCollider);
    } 
}
