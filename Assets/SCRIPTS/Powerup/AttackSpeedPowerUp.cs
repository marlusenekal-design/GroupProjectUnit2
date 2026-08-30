using UnityEngine;

public class AttackSpeedPowerUp : PowerUp
{
    [Header("Attack Speed Settings")]
    [SerializeField] private float fireRateMultiplier = 2.5f;
    [SerializeField] private float duration = 6f;

    protected override void ApplyEffect(GameObject player)
    {
        if (player.TryGetComponent(out Weapon playerWeapon))
        {
            playerWeapon.ActivateAttackSpeedBoost(fireRateMultiplier, duration);
        }
    }
}
