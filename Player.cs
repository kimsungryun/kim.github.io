using UnityEngine;
using System.Collections;

namespace Completed
{
	public class Player : MovingObject
	{
		public float restartLevelDelay = 1f;
		public int pointsPerFood = 10;
		public int pointsPerSoda = 20;
		public int wallDamage = 1;

		private Animator animator;
		private int Crown;

		protected override void Start()
		{
			animator = GetComponent<Animator>();

			Crown = GameManager.instance.playerFoodPoints;

			foodText.text = "Crown: " + Crown;

			base.Start();
		}


		private void OnDisable()
		{
			GameManager.instance.playerFoodPoints = Crown;
		}


		private void Update()
		{

			if (!GameManager.instance.playersTurn) return;

			int horizontal = 0;
			int vertical = 0;
		}

		protected override void AttemptMove <T> (int xDir, int yDir)
        {
			Crown--;
        }
		private void CheckIfGameOver()
        {
			if (Crown <= 0)
				GameManager.instance.GameOver();
        }
	}
}

