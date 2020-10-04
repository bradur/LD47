using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField]
    bool StrikingRequested;

    [SerializeField]
    bool Walking;

    [SerializeField]
    bool Striking;

    Animator anim;

    private float strikeTimer;
    private float STRIKE_GRACE_PERIOD = 0.1f;

    List<AnimationListener> listeners = new List<AnimationListener>();

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void RegisterListener(AnimationListener listener)
    {
        listeners.Add(listener);
    }

    // Update is called once per frame
    void Update()
    {
        if (strikeTimer < Time.time)
        {
            StrikingRequested = false;
        }

        if (StrikingRequested)
        {
            if (!Striking)
            {
                Striking = true;
                StrikingRequested = false;
            }
        }

        anim.SetBool("Walking", Walking);
        anim.SetBool("Striking", Striking);

    }
    
    public void Walk()
    {
        Walking = true;
    }
    
    public void Stop()
    {
        Walking = false;
    }
    
    public void Strike()
    {
        StrikingRequested = true;
        strikeTimer = Time.time + STRIKE_GRACE_PERIOD;
    }

    public void StrikeDone()
    {
        Striking = false;
        foreach (var listener in listeners)
        {
            listener.StrikeDone();
        }
    }
}
