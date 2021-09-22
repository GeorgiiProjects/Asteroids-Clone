using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    // �������� Text � Ammo Text.
    [SerializeField] private Text ammoText;
    
    // ������� ��������� ����� ���������� ������, ����� ���������� � ������� PlayerController.
    public void UpdateAmmo(int count)
    {
        // ��������� ���������� ��������.
        ammoText.text = "Ammo: " + count;
    }
}
