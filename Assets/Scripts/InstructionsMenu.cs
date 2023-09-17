using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsMenu : MonoBehaviour
{
    [SerializeField] GameObject instructionsMenu;

    public void ToggleInstructionsMenu(bool isActive)
    {
        instructionsMenu.SetActive(isActive);
    }
}
