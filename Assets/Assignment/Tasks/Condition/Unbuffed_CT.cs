using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class Unbuffed_CT : ConditionTask {

        public BBParameter<GameObject> ally;
        private GameObject a;
		public BBParameter<float> reload;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            a = ally.value;
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
			if (a.GetComponent<ChargerBuffHandler>().buffTimer <= 0 && reload.value <= 0) { return true; } else { return false; } //if ally has no buff, attempt to cast a new one
		}
	}
}