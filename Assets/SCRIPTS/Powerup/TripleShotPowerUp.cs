using UnityEngine;

public class TripleShotPowerUp : PowerUp
{
    [Header("Buff Settings")]
    public float duration = 8f; // How long triple shot stays active on the player

    protected override void ApplyEffect(GameObject player)
    {
        if (player.TryGetComponent(out Weapon playerWeapon))
        {
            playerWeapon.ActivateTripleShot(duration);
        }
    }
}