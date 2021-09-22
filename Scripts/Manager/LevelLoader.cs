using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LevelLoader : MonoBehaviour
{
    // ���������� ��� �������� ���������� ������, ������������� � Game Over Canvas.
    [SerializeField] private int buildIndex;

    private void Update()
    {
        // ��������� ��������� ������� ��� ������� ������� Backspace.
        RestartLevel();
    }

    // ����� ��� �������� ������� ������.
    private void RestartLevel()
    {
        // �������� ������ ��� ����� � ����������.
        Keyboard kb = InputSystem.GetDevice<Keyboard>();

        // ���� ������ Backspace
        if (kb.backspaceKey.wasPressedThisFrame)
        {
            // ����������� ������ �������.
            SceneManager.LoadScene(buildIndex);
        }      
    }
}
