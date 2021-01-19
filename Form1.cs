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

		Point pStart = new Point(0, 0);     // Điểm đầu chuột
		Point pEnd = new Point(0, 0);       // Điểm cuối chuột
        Point pLast = new Point();
		Shape.shapeType selectedType = Shape.shapeType.NONE;       // Loại hình được chọn
		List<Shape> shapes = new List<Shape>();     // Danh sách hình đang được vẽ
		int selectedWidth = 1;      // Độ dày nét vẽ
		Color selectedColor = Color.White;
		bool shapesChanged = true;		// Kiểm tra tập hình có thay đổi không

        List<Point> NewPolygon = new List<Point>();// Tập điểm vẽ đa giác. 
       
        
        int selectedShape = -1;     // Index hình được chọn (-1 là không chọn gì)

        int selectedAffine = -1; // Biến lựa chọn loại biến đổi affine


        public Form1()
        {
            InitializeComponent();
        }

		private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
		{
			// Get the OpenGL object
			OpenGL gl = openGLControl.OpenGL;

			// Set the clear color
			gl.ClearColor(0, 0, 0, 0);

			// Set projection matrix
			gl.MatrixMode(OpenGL.GL_PROJECTION);

			// Load the identity
			gl.LoadIdentity();
		}

		private void openGLControl_Resized(object sender, EventArgs e)
		{
			OpenGL gl = openGLControl.OpenGL;

			gl.MatrixMode(OpenGL.GL_PROJECTION);

			gl.LoadIdentity();

			// Create a perspective transformation
			gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
			gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);

		}

		private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
		{
			OpenGL gl = openGLControl.OpenGL;

			if (shapesChanged)	// Chỉ vẽ lại khi tập hình có sự thay đổi mới (để đo thời gian không bị lặp)
			{
				gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

				if (shapes.Count > 0)
				{
					for (int i = 0; i < shapes.Count - 1; i++)  // Vẽ tới hình kế cuối, để hình cuối dùng đo thời gian
					{
						shapes[i].Draw(gl);
					}
                    
					// Đo thời gian vẽ hình cuối trong tập hình
					Stopwatch watch = Stopwatch.StartNew();
					shapes.Last().Draw(gl);
					watch.Stop();
					label_Time.Text = watch.Elapsed.TotalMilliseconds.ToString() + " ms";
				}

				gl.Flush();
				shapesChanged = false;
			}

            if (selectedShape >= 0)		// Vẽ tập điểm điều khiển
            {
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

		private void btn_Line_Click(object sender, EventArgs e) // Chọn vẽ đường thẳng
		{
			selectedType = Shape.shapeType.LINE;
            selectedShape = -1;
            selectedAffine = -1;
		}

		private void btn_Circle_Click(object sender, EventArgs e) // Chọn vẽ hình tròn
		{
			selectedType = Shape.shapeType.CIRCLE;
            selectedShape = -1;
            selectedAffine = -1;
		}

		private void btn_Rectangle_Click(object sender, EventArgs e) // Chọn vẽ hình chữ nhật
		{
			selectedType = Shape.shapeType.RECTANGLE;
            selectedShape = -1;
            selectedAffine = -1;
		}

		private void btn_Ellipse_Click(object sender, EventArgs e) // Chọn vẽ hình Ellip
		{
			selectedType = Shape.shapeType.ELLIPSE;
            selectedShape = -1;
            selectedAffine = -1;
		}

		private void btn_Triangle_Click(object sender, EventArgs e) // Chọn vẽ tam giác đều
		{
			selectedType = Shape.shapeType.TRIANGLE;
            selectedShape = -1;
            selectedAffine = -1;
		}

		private void btn_Pentagon_Click(object sender, EventArgs e) // Chọn vẽ hình ngũ giác 
		{
			selectedType = Shape.shapeType.PENTAGON;
            selectedShape = -1;
            selectedAffine = -1;
		}

		private void btn_Hexagon_Click(object sender, EventArgs e) // Chọn vẽ hình lục giác
		{
			selectedType = Shape.shapeType.HEXAGON;
            selectedShape = -1;
            selectedAffine = -1;
		}

		private void num_LineWidth_Change(object sender, EventArgs e) // Thay đổi nét vẽ
		{
			selectedWidth = (int)num_LineWidth.Value;
            selectedShape = -1;
            selectedAffine = -1;
		}

		private void btn_Color_Click(object sender, EventArgs e) // Chọn màu
		{
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				selectedColor = colorDialog.Color;
			}
		}

		private void mouse_Down(object sender, MouseEventArgs e)	// Lúc nhấn chuột
		{
            if (selectedType != Shape.shapeType.NONE)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {

                    pStart = pEnd = e.Location;

                    shapes.Add(new Shape(selectedType, selectedWidth, selectedColor)); // Vẽ hình theo nút đã lựa chọn
                    shapes.Last().rasterPoints.Add(pStart);

                }


                if (e.Button == System.Windows.Forms.MouseButtons.Right) // Chuột phải cho việc kết thúc vẽ đa giác 
                {
                    if (selectedType == Shape.shapeType.POLYGON) // Nếu chọn vẽ polygon - vẽ đa giác.
                    {
                        if (NewPolygon.Count > 2) // Tập điểm lớn hơn 2 thì vẽ
                        {
                            ShapeConstruction construct = new ShapeConstruction();
                            construct.ConstructLine(shapes.Last(), NewPolygon[0], NewPolygon.Last()); // vẽ đt từ điểm đầu đến cuối để hoàn thiện hình.
                            shapesChanged = true; // cập nhật vẽ.
                            NewPolygon = new List<Point>(); // tạo lại tập điểm mới.
                        }
                    }
                }


            }
            else // Hiển thị control point nếu không trong trạng thái vẽ. - Nút None là thiết lập trái thái không vẽ.
            {
               	// Nhấn chuột trái để 
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    pStart = pEnd = e.Location;
                    pLast = pStart;
                    // Tính khoảng cách min từ chuột đến hình
                    double minDistance = 1000.0;
                    double epsilon = 10.0;  // Hằng số epsilon

                    for (int i = 0; i < shapes.Count; i++)
                    {
                        for (int j = 0; j < shapes[i].rasterPoints.Count; j++)
                        {
                            int dx = shapes[i].rasterPoints[j].X - e.Location.X;
                            int dy = shapes[i].rasterPoints[j].Y - e.Location.Y;
                            double distance = Math.Sqrt(dx * dx + dy * dy);

                            if (distance < minDistance)
                            {
                                selectedShape = i; // Chọn hình hiển thị control point.
                                minDistance = distance; // Thiết lập minDistance để chọn ra hình nào gần hơn.
                            }
                        }
                    }

                    if (minDistance <= epsilon)		// Khoảng cách nhỏ hơn epsilon thì chọn hình
                    {
                        shapesChanged = true;
                        return;
                    }

                    selectedShape = -1; // nếu không gần bất cứ hình nào thì tắt hết control point.
                    shapesChanged = true;
                    return;

                }
                    
                
            }


            
		}

		private void mouse_Move(object sender, MouseEventArgs e)
		{
             ShapeConstruction construct = new ShapeConstruction();
			if (selectedType != Shape.shapeType.NONE)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    pEnd = e.Location;      // Liên tục cập nhật điểm cuối chuột

                    switch (selectedType)
                    {
                        case Shape.shapeType.NONE: // Chọn không gì cả
                            {
                                break;
                            }

                        case Shape.shapeType.LINE: // Chon vẽ đường thẳng
                            {
                                shapes.Last().rasterPoints.Clear();// Liên tục xóa để vẽ lại
                                construct.ConstructLine(shapes.Last(), pStart, pEnd);
                                break;
                            }

                        case Shape.shapeType.CIRCLE: // Chọn vẽ đường tròn
                            {
                                shapes.Last().rasterPoints.Clear();
                                construct.ConstructCircle(shapes.Last(), pStart, pEnd);
                                break;
                            }

                        case Shape.shapeType.RECTANGLE: // Chọn vẽ hình chữ nhật
                            {
                                shapes.Last().rasterPoints.Clear();
                                construct.ConstructRectangle(shapes.Last(), pStart, pEnd);
                                break;
                            }

                        case Shape.shapeType.ELLIPSE: // Chọn vẽ elip
                            {
                                shapes.Last().rasterPoints.Clear();
                                construct.ConstructEllipse(shapes.Last(), pStart, pEnd);
                                break;
                            }

                        case Shape.shapeType.TRIANGLE: // Chọn vẽ tam giác
                            {
                                shapes.Last().rasterPoints.Clear();
                                construct.ConstructTriangle(shapes.Last(), pStart, pEnd);
                                break;
                            }

                        case Shape.shapeType.PENTAGON: // Chọn vẽ ngũ giác
                            {
                                shapes.Last().rasterPoints.Clear();
                                construct.ConstructPentagon(shapes.Last(), pStart, pEnd);
                                break;
                            }

                        case Shape.shapeType.HEXAGON: // Chọn vẽ lục giác
                            {
                                shapes.Last().rasterPoints.Clear();
                                construct.ConstructHexagon(shapes.Last(), pStart, pEnd);
                                break;
                            }

                        case Shape.shapeType.POLYGON: // Chọn vẽ đa giác
                            {
                                // Vẽ các đt nối tiếp lẫn nhau.
                                if (e.Button == System.Windows.Forms.MouseButtons.Left) // click chuột và di chuyển chuột 1 ít để tạo điểm.
                                {

                                    NewPolygon.Add(pStart);
                                    if (NewPolygon.Count > 1)
                                    {
                                        construct.ConstructLine(shapes.Last(), NewPolygon[NewPolygon.Count - 2], NewPolygon.Last());

                                    }

                                }

                                break;
                            }
                    }

                }

                shapesChanged = true;

			}
            else // Nếu chọn nút None để tiến vào trạng thái không vẽ
            {
                 if(selectedShape != -1) 
                 {
                     if (selectedAffine == 1) // Nếu chọn biến đổi tịnh tiến
                     {
                         if (e.Button == System.Windows.Forms.MouseButtons.Left)
                         {
                             pEnd = e.Location;
                             Point translate_vector = new Point();
                             translate_vector.X = pEnd.X - pLast.X;
                             translate_vector.Y = pEnd.Y - pLast.Y;
                             shapes[selectedShape].Translate(translate_vector);
                             pLast = pEnd;
                             shapesChanged = true;
                         }


                     }

                     if (selectedAffine ==  2) // Nếu chọn co giãn hình
                     {
                         if (e.Button == System.Windows.Forms.MouseButtons.Left)
                         {
                             pEnd = e.Location;
                             pStart = shapes[selectedShape].inputPoint[0];
                             shapes[selectedShape].rasterPoints.Clear();
                             shapes[selectedShape].inputPoint.Clear();
                             shapes[selectedShape].ControlPoint.Clear();


                             switch (shapes[selectedShape].type)
                             {

                                 case Shape.shapeType.LINE:
                                     {

                                         construct.ConstructLine(shapes[selectedShape], pStart, pEnd);
                                         break;
                                     }

                                 case Shape.shapeType.CIRCLE:
                                     {

                                         construct.ConstructCircle(shapes[selectedShape], pStart, pEnd);
                                         break;
                                     }

                                 case Shape.shapeType.RECTANGLE:
                                     {

                                         construct.ConstructRectangle(shapes[selectedShape], pStart, pEnd);
                                         break;
                                     }

                                 case Shape.shapeType.ELLIPSE:
                                     {

                                         construct.ConstructEllipse(shapes[selectedShape], pStart, pEnd);
                                         break;
                                     }

                                 case Shape.shapeType.TRIANGLE:
                                     {

                                         construct.ConstructTriangle(shapes[selectedShape], pStart, pEnd);
                                         break;
                                     }

                                 case Shape.shapeType.PENTAGON:
                                     {

                                         construct.ConstructPentagon(shapes[selectedShape], pStart, pEnd);
                                         break;
                                     }

                                 case Shape.shapeType.HEXAGON:
                                     {
                                         construct.ConstructHexagon(shapes[selectedShape], pStart, pEnd);
                                         break;
                                     }



                             }
                             shapesChanged = true;
                         }
                     }
                     
                 }
            }

          
        }

        private void btn_Point_conected_Click(object sender, EventArgs e) //nút vẽ đa giác.
        {
            selectedType = Shape.shapeType.POLYGON;
        }

        private void btn_clear_Click(object sender, EventArgs e) // xóa toàn bộ hình, thiết lập trạng thái mặc định.
        {
            shapes.Clear();
            NewPolygon = new List<Point>();
            selectedShape = -1;
            selectedAffine = -1;
            shapesChanged = true;

        }

        private void btn_None_Click(object sender, EventArgs e) // Trạng thái không vẽ gì cả. 
        {
            selectedType = Shape.shapeType.NONE;
            selectedAffine = -1;
            
        }

        private void btn_Translate_Click(object sender, EventArgs e) // Chọn tịnh tiến
        {
            selectedAffine = 1; // Tịnh tiến.
        }

        private void btn_Scale_Click(object sender, EventArgs e) // Chọn co giãn 
        {
            selectedAffine = 2; // Scale
        }

	}
}
