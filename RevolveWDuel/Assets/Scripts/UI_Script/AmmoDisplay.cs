using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AmmoDisplay : MonoBehaviour
{
    public Text ammoDisplay;

    // Method to update ammo count on the UI
    public void UpdateAmmoDisplay(int currentAmmo)
    {
        ammoDisplay.text = currentAmmo.ToString();
    }
}
