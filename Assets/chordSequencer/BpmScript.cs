using UnityEngine;

// The code example shows how to implement a metronome that procedurally
// generates the click sounds via the OnAudioFilterRead callback.
// While the game is paused or suspended, this time will not be updated and sounds
// playing will be paused. Therefore developers of music scheduling routines do not have
// to do any rescheduling after the app is unpaused

[RequireComponent(typeof(AudioSource))]
public class BpmScript : MonoBehaviour
{
    private static BpmScript bpmInstance;
    public float _bpm;
    private double _beatInterval, _beatTimer, _beatIntervalD8, _beatTimerD8;
    public static bool _beatFull, _beatFullD8;
    public static int _beatCountFull, _beatCountFullD8;

    void Update(){
        BeatDetection();
    }

    void BeatDetection(){
        _beatFull = false;
        _beatInterval = 60/_bpm;
        _beatTimer += Time.deltaTime;
        if(_beatTimer >= _beatInterval){
            _beatTimer -= _beatInterval;
            _beatFull = true;
            _beatCountFull++;
        }

        _beatFullD8 = false;
        _beatIntervalD8 = _beatInterval/8;
        _beatTimerD8 += Time.deltaTime;
        if(_beatTimerD8 >= _beatIntervalD8){
            _beatTimerD8 -= _beatIntervalD8;
            _beatFullD8 = true;
            _beatCountFullD8++;
        }
    }

}
