using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI waveUI;
    [SerializeField] TextMeshProUGUI healthUI;
    [SerializeField] Animator anim;
    Spawner spawner = new Spawner();

    private bool isMenuOpen = true;
    private void OnGUI()
    {
        //Update currency amount:
        currencyUI.text = GameManager.main.currency.ToString();
    }

    public void ToggleMenu()
    {
        //Toggle Shop
        isMenuOpen = !isMenuOpen;
        //Animate Shop
        anim.SetBool("Menu Open", isMenuOpen);
    }
    public void SetSelected()
    {

    }

    private void Update()
    {
        //Update realtime Waves and Health
        waveUI.text = Spawner.main.currentWave.ToString();
        healthUI.text = GameManager.main.health.ToString();
    }
}
