using UnityEngine;
using UnityEngine.UI;

public class SpeedDisplay : MonoBehaviour
{
    // Поместим префаб Player в Rigidbody, в иерархии Game Canvas - Speed Text.
    [SerializeField] private Rigidbody playerRB;
    // Поместим Text в Speed Text.
    [SerializeField] private Text speedText;
    // Переменная для получения доступа к скорости префаба Player.
    private float speed;

    private void Update()
    {
        // Получаем доступ к скорости и конвертируем ее в км/ч.
        speed = playerRB.velocity.magnitude * 3.6f;
        // Обновляем скорость префаба Player и конвертируем float в int для отображения в игре.
        speedText.text = "Speed: " + ((int)speed);
    }
}
