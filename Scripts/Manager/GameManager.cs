using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // ������� ������������ ����� ��� ������� � ����� ������ ����.
    public static GameManager gameManager;
    // ���������� �����.
    [SerializeField] private int score;
    // ���������� ������.
    [SerializeField] private int lifes = 3;
    // ���������� ��� ������ ���������.
    static bool hasLost;

    private void Awake()
    {
        // ��������� � ����� ������ GameManager.
        gameManager = this;
    }

    private void Start()
    {
        // ���� �� ���������
        if (hasLost)
        {
            // ���� ������������ �� 0.
            score = 0;
            // ����� ������������ �� 3.
            lifes = 3;
            // ���������� ��� ������ ��������� �� �������.
            hasLost = false;
        }
        // ��������� ������ � ����.
        UIManager.uiManager.UpdateUI();
    }

    // ��������� ����� ��� ����������� ���-�� ����� ����� ����������� ������� Asteroid, ����� ���������� � ������� AsteroidController.
    public void AddScore(int amount)
    {
        // ���������� ����������� ���-�� �����.
        score += amount;
        // ��������� ���� � ����.
        UIManager.uiManager.UpdateUI();
    }

    // ��������� ����� ������ ������ ������� Player, ����� ���������� � ������� Player Controller.
    public void LoseLife()
    {
        // ������ ���� �����.
        lifes--;
        // ��������� ���-�� ������ � ����.
        UIManager.uiManager.UpdateUI();
        // ���� ���-�� ������ <= 0
        if (lifes <= 0)
        {
            // ���-�� ����� �� ����� Game Over.
            Score.score = score;
            // �������� ������������.
            hasLost = true;
            // ��������� ����� Game Over.
            SceneManager.LoadScene("Game Over");
        }
    }

    // ��������� ����� ���������� ������ ������� Player, ����� ���������� � ������� UIManager.
    public int ReadLifes()
    {
        // ��������� ���-�� ������.
        return lifes;
    }

    // ��������� ����� ���������� ����� ������� Player, ����� ���������� � ������� UIManager.
    public int ReadScore()
    {
        // ��������� ���-�� �����.
        return score;
    }
}
