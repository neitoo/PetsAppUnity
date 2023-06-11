using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarPanel : MonoBehaviour
{
    public Image progressBar;
    public float fillDuration = 2f;
    public float displayDuration = 3f;

    private Coroutine progressCoroutine;

    private void Start()
    {
        // Запускаем процесс заполнения прогресс-бара
        StartProgressBar();
    }

    private void StartProgressBar()
    {
        if (progressCoroutine != null)
        {
            StopCoroutine(progressCoroutine);
        }

        progressCoroutine = StartCoroutine(FillProgressBar());
    }

    private IEnumerator FillProgressBar()
    {
        float elapsedTime = 0f;
        float progress = 0f;
        progressBar.fillAmount = progress;

        // Заполняем прогресс-бар
        while (elapsedTime < fillDuration)
        {
            elapsedTime += Time.deltaTime;
            progress = Mathf.Clamp01(elapsedTime / fillDuration);
            progressBar.fillAmount = progress;

            yield return null;
        }

        // Дожидаемся окончания отображения заполненного прогресс-бара
        yield return new WaitForSeconds(displayDuration);

        // Сбрасываем прогресс-бар
        progressBar.fillAmount = 0f;
        progressCoroutine = null;

        // Прячем панель
        gameObject.SetActive(false);
    }
}
