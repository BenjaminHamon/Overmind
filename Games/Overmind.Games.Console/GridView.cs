using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Overmind.Games.Console
{
	public class GridView<TItem>
	{
		public GridView(TextWriter output)
		{
			if (output == null)
				throw new ArgumentNullException("output", "[GridView.Constructor] Output must not be null.");
			this.output = output;
		}

		private readonly TextWriter output;

		/// <summary>Action executed before an element is drawn.</summary>
		public Action<TItem> PreWrite;

		/// <summary>Action executed after an element is drawn.</summary>
		public Action<TItem> PostWrite;

		public bool DrawAxis = true;
		public string ColumnSeparator = "|";
		public char RowSeparator = '-';

		private double step = 1;
		public double Step
		{
			get { return step; }
			set
			{
				if (value <= 0)
					throw new ArgumentOutOfRangeException("value", value, "Step must be strictly positive");
				step = value;
			}
		}

		private int cellSize = 5;
		public int CellSize
		{
			get { return cellSize; }
			set
			{
				if (value <= 0)
					throw new ArgumentOutOfRangeException("value", value, "Cell size must be strictly positive");
				cellSize = value;
			}
		}

		public void Draw(double columnMin, double columnMax, double rowMin, double rowMax,
			IEnumerable<TItem> itemCollection, Func<TItem, IEnumerable<double>> positionGetter, Func<TItem, string> descriptor)
		{
			string rowCompleteSeparator = new String(RowSeparator, (CellSize + ColumnSeparator.Length)
				* ((int)Math.Floor((columnMax - columnMin) / Step) + (DrawAxis ? 1 : 0)) + CellSize);

			for (double rowIndex = rowMax; rowIndex >= rowMin; rowIndex -= Step)
			{
				if (rowIndex != rowMax)
					output.WriteLine(rowCompleteSeparator);

				if (DrawAxis)
					output.Write(Pad(rowIndex.ToString()) + ColumnSeparator);

				for (double columnIndex = columnMin; columnIndex <= columnMax; columnIndex += Step)
				{
					if (columnIndex != columnMin)
						output.Write(ColumnSeparator);
					double[] position = { columnIndex, rowIndex };
					TItem item = itemCollection.FirstOrDefault(x => positionGetter(x).SequenceEqual(position));
					if (EqualityComparer<TItem>.Default.Equals(item, default(TItem)) == false)
						DrawItem(item, descriptor);
					else
						output.Write(Pad(""));
				}

				output.WriteLine();

			}

			if (DrawAxis)
			{
				output.WriteLine(rowCompleteSeparator);

				output.Write(Pad("") + ColumnSeparator);
				for (double columnIndex = columnMin; columnIndex <= columnMax; columnIndex += Step)
				{
					if (columnIndex != columnMin)
						output.Write(ColumnSeparator);
					output.Write(Pad(columnIndex.ToString()));
				}
				output.WriteLine();
			}
		}

		private void DrawItem(TItem item, Func<TItem, string> descriptor)
		{
			if (PreWrite != null)
				PreWrite(item);
			output.Write(Pad(descriptor(item)));
			if (PostWrite != null)
				PostWrite(item);
		}

		private string Pad(string content)
		{
			int totalPadding = CellSize - content.Length;
			if (totalPadding <= 0)
				return content.Substring(0, CellSize);
			else
			{
				int leftPadding = totalPadding / 2;
				return new String(' ', leftPadding) + content + new String(' ', totalPadding - leftPadding);
			}
		}
	}
}
