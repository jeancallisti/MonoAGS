﻿using System;

namespace AGS.API
{
	public struct PointF
	{
		private readonly float _x, _y;

		public PointF(float x, float y)
		{
			_x = x;
			_y = y;
		}

		public float X { get { return _x; } }
		public float Y { get { return _y; } }

        public static PointF operator +(PointF p1, PointF p2)
        {
            return new PointF(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static PointF operator -(PointF p1, PointF p2)
        {
            return new PointF(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static PointF operator *(PointF p1, PointF p2)
        {
            return new PointF(p1.X * p2.X, p1.Y * p2.Y);
        }

        public static PointF operator /(PointF p1, PointF p2)
        {
            return new PointF(p1.X / p2.X, p1.Y / p2.Y);
        }

        public static PointF operator *(PointF p1, float factor)
        {
            return new PointF(p1.X * factor, p1.Y * factor);
        }

        public static PointF operator /(PointF p1, float factor)
        {
            return new PointF(p1.X / factor, p1.Y / factor);
        }

        public override string ToString ()
		{
			return string.Format ("{0:0.##},{1:0.##}", X, Y);
		}

		public override bool Equals(Object obj) 
		{
			if (obj == null) return false;
			if (!(obj is PointF)) return false;

			PointF p = (PointF)obj;
			return (X == p.X) && (Y == p.Y);
		}

		public override int GetHashCode() 
		{
			return X.GetHashCode() ^ Y.GetHashCode();
		}
	}
}

