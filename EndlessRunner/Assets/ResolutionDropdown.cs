using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionDropdown : MonoBehaviour
{
    Dropdown dropdown;

    public static ResolutionDropdown instance;

    private void Awake()
    {
        instance = this;
    }

    public void ChangeValue(int value)
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.value = value;
    }
}
