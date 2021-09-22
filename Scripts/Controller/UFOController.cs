using UnityEngine;

// Rigidbody ������������� ����������� � ������� UFO.
[RequireComponent(typeof(Rigidbody))]
// SphereCollider ������������� ����������� � ������� UFO � ������������� ����������� ��� ��� ���������.
[RequireComponent(typeof(SphereCollider))]

public class UFOController : MonoBehaviour
{
    // ��������� � ���������� � ������� UFO.
    [Header("UFO")]
    // �������� ������������ ������� UFO.
    [SerializeField] private float moveSpeed = 0.003f;
    // ���������� ����� �� ����������� ������� UFO.
    [SerializeField] private int ufoScore = 400;
    // ����������� ����������� ������ ������� UFO.
    [SerializeField] private float minUFODirection = -360;
    // ������������ ����������� ������ ������� UFO.
    [SerializeField] private float maxUFODirection = 360;

    // ������� ����� Rigidbody, ��� ������������ ������� � ��� ������������ ������� � ����.
    private Rigidbody ufoRB;
    // ������� ������ � ������ Player.
    GameObject player;

    private void Start()
    {
        // �������� ������ � Rigidbody ������� UFO ����� ��������� Rigidbody GetComponent.
        ufoRB = GetComponent<Rigidbody>();
        // ���� ������� ������ � �������� � ������ Player.
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        // ������ UFO ��������� ����� �� Player.
        // ��� ����� ���������� ������� Player - ������� UFO (�������� Vector3 ������� �� ������� ������ ��������� UFO) * �������� ������������ UFO.
        // Normalized ������ ���, ����� UFO ��������� � ������������ ���������, � �� ����������� �� ���������� �� Player.
        Vector3 direction = (player.transform.position - transform.position).normalized;
        // ��������� ���� AddForce, ������� ����� ������� UFO � ����������� Player.      
        ufoRB.AddForce(direction * moveSpeed);
    }

    // ����� ��� ��������������� ���������� ������� UFO � ����������� ������� Missile ��� Bomb.
    private void OnTriggerEnter(Collider other)
    {
        // ���� � ������� ���� ��� "Bullet" ��� ��� "Bomb"
        if (other.tag == "Bullet" || other.tag == "Bomb")
        {
            // ���������� ������ UFO.
            DestroyUFO();
            // ���������� ������ Missile ��� ������ Bomb.
            Destroy(other.gameObject);
        }
    }

    // ����������� ������� UFO � ��������� ����� �� ����.
    private void DestroyUFO()
    {   
        // ���-�� ����� �� ����������� ������� UFO.
        GameManager.gameManager.AddScore(ufoScore);
        // ���������� ������ UFO.
        Destroy(gameObject);
    }
}
