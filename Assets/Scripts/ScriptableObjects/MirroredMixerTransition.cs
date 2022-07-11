using Animancer;
using System;
using System.Collections.Generic;

namespace Rosen
{
    public class MirroredMixerTransition : MixerTransition<MirroredMixerState, bool>
    {
        public override MirroredMixerState CreateState()
        {
            throw new NotImplementedException();
        }
    }

    public class MirroredMixerState : MixerState<bool>
    {
        public override float Length => mClip.Clip.length;

        private MirroredClip mClip;

        public MirroredMixerState(MirroredClip clip)
        {
            mClip = clip;
        }


        public override string GetParameterError(bool parameter)
        {
            throw new NotImplementedException();
        }
    }

    class Test
    {
        void Yeet()
        {
        }
    }
}
