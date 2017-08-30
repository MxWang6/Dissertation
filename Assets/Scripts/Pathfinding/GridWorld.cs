using System;
using System.Collections;
using System.Collections.Generic;

public class GridWorld
{
	private Tile[,] gridWorld;
	private int width;
	private int height;

	public GridWorld (int width, int height)
	{
		gridWorld = new Tile[width, height];
		this.width = width;
		this.height = height;
	}

	public Tile getTile(Position position) {
		return gridWorld [position.x, position.y];
	}

	public void addTile(int x, int y, Tile tile) {
		gridWorld [x, y] = tile;
	}


	public List<Node> findPath(Position currentPosition, Position targetPosition) {
		Tile currentTile = gridWorld [currentPosition.x, currentPosition.y];
		Tile targetTile = gridWorld [targetPosition.x, targetPosition.y];

		List<Node> open = new List<Node> ();
		List<Node> close = new List<Node> ();

		Node startNode = new Node (currentTile);
		Node targetNode = new Node (targetTile);
		open.Add (startNode);
		Node currentNode = startNode;
		while (open.Count > 0) {
			currentNode = open [0];
			open.ForEach( (node) => {
				if (node.GetFcost() < currentNode.GetFcost() || node.GetFcost() == currentNode.GetFcost() && node.hcost < currentNode.hcost) {
					currentNode = node;
				}
			});
			open.Remove (currentNode);
			close.Add (currentNode);

			if (currentNode.Equals(targetNode)) {
				return GetPath (startNode, currentNode);
			}

			findNearNodes(currentNode).ForEach((nearNode) => {
				if (!nearNode.tile.blocked && !close.Contains(nearNode)) {
					int newGcostToNearNode = currentNode.gcost + GetDistance(currentNode, nearNode);
					double currentToNearNodeDist = GetRealDistance(currentNode, nearNode);

					if (newGcostToNearNode < nearNode.gcost || !open.Contains(nearNode)) {

						if (!open.Contains(nearNode)) {
							open.Add(nearNode);
						} else {
							// we wanna retrieve the near node stored in the open list already to update.
							nearNode = open[open.IndexOf(nearNode)];
						}
						nearNode.gcost = newGcostToNearNode;
						nearNode.hcost = GetDistance(nearNode, targetNode);
						nearNode.parent = currentNode;
						nearNode.distToParent = currentToNearNodeDist;
					}
				}
			});
		}

		// no path is found, e.g. no path can reach the distination.
		return GetPath (startNode, currentNode);
	}

	private List<Node> findNearNodes(Node currentNode) {
		Position currentPosition = currentNode.tile.getPosition ();
		List<Node> nearNodes = new List<Node> ();

		for (int x = -1; x <= 1; x++) {
			for (int y = -1; y <= 1; y++) {
				if (x == 0 && y == 0) {
					continue;
				}
				int nearPositionX = currentPosition.x + x;
				int nearPositionY = currentPosition.y + y;
				if (nearPositionX >= 0 && nearPositionX < width && nearPositionY >= 0 && nearPositionY < height) {
					nearNodes.Add (new Node (gridWorld [nearPositionX, nearPositionY]));
				}
			}
		}

		return nearNodes;
	}

	private int GetDistance(Node node1, Node node2) {
		int distanceX = Math.Abs (node1.tile.position.x - node2.tile.position.x);
		int distanceY = Math.Abs (node1.tile.position.y - node2.tile.position.y);

		if (distanceX > distanceY) {
			return 14 * distanceY + 10 * (distanceX - distanceY)+ (int)node2.tile.monsterCost; //added Monster cost here
		}

		return 14 * distanceX + 10 * (distanceY - distanceX)+ (int)node2.tile.monsterCost;
	}

	private double GetRealDistance(Node node1, Node node2) {
		double distanceX = Math.Abs (node1.tile.position.x - node2.tile.position.x);
		double distanceY = Math.Abs (node1.tile.position.y - node2.tile.position.y);

		if (distanceX > distanceY) {
			return Math.Sqrt(2) * distanceY + 1 * (distanceX - distanceY) + node2.tile.monsterCost; //added Monster cost here
		}

		return Math.Sqrt(2) * distanceX + 1 * (distanceY - distanceX) + node2.tile.monsterCost;
	}

	private List<Node> GetPath(Node startNode, Node endNode) {
		List<Node> path = new List<Node> ();
		Node currentNode = endNode;
		while (!currentNode.Equals(startNode)) {
			currentNode.tile.highlighted = true;
			path.Add (currentNode);
			currentNode = currentNode.parent;
		}
		path.Reverse ();

		return path;
	}
}
	