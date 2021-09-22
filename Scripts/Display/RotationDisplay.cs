using UnityEngine;
using UnityEngine.UI;

public class RotationDisplay : MonoBehaviour
{
    // Поместим префаб Player в Transform, в иерархии Game Canvas - Rotation Text.
    [SerializeField] private Transform player;
    // Поместим Text в Rotation Text. 
    [SerializeField] private Text rotationText;

    private void Update()
    {
        // Обновляем угол поворота префаба Player и конвертируем число в строку для отображения в игре.
        rotationText.text = "Rotation: " + player.rotation.ToString();
    }
}
