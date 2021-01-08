using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundingAwareSprite : MonoBehaviour
{
    
    public int numberOfStates;
    public List<SpriteRenderer> renderers;
    public List<Sprite> sprites;

    private List<int> states;
    

    void Awake()
    {
        states = new List<int>();

        for (int i = 0; i < 4; i++)
        {
            states.Add(0);
        }
    }

    public void SetState(int idx, int state)
    {
        Debug.Assert(idx >= 0 && idx < 4, "idx must be in [0, 4), with 0 representing up, going clockwise.");
        Debug.Assert(state < numberOfStates, "state must be lesser than the number of possible states.");

        states[idx] = state;
    }

    private int getSpriteIdx()
    {
        int spriteIdx = 0;

        for (int pow = 0; pow < 4; pow++)
        {
            spriteIdx += states[pow] * (int)Mathf.Pow(numberOfStates, pow);
        }

        return spriteIdx;
    }

    public void UpdateSprite()
    {
        Sprite currentSprite = sprites[getSpriteIdx()];

        foreach (SpriteRenderer rend in renderers)
        {
            rend.sprite = currentSprite;
        }
    }

}
