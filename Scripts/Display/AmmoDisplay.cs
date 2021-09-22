using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    // Поместим Text в Ammo Text.
    [SerializeField] private Text ammoText;
    
    // Создаем публичный метод обновления текста, будет вызываться в скрипте PlayerController.
    public void UpdateAmmo(int count)
    {
        // обновляем количество патронов.
        ammoText.text = "Ammo: " + count;
    }
}
