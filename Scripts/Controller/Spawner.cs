using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Заголовок в инспекторе в объекте Spawn Area.
    [Header("Asteroid")]
    // Поместим префаб Asteroid в Spawn Area; 
    [SerializeField] GameObject asteroid;
    // Количество появляющихся префабов Asteroid.
    [SerializeField] private int spawnAmountAsteroid = 5;
    // Количество уже появившихся префабов Asteroid.
    [SerializeField] private int alreadySpawnedAsteroid  = 0;
    // Минимальный поворот префаба Asteroid.
    [SerializeField] private float minAsteroidRotation = 0;
    // Максимальный поворот префаба Asteroid.
    [SerializeField] private float maxAsteroidRotation = 360;
    // Скорость задержки спавна префаба Asteroid при старте игры.
    [SerializeField] private float asteroidSpawnDelayOnStart = 0f;
    // Скорость задержки спавна префаба Asteroid.
    [SerializeField] private float asteroidSpawnDelay = 10f;

    // Заголовок в инспекторе в объекте Spawn Area.
    [Header("UFO")]
    // Количество уже появившихся префабов UFO.
    [SerializeField] private int alreadySpawnedUFO = 0;
    // Количество появляющихся префабов UFO.
    [SerializeField] private int spawnAmountUFO = 1;
    // Поместим префаб UFO в Spawn Area; 
    [SerializeField] GameObject ufo;
    // Скорость задержки спавна префаба UFO при старте игры.
    [SerializeField] private float ufoSpawnDelayOnStart = 7f;
    // Скорость задержки спавна префаба UFO.
    [SerializeField] private float ufoSpawnDelay = 30f;

    // Инициализируем BoxCollider для использования в скрипте.
    BoxCollider spawnAreaCollider;
    // Место в котором будут спавниться префабы Asteroid.
    Vector3 spawnArea;

    void Start()
    {
        // Получаем доступ к BoxCollider Spawn Area.
        spawnAreaCollider = GetComponent<BoxCollider>();
        // Вычисляем размер BoxCollider Spawn Area по осям x и z, по оси y оставляем по умолчанию.
        spawnArea = new Vector3(spawnAreaCollider.bounds.size.x, 0, spawnAreaCollider.bounds.size.z);
        // Со старта игры BoxCollider Spawn Area отключен, для того чтобы избежать багов.
        spawnAreaCollider.enabled = false;
        // Повторяем метод появления префаба UFO, начиная через 3 секунды от старта игры, и повторяя каждые 30 секунд во время игры.
        InvokeRepeating("SpawnUFO", ufoSpawnDelayOnStart, ufoSpawnDelay);
        // Повторяем метод появления префаба Asteroid, начиная через 0 секунд от старта игры, и повторяя каждые 10 секунд во время игры.
        InvokeRepeating("SpawnAsteroid", asteroidSpawnDelayOnStart, asteroidSpawnDelay);
    }

    // Метод спавна префабов Asteroid.
    private void SpawnAsteroid()
    {
        // Пока количество уже появившихся префабов Asteroid < финального количества появившихся префабов Asteroid.
        while (alreadySpawnedAsteroid < spawnAmountAsteroid)
        {
            // Префаб астероид спавнится в случайной позиции по оси x и z, и в позиции по умолчанию по оси y.
            Vector3 asteroidPos = new Vector3(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), 0, Random.Range(-spawnArea.z / 2, spawnArea.z / 2));
            // Если оверлап префаба Asteroid не происходит
            if (!CheckPosition(asteroidPos))
            {
                // Префаб Asteroid поворачивается от 0 до 360 по осям x и z, по оси y оставляем по умолчанию.
                Vector3 asteroidRotation = 
                    new Vector3(Random.Range(minAsteroidRotation, maxAsteroidRotation), 0, Random.Range(minAsteroidRotation, maxAsteroidRotation));
                // Копируем префаб Asteroid в случайных координатах asteroidPos, используем случайный поворот вычислений.
                Instantiate(asteroid, asteroidPos, Quaternion.Euler(asteroidRotation));
                // Префаб Asteroid продолжает спавниться.
                alreadySpawnedAsteroid++;
            }
        }
        // Сбрасываем кол-во префабов Asteroid на ноль.
        alreadySpawnedAsteroid--;
    }

    // Создаем метод для того чтобы не было оверлапа при спавне префаба Asteroid или UFO с какими либо объектами.
    private bool CheckPosition(Vector3 pos)
    {
        // префабы Asteroid или UFO спавнятся без оверлапа.
        return Physics.CheckSphere(pos, 0.8f);
    }

    // Метод спавна префабов Asteroid.
    private void SpawnUFO()
    {
        // Пока количество уже появившихся префабов UFO < 1.
        while (alreadySpawnedUFO < spawnAmountUFO)
        {
            // Префаб UFO спавнится в случайной позиции по оси x и z, и в позиции по умолчанию по оси y.
            Vector3 ufoPos = new Vector3(Random.Range(-spawnArea.x / 2, spawnArea.x / 2), 0, Random.Range(-spawnArea.z / 2, spawnArea.z / 2));
            // Если оверлап префаба UFO не происходит
            if (!CheckPosition(ufoPos))
            {   
                // Копируем префаб UFO в случайных координатах ufoPos, используем поворот по умолчанию.
                Instantiate(ufo, ufoPos, Quaternion.identity);                
                // Префаб UFO продолжает спавниться.
                alreadySpawnedUFO++;
            }           
        }
        // Сбрасываем кол-во префабов UFO на ноль.
        alreadySpawnedUFO--;
    }
}
