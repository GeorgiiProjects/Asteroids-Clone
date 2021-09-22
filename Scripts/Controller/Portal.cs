using UnityEngine;

public class Portal : MonoBehaviour
{
    // ��������� Vector3 ��� ���������� ������������ ���� � ����������� ��������� ������� Player.
    [SerializeField] private Vector3 offset;
    // ������ �����.
    [SerializeField] private float radius = 0.5f;

    // ����� ��� ���������������� ���������� Wall � ����������� ������� Player.
    private void OnTriggerEnter(Collider other)
    {
        // ��������� Wall ������������� � ����������� ������� Player � Player ������������ � ����� ����������.
        other.gameObject.transform.position += offset;
    }

    // ������������ ����� � ������ �� ����.
    private void OnDrawGizmos()
    {
        // ���� ��������� ����� �������.
        Gizmos.color = Color.green;
        // ������������ ����� � ������� Wall � ���������� �� � ������ �������, � �������� 0.5
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
