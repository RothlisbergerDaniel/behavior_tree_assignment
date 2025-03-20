using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class CheckGarbageNearby_CT : ConditionTask {

		private GameObject target;
		private float radius;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit(){
			radius = blackboard.GetVariableValue<float>("incinerateRadius");
            return null;
		}

		//Called whenever the condition gets enabled.
		//protected override void OnEnable() {
		//	
		//}

		//Called whenever the condition gets disabled.
		//protected override void OnDisable() {
		//	
		//}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
            target = blackboard.GetVariableValue<GameObject>("target"); //this will equal null if no target is set!
            if (target == null) { return false; } //return false if no target is set - we need to search for a new one.

			Collider[] hits = Physics.OverlapSphere(agent.transform.position, radius); //get collisions within the incineration radius of the robot
			if (hits.Length > 1) // make sure the length is greater than 1 to make sure we don't count the robot itself
			{
				foreach (Collider col in hits) //iterate through the list since we don't know the order
				{
                    if (col.gameObject == target) { return true; } //only return true if the target garbage is within range
                }
				
			}

			return false; //return false as a catch-all
		}
	}
}