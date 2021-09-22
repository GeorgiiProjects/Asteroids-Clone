using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// Rigidbody автоматически добавляется к префабу Player.
[RequireComponent(typeof(Rigidbody))]
// MeshCollider автоматически добавляется к префабу Player и автоматически вычисляются все его параметры.
[RequireComponent(typeof(MeshCollider))]

public class PlayerController : MonoBehaviour
{
    // Заголовок в инспекторе в префабе Player.
    [Header("Player Speed")]
    // Скорость передвижения префаба Player.
    [SerializeField] private float moveSpeed = 10f;
    // Скорость поворота префаба Player.
    [SerializeField] private float rotationSpeed = 100f;

    // Заголовок в инспекторе в префабе Player.
    [Header("Player Bullet")]
    // Поместим префаб Missile в префаб Player.
    [SerializeField] private GameObject missile;
    // Поместим префаб Bomb в префаб Player.
    [SerializeField] private GameObject bomb;
    // Точка спавна для появления префаба Missle и Bomb, помещаем префаб Spawn Point в префаб Player.
    [SerializeField] private Transform spawnPoint;

    // Заголовок в инспекторе в префабе Player.
    [Header("Bomb Bullet")]
    // Максимальное кол-во патронов префаба Bomb.
    [SerializeField] private int maxAmmo = 5;
    // Текущее кол-во патронов префаба Bomb от максимального.
    [SerializeField] private int currentAmmo = -1;
    // Время перезарядки префаба Bomb.
    [SerializeField] private float reloadTime = 3f;
    // Текущее время перезарядки от максимального.
    [SerializeField] private float currentReloadTime = -1f;

    // переменная для проверки уничтожен ли префаб Player.
    private bool isDead;
    // Перезарядка по умолчанию отключена.
    private bool isReloading = false;

    // Инициализируем Rigidbody для использования в скрипте.
    Rigidbody playerRB;
    // Начальная позиция префаба Player.
    Vector3 originalPlayerPos;
    // Инициализируем AmmoDisplay для использования в скрипте.
    private AmmoDisplay ammoDisplay;
    // Инициализируем RechargeDisplay для использования в скрипте.
    private RechargeDisplay rechargeDisplay;

    private void Start()
    {
        // Получаем доступ к Rigidbody префаба Player.
        playerRB = GetComponent<Rigidbody>();
        // Начальная позиция префаба Player.
        originalPlayerPos = transform.position;
        // Количество патронов префаба Bomb со старта игры 5.
        currentAmmo = maxAmmo;
        // Время перезарядки 3 секунды.
        currentReloadTime = reloadTime;
        // получаем доступ в иерархии к Game Canvas - Ammo Text и его скрипту AmmoDisplay.
        ammoDisplay = GameObject.Find("Ammo Text").GetComponent<AmmoDisplay>();
        // получаем доступ в иерархии к Game Canvas - Recharge Text и его скрипту RechargeDisplay.
        rechargeDisplay = GameObject.Find("Recharge Text").GetComponent<RechargeDisplay>();
    }

    // Используем FixedUpdate для передвижения префаба Player так как используем физику.
    private void FixedUpdate()
    {
        MoveAndRotate();
    }

    private void Update()
    {
        // Если префаб Player не уничтожен
        if (!isDead)
        {
            // Обновляем кол-во патронов
            ammoDisplay.UpdateAmmo(currentAmmo);
            // Обновляем время перезарядки в int (целых числах).
            rechargeDisplay.UpdateRecharge((int)currentReloadTime);
            // Вызываем метод стрельбы.
            Shoot();
        }            
    }

    // Создаем метод для стрельбы.
    private void Shoot()
    {
        // Получаем доступ для ввода с клавиатуры.
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        // Если нажать Space
        if (kb.spaceKey.wasPressedThisFrame)
        {
            // Если префаб Player не уничтожен
            if (!isDead)
            {
                // Если префаб Missile присутствует (не является нулем).
                if (missile != null)
                {
                    // Копируем префаб Missile в координатах префаба Spawn Point, поворот происходит так же как у префаба Spawn Point.
                    Instantiate(missile, spawnPoint.position, spawnPoint.rotation);
                }
            }
        }
        // Если идет перезарядка префаба Bomb
        if (isReloading)
        {
            // Перезарядка идет, начиная с 3 секунды до 0. 
            currentReloadTime -= Time.deltaTime;
            // Обновляем время перезарядки в int.
            rechargeDisplay.UpdateRecharge((int)currentReloadTime);
            // Больше ничего не делаем.
            return;
        }
        // Если патронов префаба Bomb <= 0
        if (currentAmmo <= 0)
        {
            // Запускаем перезарядку.
            StartCoroutine(Reload());
            // Больше ничего не делаем.
            return;
        }

        // Если нажать B
        else if (kb.bKey.wasPressedThisFrame)
        {
            // Количество патронов префаба Bomb уменьшается.
            currentAmmo--;
            // Обновляется кол-во оставшихся патронов префаба Bomb.
            ammoDisplay.UpdateAmmo(currentAmmo);
            // Если префаб Player не уничтожен
            if (!isDead)
            {
                // Если префаб Bomb присутствует (не является нулем).
                if (bomb != null)
                {
                    // Копируем префаб Bomb в координатах префаба Spawn Point, поворот происходит так же как у префаба Spawn Point.
                    Instantiate(bomb, spawnPoint.position, spawnPoint.rotation);
                }
            }       
        }
    }

    // Создаем метод для передвижения и поворота префаба Player.
    private void MoveAndRotate()
    {
        // Получаем доступ для ввода с клавиатуры.
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        // Если нажать и удерживать стрелку влево или клавишу A.
        if (kb.leftArrowKey.isPressed || kb.aKey.isPressed)
        {
            // Если префаб Player не уничтожен
            if (!isDead)
            {
                // Префаб Player поворачивается вокруг своей оси влево, со скоростью отвязанной от кол-ва FPS (с одинаковой скоростью на любом пк).
                transform.RotateAround(transform.position, -Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }
        // Или же нажать и удерживать стрелку вправо или клавишу D.
        else if (kb.rightArrowKey.isPressed || kb.dKey.isPressed)
        {
            // Если префаб Player не уничтожен
            if (!isDead)
            {
                // Префаб Player поворачивается вокруг своей оси влево, со скоростью отвязанной от кол-ва FPS (с одинаковой скоростью на любом пк).
                transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }
        // Или же нажать и удерживать стрелку вверх или клавишу W.
        else if (kb.upArrowKey.isPressed || kb.wKey.isPressed)
        {
            // Если префаб Player не уничтожен
            if (!isDead)
            {
                // Добавляем силу передвижения к префабу Player, префаб Player двигается вперед.
                playerRB.AddForce(transform.forward * moveSpeed);
                // Движение без физики и инерции.
                //transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            }
        }
    }

    // Создаем курутину для перезарядки префаба Bomb.
    IEnumerator Reload()
    {
        // Перезарядка префаба Bomb активна.
        isReloading = true;
        // Перезарядка идет 3 секунды.
        yield return new WaitForSeconds(reloadTime);
        // Количество патронов префаба Bomb становится максимальным.
        currentAmmo = maxAmmo;
        // Время перезарядки становится максимальным.
        currentReloadTime = reloadTime;
        // Обновляется кол-во патронов префаба Bomb.
        ammoDisplay.UpdateAmmo(currentAmmo);
        // Обновляется время перезарядки.
        rechargeDisplay.UpdateRecharge((int)currentReloadTime);
        // Перезарядка префаба Bomb не активна.
        isReloading = false;
    }

    // Метод соприкосновения коллайдера префаба Player с коллайдером префаба Asteroid или префаба UFO. 
    private void OnTriggerEnter(Collider other)
    {
        // Если у объекта имеется тэг "Asteroid"
        if (other.tag == "Asteroid")
        {
            // Префаб Player уничтожен.
            isDead = true;
            // При соприкосновении с ним будет убавляться жизнь.
            GameManager.gameManager.LoseLife();
            // Возрождаем префаб Player через 3 секунды.
            StartCoroutine(ResetGame());
        }
        // Или же у объекта имеется тэг "UFOBullet"
        else if (other.tag == "UFOBullet")
        {
            // Префаб Player уничтожен.
            isDead = true;
            // При соприкосновении с ним будет убавляться жизнь.
            GameManager.gameManager.LoseLife();
            // Возрождаем префаб Player через 3 секунды.
            StartCoroutine(ResetGame());
        }
    }

    // Курутина для возрождения префаба Player.
    IEnumerator ResetGame()
    {
        // Делаем скорость передвижения префаба Player равную нулю.
        playerRB.velocity = Vector3.zero;
        // Отключаем компонент MeshRenderer у префаба Player.
        GetComponent<MeshRenderer>().enabled = false;
        // Отключаем компонент Collider у префаба Player.
        GetComponent<Collider>().enabled = false;
        // Сбрасываем координаты нахождения префаба Player на начальные.
        transform.position = originalPlayerPos;
        // Запускаем таймер на 3 секунды.
        yield return new WaitForSeconds(3f);
        // Включаем компонент MeshRenderer у префаба Player.
        GetComponent<MeshRenderer>().enabled = true;
        // Включаем компонент Collider у префаба Player.
        GetComponent<Collider>().enabled = true;
        // Префаб Player не уничтожен.
        isDead = false;
    }
}
