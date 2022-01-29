using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Dropdown resolutionDropdown;
    [SerializeField] Toggle fullScreenToggle;

    int[] resolutionsX = new int[] { 1920, 1366, 1280, 960 };

    // Start is called before the first frame update
    void Start()
    {
        fullScreenToggle.isOn = Screen.fullScreen;
        for (int i = 0; i < resolutionsX.Length; i++)
        {
            if (Screen.width == resolutionsX[i])
            {
                resolutionDropdown.value = i;
                break;
            }
        }
    }
}
