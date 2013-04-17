using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PathFinding
{
	public class Map<TNode> : IEnumerable<TNode> where TNode : Node, new()
	{
		TNode[,] grid;

		public int Width { get; private set; }
		public int Height { get; private set; }

		public Map(int width, int height)
		{
			Width = width;
			Height = height;
			grid = new TNode[Width, Height];
			for (int y = 0; y < Height; y++) {
				for (var x = 0; x < Width; x++) {
					grid[x, y] = new TNode {
						X = x,
						Y = y
					};
				}
			}
		}

		public void Wrap(ref Int2 pos)
		{
			pos.X %= Width;
			pos.Y %= Height;

			if (pos.X < 0) pos.X += Width;
			if (pos.Y < 0) pos.Y += Height;
		}

		public TNode this[int x, int y]
		{
			get { return grid[x, y]; }
		}
		public TNode this[Int2 pos]
		{
			get { return grid[pos.X, pos.Y]; }
		}

		public IEnumerable<TNode> NeighboursOf(TNode node)
		{
			return NeighboursOf(new Int2(node.X, node.Y));
		}

		public IEnumerable<TNode> NeighboursOf(Int2 pos)
		{
			yield return GetWrapped(pos - Int2.UnitX);
			yield return GetWrapped(pos + Int2.UnitX);
			yield return GetWrapped(pos - Int2.UnitY);
			yield return GetWrapped(pos + Int2.UnitY);
		}

		public TNode GetWrapped(Int2 pos)
		{
			Wrap(ref pos);
			return this[pos];
		}

		public IEnumerator<TNode> GetEnumerator()
		{
			return grid.Cast<TNode>().GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return grid.GetEnumerator();
		}
	}

	public class Node
	{
		public int X { get; internal set; }
		public int Y { get; internal set; }
	}
}
