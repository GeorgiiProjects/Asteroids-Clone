using UnityEngine;

public class Spawner : MonoBehaviour
{
    // ��������� � ���������� � ������� Spawn Area.
    [Header("Asteroid")]
    // �������� ������ Asteroid � Spawn Area; 
    [SerializeField] GameObject asteroid;
    // ���������� ������������ �������� Asteroid.
    [SerializeField] private int spawnAmountAsteroid = 5;
    // ���������� ��� ����������� �������� Asteroid.
    [SerializeField] private int alreadySpawnedAsteroid  = 0;
    // ����������� ������� ������� Asteroid.
    [SerializeField] private float minAsteroidRotation = 0;
    // ������������ ������� ������� Asteroid.
    [SerializeField] private float maxAsteroidRotation = 360;
    // �������� �������� ������ ������� Asteroid ��� ������ ����.
    [SerializeField] private float asteroidSpawnDelayOnStart = 0f;
    // �������� �������� ������ ������� Asteroid.
    [SerializeField] private float asteroidSpawnDelay = 10f;

    // ��������� � ���������� � ������� Spawn Area.
    [Header("UFO")]
    // ���������� ��� ����������� �������� UFO.
    [SerializeField] private int alreadySpawnedUFO = 0;
    // ���������� ������������ �������� UFO.
    [SerializeField] private int spawnAmountUFO = 1;
    // �������� ������ UFO � Spawn Area; 
    [SerializeField] GameObject ufo;
    // �������� �������� ������ ������� UFO ��� ������ ����.
    [SerializeField] private float ufoSpawnDelayOnStart = 7f;
    // �������� �������� ������ ������� UFO.
    [SerializeField] private float ufoSpawnDelay = 30f;

    // �������������� BoxCollider ��� ������������� � �������.
    BoxCollider spawnAreaCollider;
    // ����� � ������� ����� ���������� ������� Asteroid.
    Vector3 spawnArea;

    void Start()
    {
        // �������� ������ � BoxCollider Spawn Area.
        spawnAreaCollider = GetComponent<BoxCollider>();
        // ��������� ������ BoxCollider Spawn Area �� ���� x � z, �� ��� y ��������� �� ���������.
        spawnArea = new Vector3(spawnAreaCollider.bounds.size.x, 0, spawnAreaCollider.bounds.size.z);
        // �� ������ ���� BoxCollider Spawn Area ��������, ��� ���� ����� �������� �����.
        spawnAreaCollider.enabled = false;
        // ��������� ����� ��������� ������� UFO, ������� ����� 3 ������� �� ������ ����, � �������� ������ 30 ������ �� ����� ����.
        InvokeRepeating("SpawnUFO", ufoSpawnDelayOnStart, ufoSpawnDelay);
        // ��������� ����� ��������� ������� Asteroid, ������� ����� 0 ������ �� ������ ����, � �������� ������ 10 ������ �� ����� ����.
        InvokeRepeating("SpawnAsteroid", asteroidSpawnDelayOnStart, asteroidSpawnDelay);
    }

    // ����� ������ �������� Asteroid.
    private void SpawnAsteroid()
    {
        // ���� ���������� ��� ����������� �������� Asteroid < ���������� ���������� ����������� �������� Asteroid.
        while (alreadySpawnedAsteroid < spawnAmountAsteroid)
        {
            // ������ �������� ��������� � ��������� ������� �� ��� x � z, � � ������� �� ��������� �� ��� y.
            Vector3 asteroidPos = new Vector3(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), 0, Random.Range(-spawnArea.z / 2, spawnArea.z / 2));
            // ���� ������� ������� Asteroid �� ����������
            if (!CheckPosition(asteroidPos))
            {
                // ������ Asteroid �������������� �� 0 �� 360 �� ���� x � z, �� ��� y ��������� �� ���������.
                Vector3 asteroidRotation = 
                    new Vector3(Random.Range(minAsteroidRotation, maxAsteroidRotation), 0, Random.Range(minAsteroidRotation, maxAsteroidRotation));
                // �������� ������ Asteroid � ��������� ����������� asteroidPos, ���������� ��������� ������� ����������.
                Instantiate(asteroid, asteroidPos, Quaternion.Euler(asteroidRotation));
                // ������ Asteroid ���������� ����������.
                alreadySpawnedAsteroid++;
            }
        }
        // ���������� ���-�� �������� Asteroid �� ����.
        alreadySpawnedAsteroid--;
    }

    // ������� ����� ��� ���� ����� �� ���� �������� ��� ������ ������� Asteroid ��� UFO � ������ ���� ���������.
    private bool CheckPosition(Vector3 pos)
    {
        // ������� Asteroid ��� UFO ��������� ��� ��������.
        return Physics.CheckSphere(pos, 0.8f);
    }

    // ����� ������ �������� Asteroid.
    private void SpawnUFO()
    {
        // ���� ���������� ��� ����������� �������� UFO < 1.
        while (alreadySpawnedUFO < spawnAmountUFO)
        {
            // ������ UFO ��������� � ��������� ������� �� ��� x � z, � � ������� �� ��������� �� ��� y.
            Vector3 ufoPos = new Vector3(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), 0, Random.Range(-spawnArea.z / 2, spawnArea.z / 2));
            // ���� ������� ������� UFO �� ����������
            if (!CheckPosition(ufoPos))
            {   
                // �������� ������ UFO � ��������� ����������� ufoPos, ���������� ������� �� ���������.
                Instantiate(ufo, ufoPos, Quaternion.identity);                
                // ������ UFO ���������� ����������.
                alreadySpawnedUFO++;
            }           
        }
        // ���������� ���-�� �������� UFO �� ����.
        alreadySpawnedUFO--;
    }
}
