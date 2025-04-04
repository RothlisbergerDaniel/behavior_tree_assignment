using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class Close_CT : ConditionTask {

		public BBParameter<GameObject> target;
		public BBParameter<float> range;
		public BBParameter<float> chargeCooldownTimer;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			
			return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
			if (chargeCooldownTimer.value > 0) { chargeCooldownTimer.SetValue(chargeCooldownTimer.value - Time.deltaTime); }

			if (Vector3.Distance(agent.transform.position, target.value.transform.position) <= range.value && chargeCooldownTimer.value <= 0)
			{ return  true; } else { return false; }
		}
	}
}