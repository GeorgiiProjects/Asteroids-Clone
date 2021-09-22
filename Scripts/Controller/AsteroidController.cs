using UnityEngine;

// Rigidbody автоматически добавляется к префабу Asteroid.
[RequireComponent(typeof(Rigidbody))]
// SphereCollider автоматически добавляется к префабу Asteroid и автоматически вычисляются все его параметры.
[RequireComponent(typeof(SphereCollider))]

public class AsteroidController : MonoBehaviour
{
    // Заголовок в инспекторе в префабе Asteroid.
    [Header("Asteroid Stats")]
    // Скорость передвижения префаба Asteroid.
    [SerializeField] private float moveSpeed = 0.003f;
    // Скорость передвижения префаба Asteroid в состоянии MEDIUM.
    [SerializeField] private float mediumMoveSpeed = 2;
    // Минимальное направление полета префаба Asteroid.
    [SerializeField] private float minAsteroidDirection = -360;
    // Максимальное направление полета префаба Asteroid.
    [SerializeField] private float maxAsteroidDirection = 360;
    // Минимальное вращение префаба Asteroid.
    [SerializeField] private float minTorque = -50;
    // Максимальное вращение префаба Asteroid.
    [SerializeField] private float maxTorque = 50;
    // Минимальный поворот префаба Asteroid.
    [SerializeField] private float minAsteroidRotation = 0;
    // Максимальный поворот префаба Asteroid.
    [SerializeField] private float maxAsteroidRotation = 360;
    // Количество очков за уничтоженный префаб Asteroid.
    [SerializeField] private int score = 100;
    // инициализируем enum для дальнейшего использования в скрипте.
    [SerializeField] private AsteroidState asteroidState;
    // Поместим префаб Asteroid 2 в префаб Asteroid 1.
    [SerializeField] private GameObject asteroidToSpawn;

    // Направление полета префаба Asteroid.
    Vector3 direction;

    // Создаем enum так как используем несколько состояний префаба Asteroid и получается список в виде листа.
    public enum AsteroidState
    {
        BIG,
        MEDIUM,
    }

    private void Start()
    {
        // Префаб Asteroid двигается от -360 до 360 по осям x и z, по оси y оставляем по умолчанию.
        direction = new Vector3(Random.Range(minAsteroidDirection, maxAsteroidDirection), 0, Random.Range(minAsteroidDirection, maxAsteroidDirection));
        // Получаем доступ к Rigidbody префаба Asteroid и добавляем ему вращение по осям x и z от -50 до 50, по оси y оставляем по умолчанию.
        GetComponent<Rigidbody>().AddTorque(Random.Range(minTorque, maxTorque), 0, Random.Range(minTorque, maxTorque));

    }

    private void Update()
    {
        // Если префаб Asteroid находится в состоянии BIG.S
        if (asteroidState == AsteroidState.BIG)
        {
            // Префаб Asteroid двигается в состоянии BIG заданом направлении, со скоростью отвязанной от кол-ва FPS (с одинаковой скоростью на любом пк).
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            // Префаб Asteroid в состоянии MEDIUM двигается в заданом направлении, со скоростью отвязанной от кол-ва FPS (с одинаковой скоростью на любом пк).
            transform.position += direction * moveSpeed * mediumMoveSpeed * Time.deltaTime;
        }    
    }

    // Метод для соприкосновения коллайдера префаба Asteroid с коллайдером префаба Missile или Bomb.
    private void OnTriggerEnter(Collider other)
    {
        // Если у объекта есть тэг "Bullet"
        if (other.tag == "Bullet")
        {
            // Уничтожаем префаб Missile.
            Destroy(other.gameObject);

            // Если состояние префаба Asteroid является BIG.
            if (asteroidState == AsteroidState.BIG)
            {
                // До тех пор пока i < 2 цикл продолжает работу.
                for (int i = 0; i < 2; i++)
                {
                    // Префаб Asteroid поворачивается от 0 до 360 по осям x и z, по оси y оставляем по умолчанию.
                    Vector3 asteroidRotation =
                        new Vector3(Random.Range(minAsteroidRotation, maxAsteroidRotation), 0, Random.Range(minAsteroidRotation, maxAsteroidRotation));
                    // Копируем префаб Asteroid в координатах asteroidToSpawn, используем случайный поворот вычислений.
                    GameObject newAsteroid = Instantiate(asteroidToSpawn, transform.position, Quaternion.Euler(asteroidRotation)) as GameObject;
                    // Получаем доступ к настройкам префаба Asteroid среднего размера.
                    AsteroidController controller = newAsteroid.GetComponent<AsteroidController>();
                    // Получаем доступ к состоянию размера префаба Asteroid и делаем его среднего размера.
                    controller.asteroidState = AsteroidState.MEDIUM;
                    // Количество очков за уничтожение префаба Asteroid в состоянии MEDIUM.
                    controller.score = 150;
                }
                // Прибавляем 150 очков за уничтожение префаба Asteroid в состоянии среднего размера.
                GameManager.gameManager.AddScore(score);
                // Уничтожаем префаб Asteroid.
                Destroy(gameObject);

            }
            // Если состояние префаба Asteroid является MEDIUM.
            if (asteroidState == AsteroidState.MEDIUM)
            {
                // Прибавляем 150 очков за уничтожение префаба Asteroid в состоянии MEDIUM.
                GameManager.gameManager.AddScore(score);
                // Уничтожаем префаб Asteroid.
                Destroy(gameObject);
            }
        }
        // Или же у объекта есть тэг "Bomb"
        else if (other.tag == "Bomb")
        {
            // Уничтожаем префаб Bomb.
            Destroy(other.gameObject);
            // Если состояние префаба Asteroid является BIG или MEDIUM.
            if (asteroidState == AsteroidState.BIG || asteroidState == AsteroidState.MEDIUM)
            {
                // Прибавляем очки за уничтожение префаба Asteroid.
                GameManager.gameManager.AddScore(score);
                // Уничтожаем префаб Asteroid.
                Destroy(gameObject);
            }
        }
    }
}
