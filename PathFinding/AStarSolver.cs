using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Collections.Concurrent;
using System.Threading;
using System.Collections;

namespace PathFinding
{
	public class AStarSolver<TTile, TSubject> : Solver<TTile, TSubject> where TTile : Node, ISolverTile<TSubject>, new()
	{
		Int2[] directions = new Int2[] {
			new Int2( 1, 0),
			new Int2(-1, 0),
			new Int2(0,  1),
			new Int2(0, -1),
		};
		bool[,] closed;

		public AStarSolver(Map<TTile> map, Heuristic heuristic, Heuristic tieBreaker = null, bool cache = false)
			: base(map, heuristic, tieBreaker, cache)
		{
			closed = new bool[Width, Height];
		}

		public AStarSolver(int width, int height, Heuristic heuristic, Heuristic tieBreaker = null, bool cache = false)
			: base(width, height, heuristic, tieBreaker, cache)
		{
			closed = new bool[Width, Height];
		}

		public Step current;
		public ConcurrentBag<Step> visited;
		protected override SearchResult SearchImpl(Int2 from, TSubject subject, Int2 to)
		{
			if (!Map[from.X, from.Y].IsWalkableBy(subject) || !Map[to.X, to.Y].IsWalkableBy(subject))
				return null;

			visited = new ConcurrentBag<Step>();
			var openSet = new Heap<Step>();
			Array.Clear(closed, 0, closed.Length);
			openSet.Insert(new Step(from));
			closed[from.X, from.Y] = true;

			Array.Clear(closed, 0, closed.Length);

			while (!openSet.Empty) {
				current = openSet.Pop();
				visited.Add(current);
				if (current.To == to) {
					return UnwrapSteps(current);
				}

				foreach (var dir in directions) {
					var neighbour = current.To + dir;
					Wrap(ref neighbour);
					if (closed[neighbour.X, neighbour.Y])
						continue;
					var node = Map[neighbour.X, neighbour.Y];
					if (node.IsWalkableBy(subject)) {
						openSet.Insert(new Step(current, neighbour, node.Cost(subject), Heuristic(from, neighbour, to), TieBreaker(from, neighbour, to)));
						closed[neighbour.X, neighbour.Y] = true;
					}
				}
			}
			return null;
		}

		public SearchResult UnwrapSteps(Step current)
		{
			var cost = current.CurrentCost;
			var stack = new Stack<Int2>();
			while (current != null) {
				stack.Push(current.To);
				current = current.Previous;
			}
			return new SearchResult(stack, cost, stack.Count);
		}
	}

	public class Step : IComparable<Step>
	{
		public Step Previous { get; private set; }
		public Int2 To { get; private set; }
		public float StepCost { get; private set; }
		public float CurrentCost { get; private set; }
		public float EstimatedTotalCost { get; private set; }
		public float Tiebreaker { get; private set; }
		public Step(Step previous, Int2 to, float stepCost, float estimated, float tiebreaker)
		{
			StepCost = stepCost;
			CurrentCost = previous.CurrentCost + stepCost;
			EstimatedTotalCost = CurrentCost + estimated;
			Previous = previous;
			To = to;
		}

		public Step(Int2 basePos)
		{
			To = basePos;
		}

		public int CompareTo(Step other)
		{
			var equality = other.EstimatedTotalCost.CompareTo(EstimatedTotalCost);
			if (equality == 0)
				equality = other.Tiebreaker.CompareTo(Tiebreaker);
			return equality;
		}

		public override string ToString()
		{
			return string.Format("{0}@{1}->{2}", To, CurrentCost, EstimatedTotalCost);
		}
	}
}
