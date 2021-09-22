using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Создаем неизменяемый класс для запуска в самом начале игры.
    public static UIManager uiManager;
    // Помещаем Score Text в Game Canvas - Score Text.
    [SerializeField] private Text scoreText;
    // Помещаем Lifes Text в Game Canvas - Lifes Text.
    [SerializeField] private Text lifesText;

    private void Awake()
    {
        // Запускаем в самом начале UIManager.
        uiManager = this;
    }

    // Создаем публичный метод обновления кол-ва очков и жизней, метод будет вызываться в скрипте GameManager.
    public void UpdateUI()
    {
        // Обновляем количество очков.
        scoreText.text = "Score: " + GameManager.gameManager.ReadScore();
        // Обновляем количество жизней.
        lifesText.text = "Lifes: " + GameManager.gameManager.ReadLifes();
    }
}
