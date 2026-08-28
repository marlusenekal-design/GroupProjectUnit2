using UnityEngine;

public class AttackSpeedPowerUp : PowerUp
{
    [Header("Attack Speed Settings")]
    [SerializeField] private float fireRateMultiplier = 2.5f; // Shoots 2.5x faster
    [SerializeField] private float duration = 6f; // Stays active for 6 seconds

    protected override void ApplyEffect(GameObject player)
    {
        if (player.TryGetComponent(out Weapon playerWeapon))
        {
            playerWeapon.ActivateAttackSpeedBoost(fireRateMultiplier, duration);
        }
    }
}
