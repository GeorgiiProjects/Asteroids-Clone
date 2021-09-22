using UnityEngine;

// Rigidbody ������������� ����������� � ������� Asteroid.
[RequireComponent(typeof(Rigidbody))]
// SphereCollider ������������� ����������� � ������� Asteroid � ������������� ����������� ��� ��� ���������.
[RequireComponent(typeof(SphereCollider))]

public class AsteroidController : MonoBehaviour
{
    // ��������� � ���������� � ������� Asteroid.
    [Header("Asteroid Stats")]
    // �������� ������������ ������� Asteroid.
    [SerializeField] private float moveSpeed = 0.003f;
    // �������� ������������ ������� Asteroid � ��������� MEDIUM.
    [SerializeField] private float mediumMoveSpeed = 2;
    // ����������� ����������� ������ ������� Asteroid.
    [SerializeField] private float minAsteroidDirection = -360;
    // ������������ ����������� ������ ������� Asteroid.
    [SerializeField] private float maxAsteroidDirection = 360;
    // ����������� �������� ������� Asteroid.
    [SerializeField] private float minTorque = -50;
    // ������������ �������� ������� Asteroid.
    [SerializeField] private float maxTorque = 50;
    // ����������� ������� ������� Asteroid.
    [SerializeField] private float minAsteroidRotation = 0;
    // ������������ ������� ������� Asteroid.
    [SerializeField] private float maxAsteroidRotation = 360;
    // ���������� ����� �� ������������ ������ Asteroid.
    [SerializeField] private int score = 100;
    // �������������� enum ��� ����������� ������������� � �������.
    [SerializeField] private AsteroidState asteroidState;
    // �������� ������ Asteroid 2 � ������ Asteroid 1.
    [SerializeField] private GameObject asteroidToSpawn;

    // ����������� ������ ������� Asteroid.
    Vector3 direction;

    // ������� enum ��� ��� ���������� ��������� ��������� ������� Asteroid � ���������� ������ � ���� �����.
    public enum AsteroidState
    {
        BIG,
        MEDIUM,
    }

    private void Start()
    {
        // ������ Asteroid ��������� �� -360 �� 360 �� ���� x � z, �� ��� y ��������� �� ���������.
        direction = new Vector3(Random.Range(minAsteroidDirection, maxAsteroidDirection), 0, Random.Range(minAsteroidDirection, maxAsteroidDirection));
        // �������� ������ � Rigidbody ������� Asteroid � ��������� ��� �������� �� ���� x � z �� -50 �� 50, �� ��� y ��������� �� ���������.
        GetComponent<Rigidbody>().AddTorque(Random.Range(minTorque, maxTorque), 0, Random.Range(minTorque, maxTorque));

    }

    private void Update()
    {
        // ���� ������ Asteroid ��������� � ��������� BIG.S
        if (asteroidState == AsteroidState.BIG)
        {
            // ������ Asteroid ��������� � ��������� BIG ������� �����������, �� ��������� ���������� �� ���-�� FPS (� ���������� ��������� �� ����� ��).
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            // ������ Asteroid � ��������� MEDIUM ��������� � ������� �����������, �� ��������� ���������� �� ���-�� FPS (� ���������� ��������� �� ����� ��).
            transform.position += direction * moveSpeed * mediumMoveSpeed * Time.deltaTime;
        }    
    }

    // ����� ��� ��������������� ���������� ������� Asteroid � ����������� ������� Missile ��� Bomb.
    private void OnTriggerEnter(Collider other)
    {
        // ���� � ������� ���� ��� "Bullet"
        if (other.tag == "Bullet")
        {
            // ���������� ������ Missile.
            Destroy(other.gameObject);

            // ���� ��������� ������� Asteroid �������� BIG.
            if (asteroidState == AsteroidState.BIG)
            {
                // �� ��� ��� ���� i < 2 ���� ���������� ������.
                for (int i = 0; i < 2; i++)
                {
                    // ������ Asteroid �������������� �� 0 �� 360 �� ���� x � z, �� ��� y ��������� �� ���������.
                    Vector3 asteroidRotation =
                        new Vector3(Random.Range(minAsteroidRotation, maxAsteroidRotation), 0, Random.Range(minAsteroidRotation, maxAsteroidRotation));
                    // �������� ������ Asteroid � ����������� asteroidToSpawn, ���������� ��������� ������� ����������.
                    GameObject newAsteroid = Instantiate(asteroidToSpawn, transform.position, Quaternion.Euler(asteroidRotation)) as GameObject;
                    // �������� ������ � ���������� ������� Asteroid �������� �������.
                    AsteroidController controller = newAsteroid.GetComponent<AsteroidController>();
                    // �������� ������ � ��������� ������� ������� Asteroid � ������ ��� �������� �������.
                    controller.asteroidState = AsteroidState.MEDIUM;
                    // ���������� ����� �� ����������� ������� Asteroid � ��������� MEDIUM.
                    controller.score = 150;
                }
                // ���������� 150 ����� �� ����������� ������� Asteroid � ��������� �������� �������.
                GameManager.gameManager.AddScore(score);
                // ���������� ������ Asteroid.
                Destroy(gameObject);

            }
            // ���� ��������� ������� Asteroid �������� MEDIUM.
            if (asteroidState == AsteroidState.MEDIUM)
            {
                // ���������� 150 ����� �� ����������� ������� Asteroid � ��������� MEDIUM.
                GameManager.gameManager.AddScore(score);
                // ���������� ������ Asteroid.
                Destroy(gameObject);
            }
        }
        // ��� �� � ������� ���� ��� "Bomb"
        else if (other.tag == "Bomb")
        {
            // ���������� ������ Bomb.
            Destroy(other.gameObject);
            // ���� ��������� ������� Asteroid �������� BIG ��� MEDIUM.
            if (asteroidState == AsteroidState.BIG || asteroidState == AsteroidState.MEDIUM)
            {
                // ���������� ���� �� ����������� ������� Asteroid.
                GameManager.gameManager.AddScore(score);
                // ���������� ������ Asteroid.
                Destroy(gameObject);
            }
        }
    }
}
