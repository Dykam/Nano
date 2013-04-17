using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Nano
{
	using Nano;

	public class GameAwaiter : GameComponent
	{
		private readonly List<Job> _jobs = new List<Job>();

		public GameAwaiter(Game game)
			: base(game)
		{ }

		public Task Delay(TimeSpan interval)
		{
			return RegisterJob(new TimerJob { Interval = interval });
		}

		public Task Delay()
		{
			return RegisterJob(new TimerJob());
		}

		public Task While(Func<bool> condition)
		{
			return RegisterJob(new WhileJob { Condition = condition });
		}

		public void TryDisposeTask(Task tt)
		{
			var job = _jobs.FirstOrDefault(j => j.TaskO == tt);
			if (job != null)
				job.IsDisposed = true;
		}

		public override void Update(GameTime gametime)
		{
			var state = new State { GameTime = gametime, };

			foreach (var job in _jobs.ToArray()) {
				if (job.IsDisposed || job.Update(state))
					_jobs.Remove(job);
			}
		}


		private Task<T> RegisterJob<T>(Job<T> job)
		{
			_jobs.Add(job);
			return job.Task;
		}

		#region inner types

		private class State
		{
			public GameTime GameTime;
		}

		private abstract class Job
		{
			public bool IsDisposed { get; set; }
			public abstract Task TaskO { get; }

			public abstract bool Update(State state);
		}

		private abstract class Job<T> : Job
		{
			protected TaskCompletionSource<T> _source = new TaskCompletionSource<T>();
			public Task<T> Task { get { return _source.Task; } }
			public override Task TaskO { get { return Task; } }
		}

		private class TimerJob : Job<object>
		{
			private TimeSpan? StartTime;
			public TimeSpan? Interval;

			public override bool Update(State state)
			{
				if (!Interval.HasValue) {
					_source.TrySetResult(null);
					return true;
				}

				if (!StartTime.HasValue)
					StartTime = state.GameTime.TotalGameTime;

				if (state.GameTime.TotalGameTime - StartTime >= Interval) {
					_source.TrySetResult(null);
					return true;
				}

				return false;
			}
		}

		private class WhileJob : Job<object>
		{
			public Func<bool> Condition;

			public override bool Update(State state)
			{
				if (Condition()) {
					_source.TrySetResult(null);
					return true;
				}
				return false;
			}
		}

		#endregion

	}


	public static class GameAwaiterExtensions
	{
		public static Task<Branch> WhenAny(this GameAwaiter gameAwaiter, params Task[] tasks)
		{
			return GameAwaiterExtensions.WhenAny(tasks)
				.ContinueWith(t => {
					try {
						foreach (var tt in tasks)
							gameAwaiter.TryDisposeTask(tt);
						return t.Result;
					} catch (Exception exception) {
						return t.Result;
					}
				}, TaskContinuationOptions.ExecuteSynchronously);
		}

		public static Task<Branch> WhenAny(params Task[] tasks)
		{
			return Task.Factory.ContinueWhenAny(tasks, task => {
					var taskType = task.GetType();
					object value = null;

					// we cannot read nonpublic types via reflection in silverlight 
					if (taskType.IsGenericType && taskType.GetGenericArguments()[0].IsPublic)
						value = task.GetType().GetProperty("Result").GetValue(task, null);

					return new Branch { Index = Array.IndexOf(tasks, task), Value = value };
				}, TaskContinuationOptions.ExecuteSynchronously);
		}

		public static Task Delay(this GameAwaiter gameAwaiter, double milliseconds)
		{
			return gameAwaiter.Delay(TimeSpan.FromMilliseconds(milliseconds));
		}
	}


	public class Branch
	{
		public int Index;
		public object Value;
	}
}