using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class IsTargetSet_CT : ConditionTask {

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		private GameObject target;

		protected override string OnInit(){
			
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
			if (target == null) { return true; } //return true if no target is set - i.e. previous target has been destroyed.
            return false; //if a target has already been found, we don't need to search for another.
		}
	}
}