using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Создаем неизменяемый класс для запуска в самом начале игры.
    public static GameManager gameManager;
    // Количество очков.
    [SerializeField] private int score;
    // Количество жизней.
    [SerializeField] private int lifes = 3;
    // переменная для сброса прогресса.
    static bool hasLost;

    private void Awake()
    {
        // Запускаем в самом начале GameManager.
        gameManager = this;
    }

    private void Start()
    {
        // Если мы проиграли
        if (hasLost)
        {
            // Очки сбрасываются на 0.
            score = 0;
            // Жизни сбрасываются на 3.
            lifes = 3;
            // переменная для сброса прогресса не активна.
            hasLost = false;
        }
        // Обновляем данные в игре.
        UIManager.uiManager.UpdateUI();
    }

    // Публичный метод для прибавления кол-ва очков после уничтожения префаба Asteroid, будет вызываться в скрипте AsteroidController.
    public void AddScore(int amount)
    {
        // прибавляем определеное кол-во очков.
        score += amount;
        // Обновляем счет в игре.
        UIManager.uiManager.UpdateUI();
    }

    // Публичный метод потери жизней префаба Player, будет вызываться в скрипте Player Controller.
    public void LoseLife()
    {
        // Теряем одну жизнь.
        lifes--;
        // Обновляем кол-во жизней в игре.
        UIManager.uiManager.UpdateUI();
        // Если кол-во жизней <= 0
        if (lifes <= 0)
        {
            // Кол-во очков на сцене Game Over.
            Score.score = score;
            // прогресс сбрасывается.
            hasLost = true;
            // Запускаем сцену Game Over.
            SceneManager.LoadScene("Game Over");
        }
    }

    // Публичный метод обновления жизней префаба Player, будет вызываться в скрипте UIManager.
    public int ReadLifes()
    {
        // обновляем кол-во жизней.
        return lifes;
    }

    // публичный метод обновления очков префаба Player, будет вызываться в скрипте UIManager.
    public int ReadScore()
    {
        // обновляем кол-во очков.
        return score;
    }
}
