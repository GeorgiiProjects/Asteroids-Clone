using UnityEngine;
using UnityEngine.UI;

public class RechargeDisplay : MonoBehaviour
{
    // Поместим Text в Recharge Text.
    [SerializeField] private Text rechargeText;

    // Создаем публичный метод обновления текста, будет вызываться в скрипте PlayerController.  
    public void UpdateRecharge(float count)
    {
        // обновляем время перезарядки.
        rechargeText.text = "Recharge: " +  count;
    }
    
}
