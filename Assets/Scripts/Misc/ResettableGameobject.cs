using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// interface for objects which should be reset, when the player dies (e.g. already killed enemies)
public interface ResettableGameobject
{
    void Reset();
}
