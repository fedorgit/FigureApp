using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FigureApp
{
    public class FigureView
    {
        private Vector[] VectorsBase;

        private Point CenterPoint;

        public Point[] PointsView;

        private Vector[] VectorsMouse;

        private double Scale { get; set; }

        private Point Position { get; set; }

        public bool IsSelect { get; set; }

        public bool IsCreate { get; set; } = false;

        public void CreateFigure(List<double> points)
        {
            int size = points.Count / 2;

            this.PointsView = new Point[size];

            for (int i = 0; i < size; i++)
                this.PointsView[i] = new Point(points[i * 2], points[i * 2 + 1]);
            
            this.ComputeCenter();

            this.VectorsBase = new Vector[size];

            for (int i = 0; i < this.PointsView.Length; i++)
            {
                this.VectorsBase[i].X = this.PointsView[i].X - this.CenterPoint.X;
                this.VectorsBase[i].Y = this.PointsView[i].Y - this.CenterPoint.Y;
            }

            this.VectorsMouse = new Vector[this.PointsView.Length];

            this.IsCreate = true;
        }

        /// <summary>
        /// Получить вектор расстояний для перемещения точек фигуры.
        /// </summary>
        /// <param name="point"></param>
        public void GetVectorsMouse(Point point)
        {
            if (!this.IsCreate) return;

            for (int i = 0; i < PointsView.Length; i++)
                this.VectorsMouse[i] = new Vector(this.PointsView[i].X - point.X, this.PointsView[i].Y - point.Y);
        }

        public void SetPosition(Point point)
        {
            if (!this.IsCreate) return;

            for (int i = 0; i < this.PointsView.Length; i++)
            {
                this.PointsView[i].X = point.X + this.VectorsMouse[i].X;
                this.PointsView[i].Y = point.Y + this.VectorsMouse[i].Y;
            }
        }

        public void SetScale(double scale)
        {
            if (!this.IsCreate) return;

            this.ComputeCenter();

            for (int i = 0; i < this.PointsView.Length; i++)
            {
                this.PointsView[i].X = this.CenterPoint.X + this.VectorsBase[i].X * scale;
                this.PointsView[i].Y = this.CenterPoint.Y + this.VectorsBase[i].Y * scale;
            }
        }


        private void ComputeCenter()
        {
            this.CenterPoint = new Point();

            for (int i = 0; i < this.PointsView.Length; i++)
            {
                this.CenterPoint.X += this.PointsView[i].X;
                this.CenterPoint.Y += this.PointsView[i].Y;
            }

            this.CenterPoint.X /= this.PointsView.Length;
            this.CenterPoint.Y /= this.PointsView.Length;
        }
    }
}
