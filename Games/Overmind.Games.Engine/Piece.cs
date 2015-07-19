using Overmind.Core;
using System;

namespace Overmind.Games.Engine
{
	public class Piece
	{
		public Piece(Player owner, Vector position, IRule rule)
		{
			this.owner = owner;
			this.rule = rule;
			this.Position = position;
		}

		private readonly Player owner;
		private readonly IRule rule;
		public Vector Position { get; internal set; }

		public override string ToString()
		{
			return String.Format("Piece Owner=[{0}] Position={1}", owner, Position);
		}

		public virtual string ToShortString()
		{
			return "P";
		}

		public virtual string Image { get { return owner.Color.Name + GetType().Name; } }

		public delegate void MovedEventHandler(Piece sender);
		public event MovedEventHandler Moved;

		public void Move(Vector destination)
		{
			if (rule.CanMove(Position, destination) == false)
				throw new Exception("Invalid move");

			DoMove(destination);
		}

		public void DoMove(Vector destination)
		{
			Position = destination;
			if (Moved != null)
				Moved(this);
		}

		public Vector Take(Piece target)
		{
			Vector destination;
			if (rule.CanEat(Position, target.Position, out destination) == false)
				throw new Exception("Invalid take");
			return destination;
		}

		public delegate void DestroyedEventHandler(Piece sender);
		public event DestroyedEventHandler Destroyed;

		internal void Destroy()
		{
			owner.PieceCollection.Remove(this);
			if (Destroyed != null)
				Destroyed(this);
		}
	}
}
