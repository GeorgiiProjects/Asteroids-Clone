using UnityEngine;
using UnityEngine.UI;

public class CoordinatesDisplay : MonoBehaviour
{
    // �������� ������ Player � Transform, � �������� Game Canvas - Coordinates Text.
    [SerializeField] private Transform player;
    // �������� Text � Coordinates Text. 
    [SerializeField] private Text coordinatesText;
  
    void Update()
    {
        // ��������� ���������� ������� Player � ������������ ����� � ������ ��� ����������� � ����.
        coordinatesText.text = "Coordinates: " + player.position.ToString();        
    }
}
