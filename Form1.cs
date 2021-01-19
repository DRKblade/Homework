using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1712400_BT1
{
  public partial class Form1 : Form
  {
    // Điểm đầu chuột
    Point pStart = new Point(0, 0);     

    // Điểm cuối chuột
    Point pEnd = new Point(0, 0);

    Point pLast = new Point();

    // Loại hình được chọn
    Shape.shapeType selectedType = Shape.shapeType.NONE;

    // Danh sách hình đang được vẽ
    List<Shape> shapes = new List<Shape>();

    // Độ dày nét vẽ
    int selectedWidth = 1;

    Color selectedColor = Color.White;

    // Kiểm tra tập hình có thay đổi không
    bool shapesChanged = true;

    // Tập điểm vẽ đa giác. 
    List<Point> NewPolygon = new List<Point>();

    // Index hình được chọn (-1 là không chọn gì)
    int selectedShape = -1;

    // Biến lựa chọn loại biến đổi affine
    int selectedAffine = -1;

    public Form1()
    {
      InitializeComponent();
    }

    private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
    {
      OpenGL gl = openGLControl.OpenGL;
      gl.ClearColor(0, 0, 0, 0);
      gl.MatrixMode(OpenGL.GL_PROJECTION);
      gl.LoadIdentity();
    }

    private void openGLControl_Resized(object sender, EventArgs e)
    {
      OpenGL gl = openGLControl.OpenGL;

      gl.MatrixMode(OpenGL.GL_PROJECTION);

      gl.LoadIdentity();

      // Tạo phép biến đổi góc nhìn
      gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
      gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);

    }

    private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
    {
      OpenGL gl = openGLControl.OpenGL;

      // Chỉ vẽ lại khi tập hình có sự thay đổi mới (để đo thời gian không bị lặp)
      if (shapesChanged) {
        gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

        if (shapes.Count > 0) {
          // Vẽ tới hình kế cuối, để hình cuối dùng đo thời gian
          for (int i = 0; i < shapes.Count - 1; i++)
            shapes[i].Draw(gl);
                    
          // Đo thời gian vẽ hình cuối trong tập hình
          Stopwatch watch = Stopwatch.StartNew();
          shapes.Last().Draw(gl);
          watch.Stop();
          label_Time.Text = watch.Elapsed.TotalMilliseconds.ToString() + " ms";
        }

        gl.Flush();
        shapesChanged = false;
      }

      // Vẽ tập điểm điều khiển
      if (selectedShape >= 0) {
        gl.PointSize(5);
        gl.Color(255.0, 0, 0);

        gl.Begin(OpenGL.GL_POINTS);

        for (int i = 0; i < shapes[selectedShape].ControlPoint.Count; i++)
          gl.Vertex(shapes[selectedShape].ControlPoint[i].X, gl.RenderContextProvider.Height - shapes[selectedShape].ControlPoint[i].Y);

        shapes[selectedShape].color = selectedColor;

        gl.End();
        gl.Flush();
      }
    }

    // Chọn vẽ đường thẳng
    private void btn_Line_Click(object sender, EventArgs e)
    {
      selectedType = Shape.shapeType.LINE;
      selectedShape = -1;
      selectedAffine = -1;
    }

    // Chọn vẽ hình tròn
    private void btn_Circle_Click(object sender, EventArgs e)
    {
      selectedType = Shape.shapeType.CIRCLE;
      selectedShape = -1;
      selectedAffine = -1;
    }

    // Chọn vẽ hình chữ nhật
    private void btn_Rectangle_Click(object sender, EventArgs e)
    {
      selectedType = Shape.shapeType.RECTANGLE;
      selectedShape = -1;
      selectedAffine = -1;
    }

    // Chọn vẽ hình Ellip
    private void btn_Ellipse_Click(object sender, EventArgs e)
    {
      selectedType = Shape.shapeType.ELLIPSE;
      selectedShape = -1;
      selectedAffine = -1;
    }

    // Chọn vẽ tam giác đều
    private void btn_Triangle_Click(object sender, EventArgs e)
    {
      selectedType = Shape.shapeType.TRIANGLE;
      selectedShape = -1;
      selectedAffine = -1;
    }

    // Chọn vẽ hình ngũ giác 
    private void btn_Pentagon_Click(object sender, EventArgs e)
    {
      selectedType = Shape.shapeType.PENTAGON;
      selectedShape = -1;
      selectedAffine = -1;
    }

    // Chọn vẽ hình lục giác
    private void btn_Hexagon_Click(object sender, EventArgs e)
    {
      selectedType = Shape.shapeType.HEXAGON;
      selectedShape = -1;
      selectedAffine = -1;
    }

    // Thay đổi nét vẽ
    private void num_LineWidth_Change(object sender, EventArgs e)
    {
      selectedWidth = (int)num_LineWidth.Value;
      selectedShape = -1;
      selectedAffine = -1;
    }

    // Chọn màu
    private void btn_Color_Click(object sender, EventArgs e)
    {
      if (colorDialog.ShowDialog() == DialogResult.OK)
        selectedColor = colorDialog.Color;
    }

    // Lúc nhấn chuột
    private void mouse_Down(object sender, MouseEventArgs e)
    {
      if (selectedType != Shape.shapeType.NONE) {
        if (e.Button == System.Windows.Forms.MouseButtons.Left) {
          pStart = pEnd = e.Location;
          // Vẽ hình theo nút đã lựa chọn
          shapes.Add(new Shape(selectedType, selectedWidth, selectedColor)); 
          shapes.Last().rasterPoints.Add(pStart);
        }

        // Chuột phải cho việc kết thúc vẽ đa giác
        if (e.Button == System.Windows.Forms.MouseButtons.Right) {
          // Nếu chọn vẽ polygon - vẽ đa giác.
          if (selectedType == Shape.shapeType.POLYGON) {
            // Tập điểm lớn hơn 2 thì vẽ
            if (NewPolygon.Count > 2) {
              ShapeConstruction construct = new ShapeConstruction();
              // vẽ đt từ điểm đầu đến cuối để hoàn thiện hình.
              construct.ConstructLine(shapes.Last(), NewPolygon[0], NewPolygon.Last()); 
              // cập nhật vẽ.
              shapesChanged = true; 
              // tạo lại tập điểm mới.
              NewPolygon = new List<Point>(); 
            }
          }
        }
      }
      else 
      {
        // Hiển thị control point nếu không trong trạng thái vẽ. - Nút None là thiết lập trái thái không vẽ.
        // Nhấn chuột trái
        if (e.Button == System.Windows.Forms.MouseButtons.Left) {
          pStart = pEnd = e.Location;
          pLast = pStart;
          // Tính khoảng cách min từ chuột đến hình
          
          double minDistance = 1000.0;
          // Hằng số epsilon
          double epsilon = 10.0;  

          for (int i = 0; i < shapes.Count; i++) {
            for (int j = 0; j < shapes[i].rasterPoints.Count; j++) {
              int dx = shapes[i].rasterPoints[j].X - e.Location.X;
              int dy = shapes[i].rasterPoints[j].Y - e.Location.Y;
              double distance = Math.Sqrt(dx * dx + dy * dy);

              if (distance < minDistance) {
                // Chọn hình hiển thị control point.
                selectedShape = i; 
                // Thiết lập minDistance để chọn ra hình nào gần hơn.
                minDistance = distance; 
              }
            }
          }

          // Khoảng cách nhỏ hơn epsilon thì chọn hình
          if (minDistance <= epsilon)    
          {
            shapesChanged = true;
            return;
          }

          // nếu không gần bất cứ hình nào thì tắt hết control point.
          selectedShape = -1; 
          shapesChanged = true;
          return;

        }
      }
    }

    private void mouse_Move(object sender, MouseEventArgs e) {
      ShapeConstruction construct = new ShapeConstruction();
      if (selectedType != Shape.shapeType.NONE) {
        if (e.Button == System.Windows.Forms.MouseButtons.Left) {
          // Liên tục cập nhật điểm cuối chuột
          pEnd = e.Location;      
          switch (selectedType) {
          // Chọn không gì cả
          case Shape.shapeType.NONE: 
            {
              break;
            }
          // Chon vẽ đường thẳng
          case Shape.shapeType.LINE: 
            {
              // Liên tục xóa để vẽ lại
              shapes.Last().rasterPoints.Clear();
              construct.ConstructLine(shapes.Last(), pStart, pEnd);
              break;
            }
          // Chọn vẽ đường tròn
          case Shape.shapeType.CIRCLE: 
            {
              shapes.Last().rasterPoints.Clear();
              construct.ConstructCircle(shapes.Last(), pStart, pEnd);
              break;
            }
          // Chọn vẽ hình chữ nhật
          case Shape.shapeType.RECTANGLE: 
            {
              shapes.Last().rasterPoints.Clear();
              construct.ConstructRectangle(shapes.Last(), pStart, pEnd);
              break;
            }
          // Chọn vẽ elip
          case Shape.shapeType.ELLIPSE: 
            {
              shapes.Last().rasterPoints.Clear();
              construct.ConstructEllipse(shapes.Last(), pStart, pEnd);
              break;
            }
          // Chọn vẽ tam giác
          case Shape.shapeType.TRIANGLE: 
            {
              shapes.Last().rasterPoints.Clear();
              construct.ConstructTriangle(shapes.Last(), pStart, pEnd);
              break;
            }
          // Chọn vẽ ngũ giác
          case Shape.shapeType.PENTAGON: 
            {
              shapes.Last().rasterPoints.Clear();
              construct.ConstructPentagon(shapes.Last(), pStart, pEnd);
              break;
            }
          // Chọn vẽ lục giác
          case Shape.shapeType.HEXAGON: 
            {
              shapes.Last().rasterPoints.Clear();
              construct.ConstructHexagon(shapes.Last(), pStart, pEnd);
              break;
            }
          // Chọn vẽ đa giác
          case Shape.shapeType.POLYGON: 
            {
              // Vẽ các đt nối tiếp lẫn nhau.
              // click chuột và di chuyển chuột 1 ít để tạo điểm.
              if (e.Button == System.Windows.Forms.MouseButtons.Left) 
              {
                NewPolygon.Add(pStart);
                if (NewPolygon.Count > 1)
                    construct.ConstructLine(shapes.Last(), NewPolygon[NewPolygon.Count - 2], NewPolygon.Last());
              }
              break;
            }
          }
        }
        shapesChanged = true;
      } else  {
        // Nếu chọn nút None để tiến vào trạng thái không vẽ
        if(selectedShape != -1) {
          // Nếu chọn biến đổi tịnh tiến
          if (selectedAffine == 1)  {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) {
             pEnd = e.Location;
             Point translate_vector = new Point();
             translate_vector.X = pEnd.X - pLast.X;
             translate_vector.Y = pEnd.Y - pLast.Y;
             shapes[selectedShape].Translate(translate_vector);
             pLast = pEnd;
             shapesChanged = true;
            }
          }

          // Nếu chọn co giãn hình
          if (selectedAffine ==  2)  {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) {
              pEnd = e.Location;
              pStart = shapes[selectedShape].inputPoint[0];
              shapes[selectedShape].rasterPoints.Clear();
              shapes[selectedShape].inputPoint.Clear();
              shapes[selectedShape].ControlPoint.Clear();

              switch (shapes[selectedShape].type) {
              case Shape.shapeType.LINE:
                construct.ConstructLine(shapes[selectedShape], pStart, pEnd);
                break;
              case Shape.shapeType.CIRCLE:
                construct.ConstructCircle(shapes[selectedShape], pStart, pEnd);
                break;
              case Shape.shapeType.RECTANGLE:
                construct.ConstructRectangle(shapes[selectedShape], pStart, pEnd);
                break;
              case Shape.shapeType.ELLIPSE:
                construct.ConstructEllipse(shapes[selectedShape], pStart, pEnd);
                break;
              case Shape.shapeType.TRIANGLE:
                construct.ConstructTriangle(shapes[selectedShape], pStart, pEnd);
                break;
              case Shape.shapeType.PENTAGON:
                construct.ConstructPentagon(shapes[selectedShape], pStart, pEnd);
                break;
              case Shape.shapeType.HEXAGON:
                construct.ConstructHexagon(shapes[selectedShape], pStart, pEnd);
                break;
              }
              shapesChanged = true;
            }
          }
        }
      }
    }

    //nút vẽ đa giác.
    private void btn_Point_conected_Click(object sender, EventArgs e)
    {
      selectedType = Shape.shapeType.POLYGON;
    }

    // xóa toàn bộ hình, thiết lập trạng thái mặc định.
    private void btn_clear_Click(object sender, EventArgs e) 
    {
      shapes.Clear();
      NewPolygon = new List<Point>();
      selectedShape = -1;
      selectedAffine = -1;
      shapesChanged = true;
    }

    // Trạng thái không vẽ gì cả. 
    private void btn_None_Click(object sender, EventArgs e)
    {
      selectedType = Shape.shapeType.NONE;
      selectedAffine = -1;
    }

    // Chọn tịnh tiến
    private void btn_Translate_Click(object sender, EventArgs e)
    {
      selectedAffine = 1; // Tịnh tiến.
    }

    // Chọn co giãn 
    private void btn_Scale_Click(object sender, EventArgs e) 
    {
      selectedAffine = 2; // Scale
    }
  }
}
