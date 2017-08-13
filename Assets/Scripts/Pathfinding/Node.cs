using System;

public class Node
{
	public Node parent;
	public Tile tile;
	public int gcost;
	public int hcost;
	public double distToParent;

	public Node (Tile tile)
	{
		this.tile = tile;
	}

	public int GetFcost() {
		return gcost + hcost;
	}

	public override bool Equals(object obj)
	{
		var node = obj as Node;

		if (node == null)
		{
			return false;
		}

		return node.tile.Equals (this.tile);
	}

}
