using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    [SerializeField] private Transform destination;
    private TransformInfo from;
    private TransformInfo to;
    private bool _open = false;
    private bool _moving = false;
    private float percentage = 1f;
    private float _timeStartedLerping;
    public float lerpingTime = 1.0f; //seconds

    private TransformInfo closedState;
    private TransformInfo openState;

    public void Awake() {
        closedState = new TransformInfo(this.transform);
        openState = new TransformInfo(destination);
    }

    public void Operate() {
        print("operating door");
        if(_open) {
            from = openState;
            to = closedState;
        } else {
            from = closedState;
            to = openState;
        }
        percentage = 0f;
        _moving = true;
        _open = !_open;
        _timeStartedLerping = Time.time;
    }

    void Update() {
        if(_moving) {
            float timeSinceStarted = Time.time - _timeStartedLerping;
            float percentage = timeSinceStarted/lerpingTime;
            print("percentage: " + percentage);
            print("From: " + from.position.ToString());
            print("To: " + to.position.ToString());
            transform.LerpTransform(from, to, percentage);

            if(percentage >= 1.0f) {
                _moving = false;
            }
        } 
    }    
}

public static class Helper {
    public static void LerpTransform(this Transform t, TransformInfo t1, TransformInfo t2, float delta) {
        t.position = Vector3.Lerp(t1.position, t2.position, delta);
        t.rotation = Quaternion.Lerp(t1.rotation, t2.rotation, delta);
        t.localScale = Vector3.Lerp(t1.localScale, t2.localScale, delta);
    }
}

public class TransformInfo {
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 localScale;

    public TransformInfo(Transform t) {
        position = t.position;
        rotation = t.rotation;
        localScale = t.localScale;
    }
}
