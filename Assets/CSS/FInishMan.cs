using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FInishMan : MonoBehaviour
{

    public ScoringOverseer score;
    public SPlaybacker pl;
    public TowerBuilder tower;
    public Transform endi;
    public Transform pause;

    private void OnDisable()
    {
        Time.timeScale = 1;
    }

    public void stopGame()
    {
        pl.erase.Play();
        if (pause.gameObject.activeInHierarchy)
        {
            pause.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pause.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void TheEnd()
    {
        if (!pl.hit.isPlaying)
        { pl.hit.Play(); }
        pause.gameObject.SetActive(false);
        endi.gameObject.SetActive(true);
    }

    public void CloseAll()
    {
        Time.timeScale = 1;
        pause.gameObject.SetActive(false);
        endi.gameObject.SetActive(false);
    }
}
