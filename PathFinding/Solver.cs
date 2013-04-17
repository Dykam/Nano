using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PathFinding
{
	public abstract class Solver<TTile, TSubject> where TTile : Node, ISolverTile<TSubject>, new()
	{
		Dictionary<Tuple<Int2, Int2>, SearchResult> cache;

		public Map<TTile> Map { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public Heuristic Heuristic { get; private set; }
		public Heuristic TieBreaker { get; private set; }

		public Solver(Map<TTile> map, Heuristic heuristic, Heuristic tieBreaker = null, bool cache = false)
		{
			Map = map;
			Width = map.Width;
			Height = map.Height;
			Heuristic = heuristic;
			TieBreaker = tieBreaker ?? heuristic;
			if (cache)
				this.cache = new Dictionary<Tuple<Int2, Int2>, SearchResult>();
		}

		public Solver(int width, int height, Heuristic heuristic, Heuristic tieBreaker = null, bool cache = false)
			: this(new Map<TTile>(width, height), heuristic, tieBreaker, cache)
		{
		}

		public void ClearCache()
		{
			if (cache != null)
				cache.Clear();
		}

		public void Wrap(ref Int2 pos)
		{
			pos.X %= Width;
			pos.Y %= Height;

			if (pos.X < 0) pos.X += Width;
			if (pos.Y < 0) pos.Y += Height;
		}

		public SearchResult Search(Int2 from, TSubject subject, Int2 to)
		{
			var key = Tuple.Create(from, to);
			if (cache != null && cache.ContainsKey(key))
				return cache[key];
			var result = SearchImpl(from, subject, to);
			if (cache != null)
				cache.Add(key, result);
			return result;
		}

		protected abstract SearchResult SearchImpl(Int2 from, TSubject subject, Int2 to);
	}

	public class SearchResult : IEnumerable<Int2>
	{
		IEnumerable<Int2> path;

		public float Cost { get; private set; }
		public int Steps { get; private set; }

		public SearchResult(IEnumerable<Int2> path, float cost, int steps)
		{
			Cost = cost;
			Steps = steps;
			this.path = path;
		}

		public IEnumerator<Int2> GetEnumerator()
		{
			return path.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return path.GetEnumerator();
		}
	}

	public delegate float Heuristic(Int2 start, Int2 current, Int2 goal);

	public interface ISolverTile<in TSubject>
	{
		bool IsWalkableBy(TSubject subject);
		float Cost(TSubject subject);
	}
}
