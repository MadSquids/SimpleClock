using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


namespace SimpleClock
{
    public class SimpleClockController : MonoBehaviour
    {
        private Text clockText;
        private Font clockFont = Resources.Load<Font>("Arial");
        private GameObject canvasGameObject;

        private void Start()
        {
            MakeClock();
            Debug.Log("Font: " + clockFont);
        }

        private void MakeClock()
        {
            DontDestroyOnLoad(this.gameObject);

            //Create a new Canvas
            canvasGameObject = new GameObject("SimpleClockCanvas");
            var canvas = canvasGameObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            Debug.Log(canvasGameObject);

            //Create a new Text Object
            var textGameObject = new GameObject("SimpleClockText");
            textGameObject.transform.SetParent(canvasGameObject.transform, false);

            // Add Text Component
            clockText = textGameObject.AddComponent<Text>();
            clockText.font = clockFont;
            Debug.Log("Parent: " + clockText.transform.parent.gameObject);
            Debug.Log(clockText);
            Debug.Log(clockText.font);
            clockText.fontSize = 36;
            clockText.alignment = TextAnchor.UpperCenter;

            // Set the RectTransform of the Text object
            var rectTransform = clockText.rectTransform;
            rectTransform.localPosition = new Vector3(0, -50, 0);
            rectTransform.sizeDelta = new Vector2(400, 100);

            Debug.Log("SimpleClockController initialized.");
        }

        private void Update()
        {
            if (clockText != null)
            {
                clockText.text = DateTime.Now.ToString("hh:mm tt");
            }
            else
            {
                Debug.LogWarning("clockText is null.");
            }
            
        }

        private void OnDestroy()
        {
            if (canvasGameObject != null)
            {
                Destroy(canvasGameObject);
                Debug.Log("SimpleClockCanvas destroyed.");
            }
        }
    }
}
