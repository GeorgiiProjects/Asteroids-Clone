using UnityEngine;

// Rigidbody автоматически добавляется к префабу UFO.
[RequireComponent(typeof(Rigidbody))]
// SphereCollider автоматически добавляется к префабу UFO и автоматически вычисляются все его параметры.
[RequireComponent(typeof(SphereCollider))]

public class UFOController : MonoBehaviour
{
    // Заголовок в инспекторе в префабе UFO.
    [Header("UFO")]
    // Скорость передвижения префаба UFO.
    [SerializeField] private float moveSpeed = 0.003f;
    // Количество очков за уничтожение префаба UFO.
    [SerializeField] private int ufoScore = 400;
    // Минимальное направление полета префаба UFO.
    [SerializeField] private float minUFODirection = -360;
    // Максимальное направление полета префаба UFO.
    [SerializeField] private float maxUFODirection = 360;

    // Создаем класс Rigidbody, для передвижения объекта и для последующего доступа к нему.
    private Rigidbody ufoRB;
    // Игровой объект с именем Player.
    GameObject player;

    private void Start()
    {
        // получаем доступ к Rigidbody префаба UFO через компонент Rigidbody GetComponent.
        ufoRB = GetComponent<Rigidbody>();
        // Ищем игровой объект в иерархии с именем Player.
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        // Префаб UFO двигается вслед за Player.
        // Для этого используем позицию Player - позицию UFO (получаем Vector3 позиции по которой должен двигаться UFO) * скорость передвижения UFO.
        // Normalized делает так, чтобы UFO двигалось с определенной скоростью, в не зависимости от расстояния до Player.
        Vector3 direction = (player.transform.position - transform.position).normalized;
        // Добавляем силу AddForce, которая будет двигать UFO в направлении Player.      
        ufoRB.AddForce(direction * moveSpeed);
    }

    // Метод для соприкосновения коллайдера префаба UFO с коллайдером префаба Missile или Bomb.
    private void OnTriggerEnter(Collider other)
    {
        // Если у объекта есть тэг "Bullet" или тэг "Bomb"
        if (other.tag == "Bullet" || other.tag == "Bomb")
        {
            // Уничтожаем префаб UFO.
            DestroyUFO();
            // Уничтожаем префаб Missile или префаб Bomb.
            Destroy(other.gameObject);
        }
    }

    // Уничтожение префаба UFO и получение очков за него.
    private void DestroyUFO()
    {   
        // Кол-во очков за уничтожение префаба UFO.
        GameManager.gameManager.AddScore(ufoScore);
        // Уничтожаем префаб UFO.
        Destroy(gameObject);
    }
}
