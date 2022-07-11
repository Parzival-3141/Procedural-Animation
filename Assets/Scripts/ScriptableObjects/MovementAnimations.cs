using Animancer;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rosen
{
    [CreateAssetMenu(menuName = nameof(MovementAnimations))]
    public class MovementAnimations : ScriptableObject
    {
        public LinearMixerTransition Transition => mixer;
        public LinearMixerState State => mixer.State;
        
        [SerializeField] private LinearMixerTransition mixer;

        [Space]

        //public AnimationClip idle;
        //public MirroredClip runReach, runPass;
        //public MirroredClip walkReach, walkPass;

        public float runStrideLength = 2.1f;
        public float walkStrideLength = 0.7f;

        //public void InitMixer()
        //{
        //    mixer = new LinearMixerState();
        //    mixer.Initialize(3);

        //    mixer.CreateChild(0, idle, 0f);

        //    var walkState = new LinearMixerState();
        //    walkState.Initialize(new AnimationClip[] { walkPass.Clip, walkReach.Clip }, new float[] { 0f, 1f });
        //    mixer.SetChild(1, walkState, 0.1f);

        //    var runState = new LinearMixerState();
        //    runState.Initialize(new AnimationClip[] { runPass.Clip, runReach.Clip }, new float[] { 0f, 1f });
        //    mixer.SetChild(2, runState, 1f);

        //    Debug.Log("Init mixer.  " + mixer.ChildStates[1]);

        //}

        //@Refactor: SURELY there's a better way to propagate nested mixer values... SURELY.
        public void SetMirror(bool value)
        {
            (State.ChildStates[1] as LinearMixerState).Parameter = Bool2Float(value);
            (State.ChildStates[2] as LinearMixerState).Parameter = Bool2Float(value);
        }

        public void SetPassReach(bool reach)
        {
            (State.ChildStates[1].GetChild(0) as LinearMixerState).Parameter = Bool2Float(reach);
            (State.ChildStates[1].GetChild(1) as LinearMixerState).Parameter = Bool2Float(reach);

            (State.ChildStates[2].GetChild(0) as LinearMixerState).Parameter = Bool2Float(reach);
            (State.ChildStates[2].GetChild(1) as LinearMixerState).Parameter = Bool2Float(reach);

        }

        private float Bool2Float(bool b) => b ? 1f : 0f;
    }
}
