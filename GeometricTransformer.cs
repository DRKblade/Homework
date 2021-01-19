using SharpGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace _1712400_BT1
{


    class AffineTransform
    {
        List<float> affineMatrix;	// Ma trận biến đổi affine
        public List<float> result; // Ma trận 3x1 tọa độ mới
        public AffineTransform()
        {
            // Khởi tạo bằng ma trận đơn vị
            affineMatrix = new List<float> {	1, 0, 0, 
												0, 1, 0, 
												0, 0, 1		};
            result = new List<float> { 0, 0, 1 };
        }

        public void MultiplyMatrix(List<float> inputMatrix, Point input_point)   // Hàm nhân ma trận input với ma trận biến đổi affine
        {

            for (int i = 0; i < 3; i++)
            {

                result[i] = inputMatrix[i * 3 + 0] * input_point.X + inputMatrix[i * 3 + 1] * input_point.Y + inputMatrix[i * 3 + 2] * 1;

            }

        }

        public void Translate(float tx, float ty, Point input_Point)		// Phép tịnh tiến
        {
            // Ma trận phép tịnh tiến
            List<float> translateMatrix = new List<float> {	1, 0, tx, 
															0, 1, ty, 
															0, 0, 1	};
            MultiplyMatrix(translateMatrix, input_Point);
        }

        public void Scale(float sx, float sy, Point input_Point)		// Phép co giãn
        {
            // Ma trận phép co giãn
            List<float> scaleMatrix = new List<float> {	sx, 0, 0,
														0, sy, 0, 
														0, 0, 1	};
            MultiplyMatrix(scaleMatrix, input_Point);
        }

        public void Rotate(float theta, Point input_Point)		// Phép quay
        {
            float cosTheta = (float)Math.Cos(theta * Math.PI / 180), sinTheta = (float)Math.Sin(theta * Math.PI / 180);		// Góc quay

            // Ma trận phép quay
            List<float> rotateMatrix = new List<float> {    cosTheta, -sinTheta, 0,
															sinTheta, cosTheta, 0, 
															0,			0,		1	};
            MultiplyMatrix(rotateMatrix, input_Point);

        }
    }


    
}
