using UnityEngine;
using UnityEngine.UI;

public class CoordinatesDisplay : MonoBehaviour
{
    // Поместим префаб Player в Transform, в иерархии Game Canvas - Coordinates Text.
    [SerializeField] private Transform player;
    // Поместим Text в Coordinates Text. 
    [SerializeField] private Text coordinatesText;
  
    void Update()
    {
        // Обновляем координаты префаба Player и конвертируем число в строку для отображения в игре.
        coordinatesText.text = "Coordinates: " + player.position.ToString();        
    }
}
