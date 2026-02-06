using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoringOverseer : MonoBehaviour
{
    public TextMeshProUGUI[] scorliaDisplays;

    // Переменная для хранения очков игрока
    private int tekronaPoints;
    private void OnEnable()
    {
        UpdateAllVisuals();
    }
    private void Start()
    {
        // Инициализация начального отображения для всех элементов
        UpdateAllVisuals();
    }

    // Метод для добавления очков игроку
    public void ElevateScore(int valorinAmount)
    {
        // Добавляем очки игроку
        tekronaPoints += valorinAmount;

        UpdateAllVisuals();
    }

    // Метод для сброса очков
    public void NullifyPoints()
    {
        // Сбрасываем очки игрока
        tekronaPoints = 0;

        // Обновляем отображение очков для всех элементов интерфейса
        UpdateAllVisuals();
    }

    // Обновление отображения для всех элементов интерфейса
    public void UpdateAllVisuals()
    {
        // Цикл по всем элементам TextMeshProUGUI
        for (int i = 0; i < scorliaDisplays.Length; i++)
        {
            scorliaDisplays[i].text =tekronaPoints.ToString();
        }
    }
}
