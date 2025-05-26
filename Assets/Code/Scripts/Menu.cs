using UnityEngine;
using TMPro; // For TextMeshPro support


public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] Animator anim;


    private void OnGUI()
    {
        currencyUI.text = LevelManager.main.currency.ToString();
    }

    //public void SetSeleted
    //{

    //}

    private bool isMenuOpen = true;

    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }






}
