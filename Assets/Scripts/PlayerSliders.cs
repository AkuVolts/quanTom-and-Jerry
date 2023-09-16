using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSliders : MonoBehaviour
{
    [SerializeField] int minLevel = 0;
    [SerializeField] int maxLevel = 8;

    [SerializeField] Slider positionSlider;
    [SerializeField] Slider momentumSlider;

    [SerializeField] int positionLevel = 0;
    [SerializeField] int momentumLevel = 8;
    public int PositionLevel => positionLevel;
    public int MomentumLevel => momentumLevel;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            positionLevel = Mathf.Clamp(positionLevel - 1, minLevel, maxLevel);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            positionLevel = Mathf.Clamp(positionLevel + 1, minLevel, maxLevel);
        }
        momentumLevel = (maxLevel - minLevel) - positionLevel;

        positionSlider.value = (float)positionLevel / maxLevel;
        momentumSlider.value = (float)momentumLevel / maxLevel;
    }
}
