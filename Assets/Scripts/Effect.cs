using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect<TValue>
{
    
    public Func<TValue, TValue> Mutator { get;}
    public int Priority { get; }

    public Effect(Func<TValue, TValue> mutator, int priority)
    {
        Mutator = mutator;
        Priority = priority;
    }

}
