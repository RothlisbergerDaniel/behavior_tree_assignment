using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class Travel_AT : ActionTask {

        private GameObject target;
		private float speed;
		private Vector3 vel = new Vector3();

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            speed = blackboard.GetVariableValue<float>("moveSpeed");
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            target = blackboard.GetVariableValue<GameObject>("target");
            //EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			vel = Vector3.Normalize(new Vector3(target.transform.position.x, 0, target.transform.position.z) - new Vector3(agent.transform.position.x, 0, agent.transform.position.z));
			vel *= speed * Time.deltaTime; //eliminate y values so the robot doesn't move upwards, normalize, and multiply by speed to get simple movement.
			agent.transform.Translate(vel); //move robot towards garbage
			EndAction(true); //end action so it doesn't go on forever!
		}

		//Called when the task is disabled.
		//protected override void OnStop() {
		//	
		//}

		//Called when the task is paused.
		//protected override void OnPause() {
		//	
		//}
	}
}