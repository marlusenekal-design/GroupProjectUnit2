using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RogueLikeScript : MonoBehaviour
{
    /*
    upgrade spawn count
    upgrade enemy damage
    upgrade player total health
    upgrade enemy total health
    improve drop rate
    have a choice of 2-3 random options
*/
    [SerializeField] private GameObject[] roguelikeOptions;
    [SerializeField] private int optionInterval = 100;
    public static bool roguelike = false;


    private void Start()
    {

    }

    public void rogueOptionTiming()
    {
        if (ScoreManager.currentScore >= optionInterval)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);

                Debug.Log("Enemy Found");
            }
            roguelike = true;


            optionInterval = (optionInterval + 100);
        }
    }

}
