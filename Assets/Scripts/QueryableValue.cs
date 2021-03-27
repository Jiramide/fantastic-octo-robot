using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QueryableValue<TValue>
{

    private Func<TValue> supplier;
    private List<Effect<TValue>> effectQueue;

    public QueryableValue(Func<TValue> supplier)
    {
        this.supplier = supplier;
        this.effectQueue = new List<Effect<TValue>>(32);
    }

    public TValue GetValue()
    {
        var currentValue = supplier();

        return effectQueue.Aggregate(
            currentValue,
            (acc, effect) => effect.Mutator(acc)
        );
    }

    public void AddEffect(Effect<TValue> effectToInsert)
    {
        // implement a binary search to find proper index to insert into.
        // lower priority => goes first

        var index = effectQueue.BinarySearch(
            effectToInsert,
            Comparer<Effect<TValue>>.Create((x, y) => x.Priority - y.Priority)    
        );

        effectQueue.Insert(index, effectToInsert);
    }

    public void RemoveEffect(Effect<TValue> effectToRemove)
    {
        effectQueue.Remove(effectToRemove);
    }

}
