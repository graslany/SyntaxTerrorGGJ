using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountDownTimer : MonoBehaviour {

    [SerializeField] int _StartTime = 0;
    int _TimeLeft;
    int _TickRate = 1; //1 second
    float _LastTimeTicked;
	// Use this for initialization
	void Start ()
    {
        _TimeLeft = _StartTime;
        _LastTimeTicked = Time.time;
        ChangeDisplay();
    }

    void ChangeDisplay()
    {
        int numberOfMinutes = _TimeLeft / 60;
        int numberOfSeconds = _TimeLeft % 60;
        string displayedTimeLeft = numberOfMinutes.ToString("D2") + " : " + numberOfSeconds.ToString("D2");
        gameObject.GetComponent<UnityEngine.UI.Text>().text = displayedTimeLeft;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float currentTime = Time.time;
        if(currentTime - _LastTimeTicked > _TickRate)
        {
            _LastTimeTicked = currentTime;
            _TimeLeft -= _TickRate;
            ChangeDisplay();
        }
	}
}
