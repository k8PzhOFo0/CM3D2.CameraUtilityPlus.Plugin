using UnityEngine;
using UnityInjector;
using UnityInjector.Attributes;

namespace CM3D2.CameraUtilityPlus.Plugin
{
    [PluginFilter("CM3D2x64"),
    PluginFilter("CM3D2x86"),
    PluginFilter("CM3D2VRx64"),
    PluginName("Camera Utility Plus"),
    PluginVersion("1.0.0.0")]
    public class CameraUtilityPlus : PluginBase
    {
        private bool lightControl = true;
        private bool bgRemover = true;

        private GameObject bg;
        private int bgIndex = 0;
        private Color[] color = new Color[] { Color.black, Color.blue, Color.green, Color.red, Color.white };

        private float lightIntensityDefault;

        CameraMain mainCamera;
        LightMain mainLight;
        private void Awake()
        {
            GameObject.DontDestroyOnLoad(this);
            bg = GameObject.Find("__GameMain__/BG");
        }

        private void OnLevelWasLoaded(int level)
        {
            if (!bg.activeSelf)
            {
                bgIndex = 0;
                bg.SetActive(true);
            }
            mainCamera = GameMain.Instance.MainCamera;
            mainLight = GameMain.Instance.MainLight;

            lightIntensityDefault = mainLight.light.intensity;
        }

        private void Update()
        {
            if (bgRemover)
            {
                if (Input.GetKeyDown(KeyCode.B))
                {
                    if (bgIndex >= color.Length)
                    {
                        bgIndex = 0;
                        bg.SetActive(true);
                    }
                    else
                    {
                        if (bg.activeSelf)
                        {
                            bg.SetActive(false);
                        }
                        mainCamera.camera.backgroundColor = color[bgIndex];
                        bgIndex++;
                    }
                }
            }

            if (lightControl)
            {
                if (Input.GetKeyDown(KeyCode.Keypad5))
                {
                    mainLight.Reset();
                }
                if (Input.GetKey(KeyCode.Keypad6))
                {
                    mainLight.transform.eulerAngles += Vector3.up;
                }
                if (Input.GetKey(KeyCode.Keypad4))
                {
                    mainLight.transform.eulerAngles -= Vector3.up;
                }

                if (Input.GetKey(KeyCode.Keypad2))
                {
                    mainLight.transform.Rotate(-Vector3.right);
                }
                if (Input.GetKey(KeyCode.Keypad8))
                {
                    mainLight.transform.Rotate(Vector3.right);
                }

                if (Input.GetKey(KeyCode.KeypadPlus))
                {
                    mainLight.light.intensity += 0.1f * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.KeypadMinus))
                {
                    mainLight.light.intensity -= 0.1f * Time.deltaTime;
                }
                if (Input.GetKeyDown(KeyCode.KeypadMultiply))
                {
                    mainLight.light.intensity = lightIntensityDefault;
                }
            }
        }
    }
}