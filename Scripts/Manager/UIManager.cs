using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ������� ������������ ����� ��� ������� � ����� ������ ����.
    public static UIManager uiManager;
    // �������� Score Text � Game Canvas - Score Text.
    [SerializeField] private Text scoreText;
    // �������� Lifes Text � Game Canvas - Lifes Text.
    [SerializeField] private Text lifesText;

    private void Awake()
    {
        // ��������� � ����� ������ UIManager.
        uiManager = this;
    }

    // ������� ��������� ����� ���������� ���-�� ����� � ������, ����� ����� ���������� � ������� GameManager.
    public void UpdateUI()
    {
        // ��������� ���������� �����.
        scoreText.text = "Score: " + GameManager.gameManager.ReadScore();
        // ��������� ���������� ������.
        lifesText.text = "Lifes: " + GameManager.gameManager.ReadLifes();
    }
}
