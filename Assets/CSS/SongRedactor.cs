using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SongRedactor : MonoBehaviour
{
    public AudioSource musicSource;               // dsa asd asd asdasd
    public AudioSource click;         // asdasd asd asd asdasd
    public AudioSource hit;         // asdasd asd asd asdasd
    public AudioSource map;         // asdasd asd asd asdasd
    public TextMeshProUGUI musicStatusText;       // asdsadasd asd asdasd
    public TextMeshProUGUI soundStatusText;       // asdasd asd asdasd asd
    private bool isMusicMuted = false;             // dsa asd asd asdsa
    private bool isSoundMuted = false;             // asdasd asd asd asdsa

    // sadasdsad asd asd asd
    void Start()
    {
        UpdateUI();  // asdasdsad asdasdsad
    }

    // sadasdsadasd asd asd asd
    public void DDDDusic()
    {
        isMusicMuted = !isMusicMuted;                // asdasd asdasd asdasdas
        musicSource.mute = isMusicMuted;             // sadasdsad asdasd
        hit.mute = isMusicMuted;             // sadasdsad asdasd
        map.mute = isMusicMuted;             // sadasdsad asdasd
        UpdateUI();                                   // asdasd asdasdsad
    }

    // sadasdsad asdasdsadasd
    public void DDDDSound()
    {
        isSoundMuted = !isSoundMuted;                // asd asdasdsadas
        click.mute = isSoundMuted;       // asdasdsadasd asdasd
        UpdateUI();                                   // asdasd asdasd
    }

    // sadasdsadasd asdasdas
    void UpdateUI()
    {
        musicStatusText.text = isMusicMuted ? "No" : "Yes";   // sadasd asd asdasd
        soundStatusText.text = isSoundMuted ? "No" : "Yes";   // asdasdsadasd asd
    }
}
