using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Attack
{
    [field: SerializeField] public string AnimationName { get; private set; }
    [field: SerializeField] public float TransitionDuration { get; private set; }
    // how far into the animation we can add the force
    [field: SerializeField] public float ForceTime { get; private set; }
    // apply the force
    [field: SerializeField] public float Force { get; private set; }
    // can we combo 
    [field: SerializeField] public int ComboStateIndex { get; private set; } = -1;
    // how far through an attack will let you do another attack
    [field: SerializeField] public float ComboAttackTime { get; private set; }
    [field: SerializeField] public int Damage { get; private set; }
    [field: SerializeField] public float Knockback { get; private set; }
}
