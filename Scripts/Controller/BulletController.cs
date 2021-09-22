using UnityEngine;

public class BulletController : MonoBehaviour
{
    // �������� ������������ ������� Missile ��� Bomb.
    [SerializeField] private float moveSpeed = 20f;
    // ���������� ������ Missile ��� Bomb ����� 0.2 �������.
    [SerializeField] private float destroyBullet = 0.2f;

    private void Start()
    {
        // ���������� ������ Missile ��� Bomb ����� 0,2 ������� ����� ���������.
        Destroy(gameObject, destroyBullet);
    }

    private void Update()
    {
        // ������ Missile ��� Bomb ���������� ������ ��� � �������� �����������, �� ��������� ���������� �� ���-�� FPS (� ���������� ��������� �� ����� ��).
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}

