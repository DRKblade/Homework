using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1712400_BT1
{
  class ShapeConstruction		// Lớp các thuật toán vẽ hình
  {
    public void ConstructLine(Shape shape, Point pStart, Point pEnd)
    {
      // Control Point.
      shape.ControlPoint.Clear();
      shape.ControlPoint.Add(pStart);
      shape.ControlPoint.Add(pEnd);

      // inputPoint.
      shape.inputPoint.Add(pStart);
      shape.inputPoint.Add(pEnd);

			// Vẽ từ điểm có x nhỏ hơn
			if (pStart.X > pEnd.X)  {
        Point tmp;
        tmp = pStart;
        pStart = pEnd;
        pEnd = tmp;
      }

			// Tịnh tiến pStart đến (0, 0)
			Point translationVector = new Point(pStart.X, pStart.Y);
			pStart = new Point(0,0);
			pEnd = new Point(pEnd.X - translationVector.X, pEnd.Y - translationVector.Y);

			// Nếu dx bằng 0, đường thẳng đứng
			if (pEnd.X == 0)	{
				for (int i = Math.Min(0, pEnd.Y); i <= Math.Max(0, pEnd.Y); i++)
					shape.rasterPoints.Add(new Point(translationVector.X, i + translationVector.Y));
				return;
			}

			// Các thông số cần thiết
			int dy2 = 2 * pEnd.Y, dx2 = 2 * pEnd.X;
			float m = (float)dy2 / dx2;
			bool negativeM = false, largeM = false;

			// Nếu m < 0, đối xứng qua trục x = 0
			if (m < 0)	{
				pEnd.Y = -pEnd.Y;
				dy2 = -dy2;
				m = -m;
				negativeM = true;
			}

			// Nếu m > 1, đối xứng qua trục y = x
			if (m > 1) {
        int temp; 
        temp = pEnd.X;
        pEnd.X = pEnd.Y;
        pEnd.Y = temp;

        int tmp;
        tmp = dy2;
        dy2 = dx2;
        dx2 = tmp;
				largeM = true;
			}

			// Tính p0
			int p = dy2 - pEnd.X;

			// List các điểm vẽ
			List<Point> points = new List<Point>();

			int x = 0, y = 0;
			points.Add(new Point(x, y));
			while (x < pEnd.X)	{
				if (p > 0)	{
					x++;
					y++;
					p += dy2 - dx2;
				}	else	{
					x++;
					p += dy2;
				}
				points.Add(new Point(x, y));
			}

			// Đối xứng lại qua trục y = x
			if (largeM == true)
				for (int i = 0; i < points.Count; i++)
					points[i] = new Point(points[i].Y, points[i].X);

			// Đối xứng lại qua trục y = 0
			if (negativeM == true)
				for (int i = 0; i < points.Count; i++)
					points[i] = new Point(points[i].X, -points[i].Y);

			// Thêm tất cả vào tập điểm vẽ
			for (int i = 0; i < points.Count; i++)
				shape.rasterPoints.Add(new Point(points[i].X + translationVector.X, points[i].Y + translationVector.Y));
		}

    public void ConstructCircle(Shape shape, Point pStart, Point pEnd)
    {

      shape.inputPoint.Add(pStart);
      shape.inputPoint.Add(pEnd);
      
			// Winform tính điểm (0, 0) từ góc trên xuống => ngược chiều kim đồng hồ

			// Tính bán kính r
			double r = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2)) / 2;

			// Tính p0
			double p = 5 / 4 - r;

			// Điểm đầu (0, r)
			int x = 0;
			int y = (int)r;

			// Tâm đường tròn, cũng là vector tịnh tiến
			Point pCenter = new Point((pStart.X + pEnd.X) / 2, (pStart.Y + pEnd.Y) / 2);

			// Tập điểm vẽ ở 1/8 và đối xứng của 1/8 qua trục y = x
			List<Point> oneEighth = new List<Point>();

			//Thêm điểm đầu vào tập điểm vẽ
			oneEighth.Add(new Point(x, y));
			shape.rasterPoints.Add(new Point(x + pCenter.X, y + pCenter.Y));

			int x2 = x * 2, y2 = y * 2;

			// Midpoint
			while (y > x)	{
				if (p < 0)	{
					p += x2 + 3;
					x++;
					x2 += 2;
				}	else	{
					p += x2 - y2 + 5;
					x++;
					y--;
					x2 += 2;
					y2 -= 2;
				}

				// Tịnh tiến mỗi điểm theo tâm, rồi thêm vào tập điểm vẽ
				oneEighth.Add(new Point(x, y));
				shape.rasterPoints.Add(new Point(x + pCenter.X, y + pCenter.Y));
			}

			// Đối xứng qua trục y = x, rồi tịnh tiến theo tâm
			Point point;
			int i, size = oneEighth.Count();
			for (i = size - 1; i >= 0; i--)	{
				point = oneEighth[i];
				oneEighth.Add(new Point(point.Y, point.X));
				shape.rasterPoints.Add(new Point(point.Y + pCenter.X, point.X + pCenter.Y));
			}

			// Đối xứng 1/4 qua trục y = 0, rồi tịnh tiến theo tâm
			size = oneEighth.Count();
			for (i = size - 1; i >= 0; i--)	{
				point = oneEighth[i];
				oneEighth.Add(new Point(point.X, -point.Y));
				shape.rasterPoints.Add(new Point(point.X + pCenter.X, -point.Y + pCenter.Y));
			}

			// Đối xứng 1/2 qua trục x = 0, rồi tịnh tiến theo tâm
			size = oneEighth.Count();
			for (i = size - 1; i >= 0; i--)	{
				point = oneEighth[i];
				shape.rasterPoints.Add(new Point(-point.X + pCenter.X, point.Y + pCenter.Y));
			}


      Point temp = new Point(0, 0);
       
      //Control Point. có tất cả 8 điểm.
      shape.ControlPoint.Clear();
      // Điểm thứ 1.
      temp.X = pCenter.X - Convert.ToInt32(r);
      temp.Y = pCenter.Y + Convert.ToInt32(r);
      shape.ControlPoint.Add(temp); 


      // Điểm thứ 2.
      temp.X = pCenter.X;
      temp.Y = pCenter.Y + Convert.ToInt32(r);
      shape.ControlPoint.Add(temp); 

      // Điểm thứ 3.
      temp.X = pCenter.X + Convert.ToInt32(r);
      temp.Y = pCenter.Y + Convert.ToInt32(r);
      shape.ControlPoint.Add(temp); 

      // Điểm thứ 4.
      temp.X = pCenter.X - Convert.ToInt32(r);
      temp.Y = pCenter.Y;
      shape.ControlPoint.Add(temp); 

      // Điểm thứ 5
      temp.X = pCenter.X + Convert.ToInt32(r);
      temp.Y = pCenter.Y;
      shape.ControlPoint.Add(temp); 

      // Điểm thứ 6.
      temp.X = pCenter.X - Convert.ToInt32(r);
      temp.Y = pCenter.Y - Convert.ToInt32(r);
      shape.ControlPoint.Add(temp); 

      // Điểm thứ 7.
      temp.X = pCenter.X;
      temp.Y = pCenter.Y - Convert.ToInt32(r);
      shape.ControlPoint.Add(temp); 

      // Điểm thứ 8.
      temp.X = pCenter.X + Convert.ToInt32(r);
      temp.Y = pCenter.Y - Convert.ToInt32(r);
      shape.ControlPoint.Add(temp); 
		}

    public void ConstructRectangle(Shape shape, Point pStart, Point pEnd)
    {
			// Tính tọa độ 2 điểm còn lại của hình chữ nhật
			Point p1 = new Point(pEnd.X, pStart.Y);
			Point p2 = new Point(pStart.X, pEnd.Y);

			// Vẽ hình chữ nhật dùng thuật toán vẽ line
			ConstructLine(shape, pStart, p1);
			ConstructLine(shape, p1, pEnd);
			ConstructLine(shape, pEnd, p2);
			ConstructLine(shape, p2, pStart);

      shape.ControlPoint.Add(pStart);
      shape.ControlPoint.Add(p1);
      shape.ControlPoint.Add(pEnd);
      shape.ControlPoint.Add(p2);

      shape.inputPoint.Add(pStart);
      shape.inputPoint.Add(pEnd);
		}

    public void ConstructEllipse(Shape shape, Point pStart, Point pEnd)
    {
      // Điểm thứ 1.
      //Control Point. có tất cả 8 điểm.
      shape.ControlPoint.Clear();
      shape.ControlPoint.Add(pStart); 

      Point temp = new Point(0, 0);
      // Điểm thứ 2.
      temp.X = (pStart.X + pEnd.X) / 2;
      temp.Y = pStart.Y;
      shape.ControlPoint.Add(temp); 

      // Điểm thứ 3.
      temp.X = pEnd.X;
      temp.Y = pStart.Y;
      shape.ControlPoint.Add(temp); 

      // Điểm thứ 4.
      temp.X = pStart.X;
      temp.Y = (pStart.Y + pEnd.Y) / 2;
      shape.ControlPoint.Add(temp); 

      // Điểm thứ 5
      temp.X = pEnd.X;
      temp.Y = (pStart.Y + pEnd.Y) / 2;
      shape.ControlPoint.Add(temp); 

      // Điểm thứ 6.
      temp.X = pStart.X;
      temp.Y = pEnd.Y;
      shape.ControlPoint.Add(temp); 

      // Điểm thứ 7.
      temp.X = (pStart.X + pEnd.X) / 2;
      temp.Y = pEnd.Y;
      shape.ControlPoint.Add(temp); 

      // Điểm thứ 8.
      shape.ControlPoint.Add(pEnd); 

      shape.inputPoint.Add(pStart);
      shape.inputPoint.Add(pEnd);

			// Tính tâm ellipse
			Point pCenter = new Point((pStart.X + pEnd.X) / 2, (pStart.Y + pEnd.Y) / 2);

			// Tính đường kính rX
			double rX = Math.Sqrt(Math.Pow(pStart.X - pEnd.X, 2) + Math.Pow(pStart.Y - pStart.Y, 2)) / 2;

			// Tính đường kính rY
			double rY = Math.Sqrt(Math.Pow(pStart.X - pStart.X, 2) + Math.Pow(pStart.Y - pEnd.Y, 2)) / 2;

			//Điểm đầu (0, rY)
			int x = 0;
			int y = (int)rY;

			// Tập điểm ở 1/4
			List<Point> oneFourth = new List<Point>();

			// Thêm điểm đầu vào tập điểm vẽ
			oneFourth.Add(new Point(x, y));
			shape.rasterPoints.Add(new Point(x + pCenter.X, y + pCenter.Y));

			// Các thông số cần thiết
			double rX2 = rX * rX, rY2 = rY * rY;
			double rX2y = 2 * rX2 * y;
			double rY2x = 2 * rY2 * x;
			double decision = rY2 - rX2 * rY + rX2 / 4;

			while (rY2x < rX2y)	{
				if (decision < 0)	{
					x++;
					rY2x += 2 * rY2;
					decision += rY2x + rY2;
				}	else	{
					x++;
					y--;
					rY2x += 2 * rY2;
					rX2y -= 2 * rX2;
					decision += rY2x - rX2y + rY2;
				}

				// Tịnh tiến mỗi điểm theo tâm, rồi thêm vào tập điểm vẽ
				oneFourth.Add(new Point(x, y));
				shape.rasterPoints.Add(new Point(x + pCenter.X, y + pCenter.Y));
			}

			// xLast, yLast
			rX2y = 2 * rX2 * y;
			rY2x = 2 * rY2 * x;
			decision = rY2 * Math.Pow((x + (1 / 2)), 2) + rX2 * Math.Pow((y - 1), 2) - rX2 * rY2;

			while (y >= 0)	{
				if (decision > 0)	{
					y--;
					rX2y -= 2 * rX2;
					decision -= rX2y + rX2;
				}	else	{
					x++;
					y--;
					rY2x += 2 * rY2;
					rX2y -= 2 * rX2;
					decision += rY2x - rX2y + rX2;
				}

				// Tịnh tiến mỗi điểm theo tâm, rồi thêm vào tập điểm vẽ
				oneFourth.Add(new Point(x, y));
				shape.rasterPoints.Add(new Point(x + pCenter.X, y + pCenter.Y));
			}

			// Đối xứng 1/4 qua trục x = 0, rồi thêm vào tập điểm vẽ
			int size = oneFourth.Count();
			for (int i = size - 1; i >= 0; i--)	{
				Point p = oneFourth[i];
				oneFourth.Add(new Point(p.X, -p.Y));
				shape.rasterPoints.Add(new Point(p.X + pCenter.X, -p.Y + pCenter.Y));
			}

			// Đối xứng 1/2 qua trục y = 0, rồi thêm vào tập điểm vẽ
			size = oneFourth.Count();
			for (int i = size - 1; i >= 0; i--)	{
				Point p = oneFourth[i];
				oneFourth.Add(new Point(-p.X, p.Y));
				shape.rasterPoints.Add(new Point(-p.X + pCenter.X, p.Y + pCenter.Y));
			}
		}

    // cac hinh hoc deu luon noi tiep duong tron theo quy luat nhat dinh.

    public void ConstructTriangle(Shape shape, Point pStart, Point pEnd) 
    {
      Point center = new Point(0,0); // tam giac deu luon noi tiep duong tron va dinh luon cach nhau 120

      // tâm tam giác đều lấy ở giữa 2 điểm bắt đầu vẽ và kết thúc vẽ.
      center.X = (pStart.X + pEnd.X) / 2; 
      center.Y = (pStart.Y + pEnd.Y) / 2;

      // tam giác đều luôn nội tiếp đường tròn bán kính bằng k/c giữa 2 điểm vẽ chia 2.
      double R = Math.Sqrt(Math.Pow((pStart.X - pEnd.X), 2) + Math.Pow((pStart.Y - pEnd.Y), 2)) / 2.0;

      // 3 điểm tạo thành tam giác đều.
      Point p1 = new Point(0,0); 
      Point p2 = new Point(0,0);
      Point p3 = new Point(0,0);

      // các điểm tạo thành tam giác đều.
      p1.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 30 / 360) + center.X);
      p1.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 30 / 360) + center.Y);

      p2.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 150 / 360) + center.X);
      p2.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 150 / 360) + center.Y);

      p3.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 270 / 360) + center.X);
      p3.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 270 / 360) + center.Y);

      // vẽ đoạn thẳng nối 3 điểm.
      ConstructLine(shape, p1, p2);
      ConstructLine(shape, p1, p3);
      ConstructLine(shape, p2, p3);
      
      // Control Point
      shape.ControlPoint.Clear();
      shape.ControlPoint.Add(p1);
      shape.ControlPoint.Add(p2);
      shape.ControlPoint.Add(p3);

      shape.inputPoint.Add(pStart);
      shape.inputPoint.Add(pEnd);
		}

    public void ConstructPentagon(Shape shape, Point pStart, Point pEnd)
    {
      Point center = new Point(0, 0); // ngu giac deu luon noi tiep duong tron va dinh luon cach nhau 72

      // tâm ngũ giác đều lấy ở giữa 2 điểm bắt đầu vẽ và kết thúc vẽ.
      center.X = (pStart.X + pEnd.X) / 2;
      center.Y = (pStart.Y + pEnd.Y) / 2;

      // ngũ giác đều luôn nội tiếp đường tròn bán kính bằng k/c giữa 2 điểm vẽ chia 2.
      double R = Math.Sqrt(Math.Pow((pStart.X - pEnd.X), 2) + Math.Pow((pStart.Y - pEnd.Y), 2)) / 2.0;

      // 5 điểm để tạo thành ngũ giác.
      Point p1 = new Point(0, 0);
      Point p2 = new Point(0, 0);
      Point p3 = new Point(0, 0);
      Point p4 = new Point(0, 0);
      Point p5 = new Point(0, 0);

      // các điểm tạo thành ngũ giác đều.
      p1.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 54 / 360) + center.X);
      p1.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 54 / 360) + center.Y);

      p2.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 126 / 360) + center.X);
      p2.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 126 / 360) + center.Y);

      p3.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 198 / 360) + center.X);
      p3.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 198 / 360) + center.Y);

      p4.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 270 / 360) + center.X);
      p4.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 270 / 360) + center.Y);

      p5.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 342 / 360) + center.X);
      p5.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 342 / 360) + center.Y);

      // đựng đoạn thẳng qua 5 điểm trên.
      ConstructLine(shape, p1, p2);
      ConstructLine(shape, p2, p3);
      ConstructLine(shape, p3, p4);
      ConstructLine(shape, p4, p5);
      ConstructLine(shape, p5, p1);

      //Control Point
      shape.ControlPoint.Clear();
      shape.ControlPoint.Add(p1);
      shape.ControlPoint.Add(p2);
      shape.ControlPoint.Add(p3);
      shape.ControlPoint.Add(p4);
      shape.ControlPoint.Add(p5);

      shape.inputPoint.Add(pStart);
      shape.inputPoint.Add(pEnd);
    }

    public void ConstructHexagon(Shape shape, Point pStart, Point pEnd)
    {
      // luc deu luon noi tiep duong tron va dinh luon cach nhau 60
      Point center = new Point(0, 0); 

      // tâm lục giác đều lấy ở giữa 2 điểm bắt đầu vẽ và kết thúc vẽ.
      center.X = (pStart.X + pEnd.X) / 2;
      center.Y = (pStart.Y + pEnd.Y) / 2;

      // lục giác đều luôn nội tiếp đường tròn bán kính bằng k/c giữa 2 điểm vẽ chia 2.
      double R = Math.Sqrt(Math.Pow((pStart.X - pEnd.X), 2) + Math.Pow((pStart.Y - pEnd.Y), 2)) / 2.0;


      // 6 điểm tạo thành lục giác
      Point p1 = new Point(0, 0);
      Point p2 = new Point(0, 0);
      Point p3 = new Point(0, 0);
      Point p4 = new Point(0, 0);
      Point p5 = new Point(0, 0);
      Point p6 = new Point(0, 0);


      // các điểm tạo thành lục giác đều.
      p1.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 30 / 360) + center.X);
      p1.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 30 / 360) + center.Y);

      p2.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 90 / 360) + center.X);
      p2.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 90 / 360) + center.Y);

      p3.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 150 / 360) + center.X);
      p3.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 150 / 360) + center.Y);

      p4.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 210 / 360) + center.X);
      p4.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 210 / 360) + center.Y);

      p5.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 270 / 360) + center.X);
      p5.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 270 / 360) + center.Y);

      p6.X = Convert.ToInt32(R * Math.Cos(2 * Math.PI * 330 / 360) + center.X);
      p6.Y = Convert.ToInt32(R * Math.Sin(2 * Math.PI * 330 / 360) + center.Y);


      // vẽ đoạn thẳng qua 6 điểm trên
      ConstructLine(shape, p1, p2);
      ConstructLine(shape, p2, p3);
      ConstructLine(shape, p3, p4);
      ConstructLine(shape, p4, p5);
      ConstructLine(shape, p5, p6);
      ConstructLine(shape, p6, p1);

      shape.ControlPoint.Clear();
      shape.ControlPoint.Add(p1);
      shape.ControlPoint.Add(p2);
      shape.ControlPoint.Add(p3);
      shape.ControlPoint.Add(p4);
      shape.ControlPoint.Add(p5);
      shape.ControlPoint.Add(p6);

      shape.inputPoint.Add(pStart);
      shape.inputPoint.Add(pEnd);
    }
  }
}
