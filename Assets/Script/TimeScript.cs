using UnityEngine;

public class TimeScript : MonoBehaviour
{
    public GameObject SecondsHandler;
    public GameObject MinutesHandler;
    public GameObject HoursHandler;

    public int minutes = 0;
    public int hour = 0;
    public int seconds = 0;
    //private bool realTime = true;
    public float clockSpeed = 1.0f;
    private float temp = 0;
    public bool smoothClock = false;
    public bool HourHandEnabled = true;
    public bool MinuteHandEnabled = true;
    public bool SecondHandEnabled = true;
    void Start()
    {
          hour = System.DateTime.Now.Hour;
          minutes = System.DateTime.Now.Minute;
          seconds = System.DateTime.Now.Second;
    }

    void Update()
    {
        
        temp += Time.deltaTime * clockSpeed;
        if (temp >= 1.0f)
        {
            temp -= 1.0f;
            seconds++;
            if (seconds >= 60)
            {
                seconds = 0;
                minutes++;
                if (minutes >= 60)
                {
                    minutes = 0;
                    hour++;
                    if (hour >= 24)
                        hour = 0;
                }
            }
        }

        //-- calculate angles
        float rotationSeconds = (360.0f / 60.0f) * seconds;
        float rotationMinutes = (360.0f / 60.0f) * minutes;
        float rotationHours = ((360.0f / 12.0f) * hour) + ((360.0f / (60.0f * 12.0f)) * minutes);

        if (!smoothClock)
        {
            RotateAround(SecondsHandler.transform, -Vector3.forward, rotationSeconds);
            RotateAround(MinutesHandler.transform, -Vector3.forward, rotationMinutes);
            RotateAround(HoursHandler.transform, -Vector3.forward, rotationHours);
        }
        else if (smoothClock)
        {
            RotateClockHand(SecondsHandler, -rotationSeconds);
            RotateClockHand(MinutesHandler, -rotationMinutes);
            RotateClockHand(HoursHandler, -rotationHours);
        }
    }


    void RotateAround(Transform target, Vector3 axis, float angle)
    {
        target.rotation = Quaternion.AngleAxis(angle, axis);
    }
    void RotateClockHand(GameObject hand, float targetRotation)
    {
        Quaternion currentRotation = hand.transform.localRotation;
        Quaternion targetRotationQuat = Quaternion.Euler(0.0f, 0.0f, targetRotation);
        hand.transform.localRotation = Quaternion.Lerp(currentRotation, targetRotationQuat, Time.deltaTime * 1.0f); 
    }
}

