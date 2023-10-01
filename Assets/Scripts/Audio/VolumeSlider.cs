using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    void Start()
    {
        slider.value = AudioManager.Volume;
        slider.value = SFXManager.Volume;
    }

    void Update()
    {
        SetVolume();
    }

    private void SetVolume()
    {
        AudioManager.Volume = slider.value;
        SFXManager.Volume = slider.value;
    }
}
