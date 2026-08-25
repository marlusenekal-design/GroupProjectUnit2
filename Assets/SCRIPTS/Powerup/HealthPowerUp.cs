using UnityEngine;

public class HealthPowerUp : PowerUp
{
    public int healAmount = 25;

    protected override void ApplyEffect(GameObject player)
    {
        if (player.TryGetComponent(out Health playerHealth))
        {
            playerHealth.Heal(healAmount);
        }
    }
}