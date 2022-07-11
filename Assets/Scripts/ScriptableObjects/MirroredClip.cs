using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = nameof(MirroredClip))]
public class MirroredClip : ScriptableObject
{
    public AnimationClip Clip => GetClip(isMirrored);
    
    [SerializeField] private AnimationClip normalClip, mirroredClip;
    [NonSerialized] public bool isMirrored = false;

    public AnimationClip GetClip(bool mirrored) => mirrored ? mirroredClip : normalClip;

    public static implicit operator AnimationClip(MirroredClip c) => c.Clip;
}
