using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    // �������� Score Text � Total Score Text, � Game Over Canvas - Total Score Text.
    [SerializeField] private Text totalScoreText;

    private void Start()
    {
        // ������� �������� ���-�� ����� �� ����� Game Over.
        totalScoreText.text = "Total Score: " + Score.score;
    }
}
