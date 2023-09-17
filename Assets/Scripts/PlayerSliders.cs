using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSliders : MonoBehaviour
{
    [SerializeField] int minLevel = 0;
    [SerializeField] int maxLevel = 6;

    [SerializeField] Slider positionSlider;
    [SerializeField] Slider momentumSlider;

    [SerializeField] int positionLevel = 6;
    [SerializeField] int momentumLevel = 0;
    public int PositionLevel => positionLevel;
    public int MomentumLevel => momentumLevel;

    [SerializeField] JerryManager jerryManager;
    
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown(KeyCode.LeftArrow))
        // {
        //     positionLevel = Mathf.Clamp(positionLevel - 1, minLevel, maxLevel);
        // }
        // if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.RightArrow))
        // {
        //     positionLevel = Mathf.Clamp(positionLevel + 1, minLevel, maxLevel);
        // }
        // momentumLevel = (maxLevel - minLevel) - positionLevel;

        // positionSlider.value = (float)positionLevel / maxLevel;
        // momentumSlider.value = (float)momentumLevel / maxLevel;
        positionSlider.value = (float)(maxLevel - jerryManager.JerryCountLevelMinusOne) / maxLevel;
        momentumSlider.value = (float)jerryManager.JerryCountLevelMinusOne / maxLevel;
    }
}
