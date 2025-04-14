using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CornerManager : MonoBehaviour
{
    public Image[] corners; // Массив всех углов
    public TextMeshProUGUI scoreText;
    private int _score;
    private int _currentGreenCornerIndex; // Индекс текущего зеленого угла
    

    private void Start()
    {
        SetRandomGreenCorner();
    }

    private void OnEnable()
    {
        PlayerMovement2D.OnPlayerCollidedWithCorner += OnPlayerCollideWithCorner;
    }

    private void OnDisable()
    {
        PlayerMovement2D.OnPlayerCollidedWithCorner -= OnPlayerCollideWithCorner;
    }
    
    // Метод для активации случайного угла
    private void SetRandomGreenCorner()
    {
        // Делаем все углы серыми
        foreach (var corner in corners)
        {
            corner.color = Color.gray; // Серый цвет
        }

        // Случайный угол, который не равен текущему зеленому
        int newGreenCornerIndex;
        do
        {
            newGreenCornerIndex = Random.Range(0, corners.Length);
        } 
        while (newGreenCornerIndex == _currentGreenCornerIndex); // Проверяем, чтобы новый угол не был тем же

        _currentGreenCornerIndex = newGreenCornerIndex;
        corners[_currentGreenCornerIndex].color = Color.green; // Зеленый цвет
    }

    // Метод для обработки столкновения с углом игрока
    private void OnPlayerCollideWithCorner(GameObject corner)
    {
        // Получаем индекс угла, с которым столкнулся игрок
        var collidedCornerIndex = System.Array.IndexOf(corners, corner.GetComponent<Image>());
        
        // Проверяем, что это активный зеленый угол
        if (collidedCornerIndex != _currentGreenCornerIndex) return;
        
        // Активируем новый случайный угол
        SetRandomGreenCorner();
        
        UpdateScore();
        // Лог для отладки
        Debug.Log($"Player collided with active corner: {corner.name}. New green corner activated.");
    }

    private void UpdateScore()
    {
        scoreText.text = $"score: {++_score}";
    }
}
