using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class Close_CT : ConditionTask {

		public BBParameter<GameObject> target;
		public BBParameter<float> range;
		public BBParameter<float> reload;
		public bool moveWhenReloading;

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

			if (reload.value > 0) { reload.SetValue(reload.value - Time.deltaTime); } //reduce charge cooldown

			if (Vector3.Distance(agent.transform.position, target.value.transform.position) <= range.value && 
			   ((reload.value <= 0 && !moveWhenReloading) || (moveWhenReloading)))	//if close to player and charge
																					//cooldown is up, return true
			{ return  true; } else { return false; }
		}
	}
}