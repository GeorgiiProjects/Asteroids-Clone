using UnityEngine;
using UnityEngine.UI;

public class RotationDisplay : MonoBehaviour
{
    // �������� ������ Player � Transform, � �������� Game Canvas - Rotation Text.
    [SerializeField] private Transform player;
    // �������� Text � Rotation Text. 
    [SerializeField] private Text rotationText;

    private void Update()
    {
        // ��������� ���� �������� ������� Player � ������������ ����� � ������ ��� ����������� � ����.
        rotationText.text = "Rotation: " + player.rotation.ToString();
    }
}
