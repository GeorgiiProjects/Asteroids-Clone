using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

// Rigidbody ������������� ����������� � ������� Player.
[RequireComponent(typeof(Rigidbody))]
// MeshCollider ������������� ����������� � ������� Player � ������������� ����������� ��� ��� ���������.
[RequireComponent(typeof(MeshCollider))]

public class PlayerController : MonoBehaviour
{
    // ��������� � ���������� � ������� Player.
    [Header("Player Speed")]
    // �������� ������������ ������� Player.
    [SerializeField] private float moveSpeed = 10f;
    // �������� �������� ������� Player.
    [SerializeField] private float rotationSpeed = 100f;

    // ��������� � ���������� � ������� Player.
    [Header("Player Bullet")]
    // �������� ������ Missile � ������ Player.
    [SerializeField] private GameObject missile;
    // �������� ������ Bomb � ������ Player.
    [SerializeField] private GameObject bomb;
    // ����� ������ ��� ��������� ������� Missle � Bomb, �������� ������ Spawn Point � ������ Player.
    [SerializeField] private Transform spawnPoint;

    // ��������� � ���������� � ������� Player.
    [Header("Bomb Bullet")]
    // ������������ ���-�� �������� ������� Bomb.
    [SerializeField] private int maxAmmo = 5;
    // ������� ���-�� �������� ������� Bomb �� �������������.
    [SerializeField] private int currentAmmo = -1;
    // ����� ����������� ������� Bomb.
    [SerializeField] private float reloadTime = 3f;
    // ������� ����� ����������� �� �������������.
    [SerializeField] private float currentReloadTime = -1f;

    // ���������� ��� �������� ��������� �� ������ Player.
    private bool isDead;
    // ����������� �� ��������� ���������.
    private bool isReloading = false;

    // �������������� Rigidbody ��� ������������� � �������.
    Rigidbody playerRB;
    // ��������� ������� ������� Player.
    Vector3 originalPlayerPos;
    // �������������� AmmoDisplay ��� ������������� � �������.
    private AmmoDisplay ammoDisplay;
    // �������������� RechargeDisplay ��� ������������� � �������.
    private RechargeDisplay rechargeDisplay;

    private void Start()
    {
        // �������� ������ � Rigidbody ������� Player.
        playerRB = GetComponent<Rigidbody>();
        // ��������� ������� ������� Player.
        originalPlayerPos = transform.position;
        // ���������� �������� ������� Bomb �� ������ ���� 5.
        currentAmmo = maxAmmo;
        // ����� ����������� 3 �������.
        currentReloadTime = reloadTime;
        // �������� ������ � �������� � Game Canvas - Ammo Text � ��� ������� AmmoDisplay.
        ammoDisplay = GameObject.Find("Ammo Text").GetComponent<AmmoDisplay>();
        // �������� ������ � �������� � Game Canvas - Recharge Text � ��� ������� RechargeDisplay.
        rechargeDisplay = GameObject.Find("Recharge Text").GetComponent<RechargeDisplay>();
    }

    // ���������� FixedUpdate ��� ������������ ������� Player ��� ��� ���������� ������.
    private void FixedUpdate()
    {
        MoveAndRotate();
    }

    private void Update()
    {
        // ���� ������ Player �� ���������
        if (!isDead)
        {
            // ��������� ���-�� ��������
            ammoDisplay.UpdateAmmo(currentAmmo);
            // ��������� ����� ����������� � int (����� ������).
            rechargeDisplay.UpdateRecharge((int)currentReloadTime);
            // �������� ����� ��������.
            Shoot();
        }            
    }

    // ������� ����� ��� ��������.
    private void Shoot()
    {
        // �������� ������ ��� ����� � ����������.
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        // ���� ������ Space
        if (kb.spaceKey.wasPressedThisFrame)
        {
            // ���� ������ Player �� ���������
            if (!isDead)
            {
                // ���� ������ Missile ������������ (�� �������� �����).
                if (missile != null)
                {
                    // �������� ������ Missile � ����������� ������� Spawn Point, ������� ���������� ��� �� ��� � ������� Spawn Point.
                    Instantiate(missile, spawnPoint.position, spawnPoint.rotation);
                }
            }
        }
        // ���� ���� ����������� ������� Bomb
        if (isReloading)
        {
            // ����������� ����, ������� � 3 ������� �� 0. 
            currentReloadTime -= Time.deltaTime;
            // ��������� ����� ����������� � int.
            rechargeDisplay.UpdateRecharge((int)currentReloadTime);
            // ������ ������ �� ������.
            return;
        }
        // ���� �������� ������� Bomb <= 0
        if (currentAmmo <= 0)
        {
            // ��������� �����������.
            StartCoroutine(Reload());
            // ������ ������ �� ������.
            return;
        }

        // ���� ������ B
        else if (kb.bKey.wasPressedThisFrame)
        {
            // ���������� �������� ������� Bomb �����������.
            currentAmmo--;
            // ����������� ���-�� ���������� �������� ������� Bomb.
            ammoDisplay.UpdateAmmo(currentAmmo);
            // ���� ������ Player �� ���������
            if (!isDead)
            {
                // ���� ������ Bomb ������������ (�� �������� �����).
                if (bomb != null)
                {
                    // �������� ������ Bomb � ����������� ������� Spawn Point, ������� ���������� ��� �� ��� � ������� Spawn Point.
                    Instantiate(bomb, spawnPoint.position, spawnPoint.rotation);
                }
            }       
        }
    }

    // ������� ����� ��� ������������ � �������� ������� Player.
    private void MoveAndRotate()
    {
        // �������� ������ ��� ����� � ����������.
        Keyboard kb = InputSystem.GetDevice<Keyboard>();
        // ���� ������ � ���������� ������� ����� ��� ������� A.
        if (kb.leftArrowKey.isPressed || kb.aKey.isPressed)
        {
            // ���� ������ Player �� ���������
            if (!isDead)
            {
                // ������ Player �������������� ������ ����� ��� �����, �� ��������� ���������� �� ���-�� FPS (� ���������� ��������� �� ����� ��).
                transform.RotateAround(transform.position, -Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }
        // ��� �� ������ � ���������� ������� ������ ��� ������� D.
        else if (kb.rightArrowKey.isPressed || kb.dKey.isPressed)
        {
            // ���� ������ Player �� ���������
            if (!isDead)
            {
                // ������ Player �������������� ������ ����� ��� �����, �� ��������� ���������� �� ���-�� FPS (� ���������� ��������� �� ����� ��).
                transform.RotateAround(transform.position, Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }
        // ��� �� ������ � ���������� ������� ����� ��� ������� W.
        else if (kb.upArrowKey.isPressed || kb.wKey.isPressed)
        {
            // ���� ������ Player �� ���������
            if (!isDead)
            {
                // ��������� ���� ������������ � ������� Player, ������ Player ��������� ������.
                playerRB.AddForce(transform.forward * moveSpeed);
                // �������� ��� ������ � �������.
                //transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
            }
        }
    }

    // ������� �������� ��� ����������� ������� Bomb.
    IEnumerator Reload()
    {
        // ����������� ������� Bomb �������.
        isReloading = true;
        // ����������� ���� 3 �������.
        yield return new WaitForSeconds(reloadTime);
        // ���������� �������� ������� Bomb ���������� ������������.
        currentAmmo = maxAmmo;
        // ����� ����������� ���������� ������������.
        currentReloadTime = reloadTime;
        // ����������� ���-�� �������� ������� Bomb.
        ammoDisplay.UpdateAmmo(currentAmmo);
        // ����������� ����� �����������.
        rechargeDisplay.UpdateRecharge((int)currentReloadTime);
        // ����������� ������� Bomb �� �������.
        isReloading = false;
    }

    // ����� ��������������� ���������� ������� Player � ����������� ������� Asteroid ��� ������� UFO. 
    private void OnTriggerEnter(Collider other)
    {
        // ���� � ������� ������� ��� "Asteroid"
        if (other.tag == "Asteroid")
        {
            // ������ Player ���������.
            isDead = true;
            // ��� ��������������� � ��� ����� ���������� �����.
            GameManager.gameManager.LoseLife();
            // ���������� ������ Player ����� 3 �������.
            StartCoroutine(ResetGame());
        }
        // ��� �� � ������� ������� ��� "UFOBullet"
        else if (other.tag == "UFOBullet")
        {
            // ������ Player ���������.
            isDead = true;
            // ��� ��������������� � ��� ����� ���������� �����.
            GameManager.gameManager.LoseLife();
            // ���������� ������ Player ����� 3 �������.
            StartCoroutine(ResetGame());
        }
    }

    // �������� ��� ����������� ������� Player.
    IEnumerator ResetGame()
    {
        // ������ �������� ������������ ������� Player ������ ����.
        playerRB.velocity = Vector3.zero;
        // ��������� ��������� MeshRenderer � ������� Player.
        GetComponent<MeshRenderer>().enabled = false;
        // ��������� ��������� Collider � ������� Player.
        GetComponent<Collider>().enabled = false;
        // ���������� ���������� ���������� ������� Player �� ���������.
        transform.position = originalPlayerPos;
        // ��������� ������ �� 3 �������.
        yield return new WaitForSeconds(3f);
        // �������� ��������� MeshRenderer � ������� Player.
        GetComponent<MeshRenderer>().enabled = true;
        // �������� ��������� Collider � ������� Player.
        GetComponent<Collider>().enabled = true;
        // ������ Player �� ���������.
        isDead = false;
    }
}
