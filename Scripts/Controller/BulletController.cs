using UnityEngine;

public class BulletController : MonoBehaviour
{
    // Скорость передвижения префаба Missile или Bomb.
    [SerializeField] private float moveSpeed = 20f;
    // Уничтожаем префаб Missile или Bomb через 0.2 секунды.
    [SerializeField] private float destroyBullet = 0.2f;

    private void Start()
    {
        // Уничтожаем префаб Missile или Bomb через 0,2 секунды после появления.
        Destroy(gameObject, destroyBullet);
    }

    private void Update()
    {
        // префаб Missile или Bomb появляется каждый раз в заданных координатах, со скоростью отвязанной от кол-ва FPS (с одинаковой скоростью на любом пк).
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }
}

