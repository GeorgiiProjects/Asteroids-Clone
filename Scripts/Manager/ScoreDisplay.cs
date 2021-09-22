using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    // Поместим Score Text в Total Score Text, в Game Over Canvas - Total Score Text.
    [SerializeField] private Text totalScoreText;

    private void Start()
    {
        // Выводим набраное кол-во очков на сцену Game Over.
        totalScoreText.text = "Total Score: " + Score.score;
    }
}
