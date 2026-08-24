using UnityEngine;

public class RogueEnemyMoveSpeed : MonoBehaviour
{
    void rogueEnemyMoveSpeed()
    {
        Enemy.moveSpeed++;

        RogueLikeScript.roguelike = false;
    }
}
