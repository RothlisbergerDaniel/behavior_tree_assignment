using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class Charge_AT : ActionTask {

		public GameObject aimPivot; //object pivot to scale charge indicator correctly
		public BBParameter<float> chargeDelay; //time between preparing to charge and actually charging
		public BBParameter<float> chargeDistance; //how far to charge
		public BBParameter<float> chargeSpeed; //how FAST to charge
        public BBParameter<NavMeshAgent> navmesh; //navmesh
		public BBParameter<GameObject> target; //player to aim at
		public BBParameter<float> chargeCooldown; //max cooldown before the enemy can charge again
		public BBParameter<float> chargeCooldownTimer; //timer to keep track of charge time
        private NavMeshAgent nma;		//
        private float cDelay;			//
		private float cDistance;		// LOCAL versions of the BBParameters for cleaner code
		private float cSpeed;			//
		private float chargeDelayTimer; //
		private float chargeTime; //how long to charge for.
		private VariableContainer vc;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			cDelay = chargeDelay.value;
			cDistance = chargeDistance.value;
			cSpeed = chargeSpeed.value;
            nma = navmesh.value;
			vc = agent.GetComponent<VariableContainer>();
            aimPivot.SetActive(false);
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			chargeDelayTimer = 0; //set to zero so it can start counting up
			chargeTime = cDistance / cSpeed; //set the time that the unit should stay in motion for once the charge activates
			nma.SetDestination(agent.transform.position); //stop its movement in place
			agent.transform.LookAt(target.value.transform.position); //aim at player
			aimPivot.SetActive(true); //enable aim reticle
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
			if (chargeDelayTimer >= cDelay ) //if charge windup is finished
			{
                aimPivot.SetActive(false); //disable aim reticle

                vc.velocity = agent.transform.forward * cSpeed; //set velocity here??? Probably completely unnecessary???
				chargeTime -= Time.deltaTime; //lower amount of time to stay in motion
                nma.Move(vc.velocity * Time.deltaTime); //apply force to the agent
                nma.SetDestination(agent.transform.position); //update destination so it doesn't try to move while charging
                if (chargeTime <= 0 ) //if it should stop charging
				{
                    vc.velocity = new Vector3();
					chargeCooldownTimer.SetValue(chargeCooldown.value); //set cooldown to max value
                    EndAction(true); //end this action
				}
			} else //if it's winding up
			{
                chargeDelayTimer += Time.deltaTime; //increase the windup timer
                aimPivot.transform.localScale = new Vector3(1, 1, (chargeDelayTimer / cDelay) * cDistance * 0.33f); //make reticle longer
            }

		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}