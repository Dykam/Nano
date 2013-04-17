using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections.Concurrent;

namespace PathFinding
{
	public class Heap<T> where T : IComparable<T>
	{
		internal List<HeapNode<T>> nodes;
		public HeapNode<T> First { get { return nodes.Count == 0 ? null : nodes[0]; } }
		HeapNode<T> Last { get { return nodes.Count == 0 ? null : nodes[nodes.Count - 1]; } }

		public int Length { get { return nodes.Count; } }
		public bool Empty { get { return Length == 0; } }


		public Heap()
		{
			nodes = new List<HeapNode<T>>();
		}

		public HeapNode<T> Insert(T value)
		{
			HeapNode<T> node = new HeapNode<T>(value) {
				Tree = this,
				index = nodes.Count
			};
			nodes.Add(node);
			rebalance(node);
			return node;
		}

		public void Delete(HeapNode<T> node)
		{
			if (node.Tree != this)
				throw new ArgumentException("Node not part of the current Heap", "node");

			var last = Last;
			swap(node, last);
			nodes.RemoveAt(nodes.Count - 1);
			tryMoveDown(last);

			node.Tree = null;
			node.index = 0;
		}

		public T Pop()
		{
			var first = First;
			Delete(first);
			return first.value;
		}

		internal void rebalance(HeapNode<T> node)
		{
			tryMoveUp(node);
			tryMoveDown(node);
		}

		void tryMoveUp(HeapNode<T> node)
		{
			if (node.Parent == null)
				return;
			if (node.Parent.value.CompareTo(node.value) >= 0)
				return;
			swap(node, node.Parent);
			tryMoveUp(node);
		}

		void tryMoveDown(HeapNode<T> node)
		{
			var left = node.Left;
			var right = node.Right;
			var largest = node;
			if (left != null && left.value.CompareTo(largest.value) > 0)
				largest = left;
			if (right != null && right.value.CompareTo(largest.value) > 0)
				largest = right;

			if (largest != node) {
				swap(largest, node);
				tryMoveDown(node);
			}
		}

		void swap(HeapNode<T> node1, HeapNode<T> node2)
		{
			nodes[node1.index] = node2;
			nodes[node2.index] = node1;
			var index1 = node1.index;
			node1.index = node2.index;
			node2.index = index1;
		}
	}

	public class HeapNode<T> where T : IComparable<T>
	{
		internal T value;
		public T Value
		{
			get
			{
				return value;
			}
			set
			{
				this.value = value;
				Tree.rebalance(this);
			}
		}

		internal int index;

		internal HeapNode(T value)
		{
			this.value = value;
		}

		public Heap<T> Tree { get; internal set; }
		public HeapNode<T> Parent { get { return index == 0 ? null : Tree.nodes[(index - 1) / 2]; } }
		public HeapNode<T> Left { get { return index * 2 + 1 >= Tree.nodes.Count ? null : Tree.nodes[index * 2 + 1]; } }
		public HeapNode<T> Right { get { return index * 2 + 2 >= Tree.nodes.Count ? null : Tree.nodes[index * 2 + 2]; } }

		public override string ToString()
		{
			return value.ToString();
		}

		public void VerifyValid()
		{
			if (Left != null && (value.CompareTo(Left.value) < 0))
				throw new Exception();
			if (Right != null && (value.CompareTo(Right.value) < 0))
				throw new Exception();
			if (Left != null)
				Left.VerifyValid();
			if (Right != null)
				Right.VerifyValid();
		}
	}
}
