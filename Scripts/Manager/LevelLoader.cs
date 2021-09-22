using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelLoader : MonoBehaviour
{
    // Переменная для загрузки начального уровня, настраивается в Game Over Canvas.
    [SerializeField] private int buildIndex;

    private void Update()
    {
        // Загружаем начальный уровень при нажатии клавиши Backspace.
        RestartLevel();
    }

    // Метод для загрузки первого уровня.
    private void RestartLevel()
    {
        // Получаем доступ для ввода с клавиатуры.
        Keyboard kb = InputSystem.GetDevice<Keyboard>();

        // Если нажать Backspace
        if (kb.backspaceKey.wasPressedThisFrame)
        {
            // Загружается первый уровень.
            SceneManager.LoadScene(buildIndex);
        }      
    }
}
