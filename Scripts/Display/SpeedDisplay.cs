using UnityEngine;
using UnityEngine.UI;

public class SpeedDisplay : MonoBehaviour
{
    // �������� ������ Player � Rigidbody, � �������� Game Canvas - Speed Text.
    [SerializeField] private Rigidbody playerRB;
    // �������� Text � Speed Text.
    [SerializeField] private Text speedText;
    // ���������� ��� ��������� ������� � �������� ������� Player.
    private float speed;

    private void Update()
    {
        // �������� ������ � �������� � ������������ �� � ��/�.
        speed = playerRB.velocity.magnitude * 3.6f;
        // ��������� �������� ������� Player � ������������ float � int ��� ����������� � ����.
        speedText.text = "Speed: " + ((int)speed);
    }
}
