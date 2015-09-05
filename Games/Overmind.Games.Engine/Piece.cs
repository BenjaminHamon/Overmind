using Overmind.Core;
using System;
using System.Collections.Generic;

namespace Overmind.Games.Engine
{
	public class Piece
	{
		public Piece(Player owner, Vector position)
		{
			this.Owner = owner;
			this.position = position;
		}

		public readonly Player Owner;

		private Vector position;
		public Vector Position
		{
			get { return position; }
			set
			{
				position = value;
				if (Moved != null)
					Moved(this);
			}
		}

		public IEnumerable<ICommand> CommandCollection;

		public override string ToString()
		{
			return String.Format("Piece Owner=[{0}] Position={1}", Owner, Position);
		}

		public virtual string ToShortString()
		{
			return "P";
		}

		public virtual string Image
		{
			get
			{
				if (Owner.Color == null)
					return GetType().Name;
				return Owner.Color.Value.Name + GetType().Name;
			}
		}

		public delegate void MovedEventHandler(Piece sender);
		public event MovedEventHandler Moved;

		public delegate void DestroyedEventHandler(Piece sender);
		public event DestroyedEventHandler Destroyed;

		public void Destroy()
		{
			Owner.PieceCollection.Remove(this);
			if (Destroyed != null)
				Destroyed(this);
		}
	}
}
