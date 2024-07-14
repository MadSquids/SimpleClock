using System;
using System.Collections;
using UnityEngine;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using TMPro;


namespace SimpleClock
{
    public class SimpleClockController : MonoBehaviour
    {
        private FloatingScreen screen;
        private TextMeshProUGUI clockText;

        private void Start()
        {
            MakeClock();
        }

        //Updates the time of the clock every second.
        private IEnumerator UpdateTime()
        {
            while (true)
            {
                if (clockText != null)
                {
                    //Debug.Log("Checking Time.");

                    clockText.text = GetCurrentTime();
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    Debug.LogError("clockText is null.");
                    ClockBroken();      //Clock is broken calling broken function and destroying the clock.
                    yield break;
                }
            }
        }

        //Makes the clock.
        private void MakeClock()
        {
            DontDestroyOnLoad(gameObject);

            //Error catching
            try
            {
                //Create floating screen
                screen = FloatingScreen.CreateFloatingScreen(
                    new Vector2(30f, 15f),     //screen size
                    false,                      //create handle
                    new Vector3(0f, 2.75f, 3f),    //position
                    Quaternion.identity,        //rotation
                    0f,                         //curvaturRadius
                    false                       //hasBackground
                    );

                //Check if screen was made. else throw error.
                if (screen != null)
                {
                    DontDestroyOnLoad(screen);

                    //Create text
                    clockText = BeatSaberUI.CreateText(
                        screen.gameObject.GetComponent<RectTransform>(),
                        GetCurrentTime(),
                        new Vector2(0, 0)
                    );

                    //Check if text was made. else throw error.
                    if (clockText != null)
                    {
                        //Debug.Log("clockText created successfully.");

                        //Configure Text
                        clockText.alignment = TextAlignmentOptions.Center;
                        clockText.fontSize = 8f;
                        clockText.color = Color.white;

                        //Start checking for the time every second.
                        StartCoroutine(UpdateTime());
                    }
                    else
                    {
                        Debug.LogError("Failed to create clockText.");
                        ClockBroken();      //Clock is broken calling broken function and destroying the clock.
                    }
                }
                else
                {
                    Debug.LogError("Failed to create screen.");
                    ClockBroken();      //Clock is broken calling broken function and destroying the clock.
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception while creating clock: {e}");
                ClockBroken();      //Clock is broken calling broken function and destroying the clock.
            }

            //Debug.Log("SimpleClockController initialized.");
        }

        //Returns current time as a String in hours:minutes AM/PM format.
        private string GetCurrentTime()
        {
            return DateTime.Now.ToString("hh:mm tt");
        }

        private void ClockBroken()
        {
            if (screen != null)
            {
                Destroy(screen);
            }

            //Error message for the logs so people know what to do to get the plugin fixed.
            Debug.LogError("The clock is broken. :(\nStart an issue on the GitHub for SimpleClock, post your _latest.log, and I will likely fix it. :)");
        }

        private void OnDestroy()
        {
            if (screen != null)
            {
                Destroy(screen);
                Debug.Log("SimpleClockScreen destroyed.");
            }
        }
    }
}
