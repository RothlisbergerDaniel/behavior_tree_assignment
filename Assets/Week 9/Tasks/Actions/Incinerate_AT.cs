using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class Incinerate_AT : ActionTask {

		private GameObject target;
		private float incinerateTime; //max time to incinerate
		private float timer; //timer to check how long incineration has been going

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
            incinerateTime = blackboard.GetVariableValue<float>("incinerateTime");
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			timer = 0;
            target = blackboard.GetVariableValue<GameObject>("target");
            //EndAction(true);
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			timer += Time.deltaTime;
			if (timer >= incinerateTime)
			{
				target.GetComponent<DestroyGarbage>().destroyGarbage();
				EndAction(true); //failing to end action here will result in a NullReferenceException!
			}
		}

	}
}