using UnityEngine;
using System.Collections;
using FMODUnity;

namespace VRStep
{

	public class PlaySoundOnStep : MonoBehaviour
	{

		public AudioSource sound;
        public StudioEventEmitter soundEmitter;

		// Use this for initialization
		void Start()
		{
			StepDetector.AddStepAction(OnStep);
		}

		void OnStep()
		{
            if (this.soundEmitter != null)
                this.soundEmitter.Play(); ;
			//sound.Play();
		}
	}
}
