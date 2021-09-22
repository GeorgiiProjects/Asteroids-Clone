using UnityEngine;

public class Portal : MonoBehaviour
{
    // Структура Vector3 для назначения расположения сфер и перемещения координат префаба Player.
    [SerializeField] private Vector3 offset;
    // Размер сферы.
    [SerializeField] private float radius = 0.5f;

    // Метод для соприкосновениия коллайдера Wall с коллайдером префаба Player.
    private void OnTriggerEnter(Collider other)
    {
        // Коллайдер Wall соприкасается с коллайдером префаба Player и Player перемещается в новые координаты.
        other.gameObject.transform.position += offset;
    }

    // Отрисовываем сферы и задаем им цвет.
    private void OnDrawGizmos()
    {
        // Цвет созданной сферы зеленый.
        Gizmos.color = Color.green;
        // Отрисовываем сферу в позиции Wall и перемещаем их в нужные позиции, с радиусом 0.5
        Gizmos.DrawWireSphere(transform.position + offset, radius);
    }
}
