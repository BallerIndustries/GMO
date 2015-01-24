using UnityEngine;
using System.Collections;

namespace Cat
{
	public class BowserCameraController : MonoBehaviour
	{
		public BowserPlayerController Player;
		public int OffsetX;
		public int OffsetY;
		public int DeadZoneWidth;
		public int DeadZoneHeight;
		public bool FollowPlayer;

		public void Update()
		{
			if (!FollowPlayer)
			{
				return;
			}

			float newCameraX;
			if ((Player.transform.position.x - Camera.main.transform.position.x + OffsetX) > DeadZoneWidth)
			{
				newCameraX = Player.transform.position.x - DeadZoneWidth + OffsetX;
			}
			else if ((Player.transform.position.x - Camera.main.transform.position.x + OffsetX) < -DeadZoneWidth)
			{
				newCameraX = Player.transform.position.x + DeadZoneWidth + OffsetX;
			}
			else
			{
				newCameraX = Camera.main.transform.position.x;
			}

			float newCameraY;
			if ((Player.transform.position.y - Camera.main.transform.position.y + OffsetY) > DeadZoneHeight)
			{
				newCameraY = Player.transform.position.y - DeadZoneHeight + OffsetY;
			}
			else if ((Player.transform.position.y - Camera.main.transform.position.y + OffsetY) < -DeadZoneHeight)
			{
				newCameraY = Player.transform.position.y + DeadZoneHeight + OffsetY;
			}
			else
			{
				newCameraY = Camera.main.transform.position.y;
			}

			var newCameraZ = Camera.main.transform.position.z;

			Camera.main.transform.position = new Vector3(newCameraX, newCameraY, newCameraZ);
		}
	}
}