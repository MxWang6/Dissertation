using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class NotificationSystem
{
	private static Queue<MonsterMoveEvent> queue = new Queue<MonsterMoveEvent>();
	private static HashSet<Player> subscribers = new HashSet<Player> ();

	public static void subscribe(Player player) {
		subscribers.Add (player);
	}

	public static void publish(MonsterMoveEvent moveEvent) {
		queue.Enqueue (moveEvent);
	}

	public static void start() {
		queue.Clear ();
		subscribers.Clear ();

		new Thread(() =>
			{
				Thread.CurrentThread.IsBackground = true;

				while (true) {
					if (queue.Count > 0) {
						MonsterMoveEvent eventObj = queue.Dequeue ();
						foreach (Player subscriber in subscribers) {
							subscriber.monsterMoved ((MonsterMoveEvent) eventObj);
						}
					}
				}
			}).Start();
	}
}
