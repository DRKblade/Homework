using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1712400_BT1
{
  class Shape
  {
    public enum shapeType { NONE, LINE, CIRCLE, RECTANGLE, ELLIPSE, TRIANGLE, PENTAGON, HEXAGON, POLYGON};

    public shapeType type;

    public int lineWidth;

    public Color color;

    // Tập điểm vẽ
    public List<Point> rasterPoints;  

    // Tập điểm control point
    public List<Point> ControlPoint = new List<Point>(); 

    // Tập điểm gốc khi truyền để xét biến đổi
    public List<Point> inputPoint = new List<Point>(); 

    // Khởi tạo
    public Shape(shapeType selectedType, int selectedWidth, Color selectedColor)  
    {
      type = selectedType;
      lineWidth = selectedWidth;
      color = selectedColor;
      rasterPoints = new List<Point>();
    }
    // Hàm vẽ từng pixel
    public void Draw(OpenGL gl)   
    {
      gl.PointSize(lineWidth);
      gl.Color(color.R, color.G, color.B);
      gl.Begin(OpenGL.GL_POINTS);

      for (int i = 0; i < rasterPoints.Count; i++)
        gl.Vertex(rasterPoints[i].X, gl.RenderContextProvider.Height - rasterPoints[i].Y);
      gl.End();
    }

    // Phép tịnh tiến 
    public void Translate(Point translate_Vector) 
    {
      // tạo tập rasterPoints tạm
      List<Point> temp_rasterPoints = new List<Point>(rasterPoints); 
      // tạo tập điều khiển tạm
      List<Point> temp_ctrlPoint = new List<Point>(ControlPoint); 
      // tạo tập inputPoint tạm
      List<Point> temp_inputPoint = new List<Point>(inputPoint); 
      Point temp_point = new Point();

      // Xóa các tập gốc để tiến hành thay đổi điểm mới
      rasterPoints.Clear(); 
      ControlPoint.Clear();
      inputPoint.Clear();

      for (int i = 0; i < temp_rasterPoints.Count; i++) {
        AffineTransform affinematrix = new AffineTransform();
        // Tịnh tiến 1 điểm theo vecto tịnh tiến
        affinematrix.Translate(translate_Vector.X, translate_Vector.Y, temp_rasterPoints[i]); 
        temp_point.X = Convert.ToInt32(affinematrix.result[0]);
        temp_point.Y = Convert.ToInt32(affinematrix.result[1]);
        rasterPoints.Add(temp_point);
      }

      for (int i = 0; i < temp_ctrlPoint.Count; i++) {
        AffineTransform affinematrix = new AffineTransform();
        affinematrix.Translate(translate_Vector.X, translate_Vector.Y, temp_ctrlPoint[i]);
        temp_point.X = Convert.ToInt32(affinematrix.result[0]);
        temp_point.Y = Convert.ToInt32(affinematrix.result[1]);
        ControlPoint.Add(temp_point);
      }

      for (int i = 0; i < temp_inputPoint.Count; i++) {
        AffineTransform affinematrix = new AffineTransform();
        affinematrix.Translate(translate_Vector.X, translate_Vector.Y, temp_inputPoint[i]);
        temp_point.X = Convert.ToInt32(affinematrix.result[0]);
        temp_point.Y = Convert.ToInt32(affinematrix.result[1]);
        inputPoint.Add(temp_point);
      }
    }
  }
}
