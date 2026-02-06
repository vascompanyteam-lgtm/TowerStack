using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMap : MonoBehaviour
{
    public Transform loadingMap;
    public Transform menuMap;
    public Transform playMap;
    public Transform playObjects;
    public Transform settingsMap;
    public Transform InfoMap;
    public Transform BestMap;
    public Transform DailyMap;
    public SPlaybacker pl;
    public void disbaleEver()
    {
        loadingMap.gameObject.SetActive(false);
        menuMap.gameObject.SetActive(false);
        playMap.gameObject.SetActive(false);
        playObjects.gameObject.SetActive(false);
        settingsMap.gameObject.SetActive(false);
        InfoMap.gameObject.SetActive(false);
        DailyMap.gameObject.SetActive(false);
        BestMap.gameObject.SetActive(false);
        pl.erase.Play();
    }

    public void CameTO(string name)
    {
        StopAllCoroutines();
        StartCoroutine(ComeTo(name));
    }
    IEnumerator ComeTo(string code)
    {
        disbaleEver();
        loadingMap.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        switch (code)
        {
            case "Start":
                menuMap.gameObject.SetActive(true);
                break;
            case "Opti":
                settingsMap.gameObject.SetActive(true);
                break;
            case "Tuti":
                InfoMap.gameObject.SetActive(true);
                break;
            case "Lead":
                BestMap.gameObject.SetActive(true);
                break;
            case "Game":
                playMap.gameObject.SetActive(true);
                playObjects.gameObject.SetActive(true);
                break;
         
            default:
                break;
        }
        loadingMap.gameObject.SetActive(false);
    }
}
