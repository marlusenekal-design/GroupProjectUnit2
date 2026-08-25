using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Android;

public class RogueEnemyMoveSpeed : MonoBehaviour
{
    public GameObject speedUpPanel;
    [SerializeField] private int optionInterval = 100;

    public void ShowSpeedUp(float duration)
    {
        StartCoroutine(SpeedUpRoutine(duration));
    }
    public void Update()
    {
        if (ScoreManager.currentScore >= optionInterval)
        {
            ShowSpeedUp(2f);
            rogueEnemyMoveSpeed();
            optionInterval = (optionInterval + 100);
        }
    }

    private IEnumerator SpeedUpRoutine(float duration)
    {
        /*GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);

            Debug.Log("Enemy Found");
        }*/
        speedUpPanel.SetActive(true);
        yield return new WaitForSeconds(duration);
        speedUpPanel.SetActive(false);

    }
void rogueEnemyMoveSpeed()
    {
        Enemy.moveSpeed++;
    }
}
