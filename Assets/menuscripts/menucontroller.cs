using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Toggle soundToggle;
    public Slider volumeSlider;
    public Dropdown resolutionDropdown;

    void Start()
    {
        // Initialize values
        soundToggle.isOn = true;
        volumeSlider.value = 0.5f;

        // Populate dropdown options
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(new System.Collections.Generic.List<string> {
            "800x600", "1280x720", "1920x1080"
        });

        // Add listeners
        soundToggle.onValueChanged.AddListener(delegate { OnToggleChanged(); });
        volumeSlider.onValueChanged.AddListener(delegate { OnSliderChanged(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnDropdownChanged(); });
    }

    void OnToggleChanged()
    {
        Debug.Log("Sound toggled: " + soundToggle.isOn);
    }

    void OnSliderChanged()
    {
        Debug.Log("Volume: " + volumeSlider.value);
    }

    void OnDropdownChanged()
    {
        Debug.Log("Resolution: " + resolutionDropdown.options[resolutionDropdown.value].text);
    }
}
