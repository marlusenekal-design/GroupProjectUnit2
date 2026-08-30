using UnityEngine;

public class TripleShotPowerUp : PowerUp
{
    [Header("Buff Settings")]
    public float duration = 8f;

    protected override void ApplyEffect(GameObject player)
    {
        if (player.TryGetComponent(out Weapon playerWeapon))
        {
            playerWeapon.ActivateTripleShot(duration);
        }
    }
}