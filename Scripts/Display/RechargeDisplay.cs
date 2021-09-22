using UnityEngine;
using UnityEngine.UI;

public class RechargeDisplay : MonoBehaviour
{
    // �������� Text � Recharge Text.
    [SerializeField] private Text rechargeText;

    // ������� ��������� ����� ���������� ������, ����� ���������� � ������� PlayerController.  
    public void UpdateRecharge(float count)
    {
        // ��������� ����� �����������.
        rechargeText.text = "Recharge: " +  count;
    }
    
}
