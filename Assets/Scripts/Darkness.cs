using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darkness : MonoBehaviour
{

     [SerializeField] GameObject[] officeLights;
     [SerializeField] GameObject[] darkLights;
     [SerializeField] Material skyboxMat;


    public void turnLights(bool lightSwitch)
    {
        Color newBackgroundColor = new Color(0.192f, 0.302f, 0.475f);
        Color newSkyColor = new Color32(0xA0, 0xA7, 0xB7, 0xFF);

        if (!lightSwitch)
        {
            RenderSettings.skybox = null;
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Skybox;
            RenderSettings.ambientLight = Color.black;
            Camera.main.clearFlags = CameraClearFlags.SolidColor;
            Camera.main.backgroundColor = Color.black;
        }

        if(lightSwitch)
        {

            // Revert the changes
            RenderSettings.skybox = skyboxMat;
            RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
            RenderSettings.ambientLight = newSkyColor;
            Camera.main.clearFlags = CameraClearFlags.Skybox;
            Camera.main.backgroundColor = newBackgroundColor;
        }

        foreach (var light in officeLights)
        {
            light.SetActive(lightSwitch);
        }

        foreach (var darkLight in darkLights)
        {
            darkLight.SetActive(!lightSwitch);
        }
    }
}
