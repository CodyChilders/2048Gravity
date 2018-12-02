using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public AudioClip hitNoMerge;
    public AudioClip hitMerge;
    public AudioClip hitNonBall;
    public AudioClip destroyed;
    public AudioClip gotTarget; //special case of merging

    public float deathPlane = -10;

    GUIStyle gs;

    public int CurrentValue
    {
        get
        {
            return currentValue;
        }
        set
        {
            currentValue = value;
        }
    }

    int currentValue;
    Camera cam = null;
    AudioSource audioSource;

    void Start()
    {
        currentValue = GameState.GetGameController().GetInitialValue();
        SetMaterial();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        Debug.Assert(cam != null);
        audioSource = GetComponent<AudioSource>();
        Debug.Assert(audioSource != null);
        gs = new GUIStyle() { fontSize = 30 };
    }

    void Update()
    {
        if (transform.position.y < deathPlane)
        {
            //PlaySound(destroyed);
            GameState.GetGameController().BallDestroyed(gameObject, currentValue);
            Destroy(gameObject);
        }
    }

    void PlaySound(AudioClip ac)
    {
        if(ac == null)
        {
            throw new ArgumentNullException("Must give an audio clip.");
        }

        if (!audioSource.isPlaying)
        {
            audioSource.clip = ac;
            audioSource.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if it is not a ball, return
        GameObject target = collision.gameObject;
        if (!target.tag.Equals("NumberedBall"))
        {
            //PlaySound(hitNonBall);
            return;
        }

        //if the values are not equal, no need to merge
        int val1 = CurrentValue;
        int val2 = target.GetComponent<BallBehavior>().CurrentValue;
        if(!GameState.GetGameController().ShouldMerge(val1, val2))
        {
            PlaySound(hitNoMerge);
            return;
        }

        //double the value of the lower-altitude ball, and destroy the higher-altitude one
        GameObject highest;
        GameObject lowest;
        if (transform.position.y >= target.transform.position.y)
        {
            highest = gameObject;
            lowest = target;
        }
        else
        {
            highest = target;
            lowest = gameObject;
        }

        
        Destroy(highest);

        BallBehavior lowestBB = lowest.GetComponent<BallBehavior>();
        int newValue = GameState.GetGameController().Merge(val1, val2);
        lowestBB.CurrentValue = newValue;
        lowestBB.SetMaterial();

        if (GameState.GetGameController().IsTargetValue(lowestBB.CurrentValue))
        {  
            PlaySound(gotTarget);
        }
        else
        {
            PlaySound(hitMerge);
        }

        GameState.GetGameController().BallMerged(lowest, lowestBB.CurrentValue);
    }

    private void OnGUI()
    {
        if (GameState.GetGameController().ShouldDisplayValue())
        {
            const int labelWidth = 80;
            const int labelHeight = 20;
            Vector3 screenPosition = cam.WorldToScreenPoint(transform.position);
            screenPosition.y = Screen.height - screenPosition.y;
            Rect rect = new Rect(screenPosition.x, screenPosition.y, labelWidth, labelHeight);
            GUI.contentColor = Color.black;
            GUI.Label(rect, currentValue.ToString(), gs);
        }
    }

    public void SetMaterial()
    {
        Material desiredMaterial = GameState.GetGameController().GetMaterial(currentValue);
        GetComponent<Renderer>().material = desiredMaterial;
    }
}
