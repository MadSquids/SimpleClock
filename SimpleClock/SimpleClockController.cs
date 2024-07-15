using System;
using System.Collections;
using UnityEngine;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.FloatingScreen;
using TMPro;
using Zenject;

namespace SimpleClock
{
    public class SimpleClockController : IInitializable, IDisposable
    {
        private FloatingScreen _screen;
        private TextMeshProUGUI _clockText;
        private Coroutine _clockUpdater;

        public void Initialize()
        {
            MakeClock();
            StartClockUpdater();
        }

        public void Dispose()
        {
            if (_clockUpdater != null)
            {
                //Debug.Log("Stopping clock updater");
                StopClockUpdater();
                _clockUpdater = null;
            }

            if (_screen != null)
            {
                UnityEngine.Object.Destroy(_screen.gameObject);
                _screen = null;
                _clockText = null;
            }
        }

        //Makes the clock.
        private void MakeClock()
        {
            //Error catching
            try
            {
                //Create floating screen
                _screen = FloatingScreen.CreateFloatingScreen(
                    new Vector2(30f, 15f),       //screen size
                    false,                       //create handle
                    new Vector3(0f, 2.75f, 3f),  //position
                    Quaternion.identity,         //rotation
                    0f,                          //curvaturRadius
                    false                        //hasBackground
                    );

                //Check if screen was made. else throw error.
                if (_screen != null)
                {
                    //Create text
                    _clockText = BeatSaberUI.CreateText(
                        _screen.gameObject.GetComponent<RectTransform>(),
                        GetCurrentTime(),
                        new Vector2(0, 0)
                    );

                    //Check if text was made. else throw error.
                    if (_clockText != null)
                    {
                        //Debug.Log("clockText created successfully.");

                        //Configure Text
                        _clockText.alignment = TextAlignmentOptions.Center;
                        _clockText.fontSize = 8f;
                        _clockText.color = Color.white;
                    }
                    else
                    {
                        Debug.LogError("Failed to create clockText.");
                    }
                }
                else
                {
                    Debug.LogError("Failed to create screen.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception while creating clock: {e}");
            }
            //Debug.Log("SimpleClockController initialized.");
        }

        //Starts the coroutine to keep the clock updated.
        private void StartClockUpdater()
        {
            _clockUpdater = CoroutineRunner.Instance.StartCoroutine(UpdateTime());
        }

        //Stops the coroutine updating the clock.
        private void StopClockUpdater()
        {
            CoroutineRunner.StopRoutine(_clockUpdater);
        }

        //Returns current time as a String in hours:minutes AM/PM format.
        private string GetCurrentTime()
        {
            return DateTime.Now.ToString("hh:mm tt");
        }

        //Updates the clockText to the current time every second.
        private IEnumerator UpdateTime()
        {
            while (true)
            {
                if (_clockText != null)
                {
                    //Debug.Log("Checking Time.");
                    _clockText.text = GetCurrentTime();
                    yield return new WaitForSeconds(1);
                }
                else
                {
                    Debug.LogError("clockText is null.");
                    yield break;
                }
            }
        }
    }
}
