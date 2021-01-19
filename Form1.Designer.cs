namespace _1712400_BT1
{
  partial class Form1
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.openGLControl = new SharpGL.OpenGLControl();
      this.btn_Line = new System.Windows.Forms.Button();
      this.btn_Circle = new System.Windows.Forms.Button();
      this.btn_Rectangle = new System.Windows.Forms.Button();
      this.btn_Ellipse = new System.Windows.Forms.Button();
      this.btn_Triangle = new System.Windows.Forms.Button();
      this.btn_Pentagon = new System.Windows.Forms.Button();
      this.btn_Hexagon = new System.Windows.Forms.Button();
      this.num_LineWidth = new System.Windows.Forms.NumericUpDown();
      this.label1 = new System.Windows.Forms.Label();
      this.colorDialog = new System.Windows.Forms.ColorDialog();
      this.btn_Color = new System.Windows.Forms.Button();
      this.label_Time = new System.Windows.Forms.Label();
      this.btn_Polygon = new System.Windows.Forms.Button();
      this.btn_clear = new System.Windows.Forms.Button();
      this.btn_Coloring_Floodfill = new System.Windows.Forms.Button();
      this.btn_BoundaryFill = new System.Windows.Forms.Button();
      this.btn_None = new System.Windows.Forms.Button();
      this.btn_Translate = new System.Windows.Forms.Button();
      this.btn_Scale = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.num_LineWidth)).BeginInit();
      this.SuspendLayout();
      // 
      // openGLControl
      // 
      this.openGLControl.DrawFPS = false;
      this.openGLControl.Location = new System.Drawing.Point(12, 79);
      this.openGLControl.Name = "openGLControl";
      this.openGLControl.RenderContextType = SharpGL.RenderContextType.DIBSection;
      this.openGLControl.RenderTrigger = SharpGL.RenderTrigger.TimerBased;
      this.openGLControl.Size = new System.Drawing.Size(893, 358);
      this.openGLControl.TabIndex = 1;
      this.openGLControl.OpenGLInitialized += new System.EventHandler(this.openGLControl_OpenGLInitialized);
      this.openGLControl.OpenGLDraw += new SharpGL.RenderEventHandler(this.openGLControl_OpenGLDraw);
      this.openGLControl.Resized += new System.EventHandler(this.openGLControl_Resized);
      this.openGLControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mouse_Down);
      this.openGLControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouse_Move);
      // 
      // btn_Line
      // 
      this.btn_Line.Location = new System.Drawing.Point(10, 14);
      this.btn_Line.Margin = new System.Windows.Forms.Padding(2);
      this.btn_Line.Name = "btn_Line";
      this.btn_Line.Size = new System.Drawing.Size(65, 28);
      this.btn_Line.TabIndex = 2;
      this.btn_Line.Text = "Line";
      this.btn_Line.UseVisualStyleBackColor = true;
      this.btn_Line.Click += new System.EventHandler(this.btn_Line_Click);
      // 
      // btn_Circle
      // 
      this.btn_Circle.Location = new System.Drawing.Point(79, 14);
      this.btn_Circle.Margin = new System.Windows.Forms.Padding(2);
      this.btn_Circle.Name = "btn_Circle";
      this.btn_Circle.Size = new System.Drawing.Size(64, 28);
      this.btn_Circle.TabIndex = 3;
      this.btn_Circle.Text = "Circle";
      this.btn_Circle.UseVisualStyleBackColor = true;
      this.btn_Circle.Click += new System.EventHandler(this.btn_Circle_Click);
      // 
      // btn_Rectangle
      // 
      this.btn_Rectangle.Location = new System.Drawing.Point(148, 14);
      this.btn_Rectangle.Margin = new System.Windows.Forms.Padding(2);
      this.btn_Rectangle.Name = "btn_Rectangle";
      this.btn_Rectangle.Size = new System.Drawing.Size(65, 28);
      this.btn_Rectangle.TabIndex = 4;
      this.btn_Rectangle.Text = "Rectangle";
      this.btn_Rectangle.UseVisualStyleBackColor = true;
      this.btn_Rectangle.Click += new System.EventHandler(this.btn_Rectangle_Click);
      // 
      // btn_Ellipse
      // 
      this.btn_Ellipse.Location = new System.Drawing.Point(218, 14);
      this.btn_Ellipse.Margin = new System.Windows.Forms.Padding(2);
      this.btn_Ellipse.Name = "btn_Ellipse";
      this.btn_Ellipse.Size = new System.Drawing.Size(62, 28);
      this.btn_Ellipse.TabIndex = 5;
      this.btn_Ellipse.Text = "Ellipse";
      this.btn_Ellipse.UseVisualStyleBackColor = true;
      this.btn_Ellipse.Click += new System.EventHandler(this.btn_Ellipse_Click);
      // 
      // btn_Triangle
      // 
      this.btn_Triangle.Location = new System.Drawing.Point(284, 14);
      this.btn_Triangle.Margin = new System.Windows.Forms.Padding(2);
      this.btn_Triangle.Name = "btn_Triangle";
      this.btn_Triangle.Size = new System.Drawing.Size(64, 28);
      this.btn_Triangle.TabIndex = 6;
      this.btn_Triangle.Text = "Triangle";
      this.btn_Triangle.UseVisualStyleBackColor = true;
      this.btn_Triangle.Click += new System.EventHandler(this.btn_Triangle_Click);
      // 
      // btn_Pentagon
      // 
      this.btn_Pentagon.Location = new System.Drawing.Point(352, 14);
      this.btn_Pentagon.Margin = new System.Windows.Forms.Padding(2);
      this.btn_Pentagon.Name = "btn_Pentagon";
      this.btn_Pentagon.Size = new System.Drawing.Size(64, 28);
      this.btn_Pentagon.TabIndex = 7;
      this.btn_Pentagon.Text = "Pentagon";
      this.btn_Pentagon.UseVisualStyleBackColor = true;
      this.btn_Pentagon.Click += new System.EventHandler(this.btn_Pentagon_Click);
      // 
      // btn_Hexagon
      // 
      this.btn_Hexagon.Location = new System.Drawing.Point(422, 14);
      this.btn_Hexagon.Margin = new System.Windows.Forms.Padding(2);
      this.btn_Hexagon.Name = "btn_Hexagon";
      this.btn_Hexagon.Size = new System.Drawing.Size(64, 28);
      this.btn_Hexagon.TabIndex = 8;
      this.btn_Hexagon.Text = "Hexagon";
      this.btn_Hexagon.UseVisualStyleBackColor = true;
      this.btn_Hexagon.Click += new System.EventHandler(this.btn_Hexagon_Click);
      // 
      // num_LineWidth
      // 
      this.num_LineWidth.Location = new System.Drawing.Point(755, 22);
      this.num_LineWidth.Margin = new System.Windows.Forms.Padding(2);
      this.num_LineWidth.Maximum = new decimal(new int[] {
      5,
      0,
      0,
      0});
      this.num_LineWidth.Minimum = new decimal(new int[] {
      1,
      0,
      0,
      0});
      this.num_LineWidth.Name = "num_LineWidth";
      this.num_LineWidth.Size = new System.Drawing.Size(56, 20);
      this.num_LineWidth.TabIndex = 9;
      this.num_LineWidth.Value = new decimal(new int[] {
      1,
      0,
      0,
      0});
      this.num_LineWidth.ValueChanged += new System.EventHandler(this.num_LineWidth_Change);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(753, 7);
      this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(58, 13);
      this.label1.TabIndex = 10;
      this.label1.Text = "Line Width";
      // 
      // colorDialog
      // 
      this.colorDialog.Color = System.Drawing.Color.White;
      this.colorDialog.FullOpen = true;
      // 
      // btn_Color
      // 
      this.btn_Color.Location = new System.Drawing.Point(815, 19);
      this.btn_Color.Margin = new System.Windows.Forms.Padding(2);
      this.btn_Color.Name = "btn_Color";
      this.btn_Color.Size = new System.Drawing.Size(56, 19);
      this.btn_Color.TabIndex = 11;
      this.btn_Color.Text = "Color";
      this.btn_Color.UseVisualStyleBackColor = true;
      this.btn_Color.Click += new System.EventHandler(this.btn_Color_Click);
      // 
      // label_Time
      // 
      this.label_Time.AutoSize = true;
      this.label_Time.Location = new System.Drawing.Point(875, 19);
      this.label_Time.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.label_Time.Name = "label_Time";
      this.label_Time.Size = new System.Drawing.Size(29, 13);
      this.label_Time.TabIndex = 12;
      this.label_Time.Text = "0 ms";
      // 
      // btn_Polygon
      // 
      this.btn_Polygon.Location = new System.Drawing.Point(491, 15);
      this.btn_Polygon.Name = "btn_Polygon";
      this.btn_Polygon.Size = new System.Drawing.Size(91, 27);
      this.btn_Polygon.TabIndex = 13;
      this.btn_Polygon.Text = "Polygon";
      this.btn_Polygon.UseVisualStyleBackColor = true;
      this.btn_Polygon.Click += new System.EventHandler(this.btn_Point_conected_Click);
      // 
      // btn_clear
      // 
      this.btn_clear.Location = new System.Drawing.Point(588, 17);
      this.btn_clear.Name = "btn_clear";
      this.btn_clear.Size = new System.Drawing.Size(75, 23);
      this.btn_clear.TabIndex = 14;
      this.btn_clear.Text = "clear";
      this.btn_clear.UseVisualStyleBackColor = true;
      this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
      // 
      // btn_Coloring_Floodfill
      // 
      this.btn_Coloring_Floodfill.Location = new System.Drawing.Point(10, 50);
      this.btn_Coloring_Floodfill.Name = "btn_Coloring_Floodfill";
      this.btn_Coloring_Floodfill.Size = new System.Drawing.Size(133, 23);
      this.btn_Coloring_Floodfill.TabIndex = 15;
      this.btn_Coloring_Floodfill.Text = "Coloring_Floodfill";
      this.btn_Coloring_Floodfill.UseVisualStyleBackColor = true;
      // 
      // btn_BoundaryFill
      // 
      this.btn_BoundaryFill.Location = new System.Drawing.Point(150, 50);
      this.btn_BoundaryFill.Name = "btn_BoundaryFill";
      this.btn_BoundaryFill.Size = new System.Drawing.Size(114, 23);
      this.btn_BoundaryFill.TabIndex = 16;
      this.btn_BoundaryFill.Text = "Coloring_Boundaryfill";
      this.btn_BoundaryFill.UseVisualStyleBackColor = true;
      // 
      // btn_None
      // 
      this.btn_None.Location = new System.Drawing.Point(588, 47);
      this.btn_None.Name = "btn_None";
      this.btn_None.Size = new System.Drawing.Size(75, 23);
      this.btn_None.TabIndex = 17;
      this.btn_None.Text = "None";
      this.btn_None.UseVisualStyleBackColor = true;
      this.btn_None.Click += new System.EventHandler(this.btn_None_Click);
      // 
      // btn_Translate
      // 
      this.btn_Translate.Location = new System.Drawing.Point(284, 46);
      this.btn_Translate.Name = "btn_Translate";
      this.btn_Translate.Size = new System.Drawing.Size(75, 23);
      this.btn_Translate.TabIndex = 18;
      this.btn_Translate.Text = "Translate";
      this.btn_Translate.UseVisualStyleBackColor = true;
      this.btn_Translate.Click += new System.EventHandler(this.btn_Translate_Click);
      // 
      // btn_Scale
      // 
      this.btn_Scale.Location = new System.Drawing.Point(365, 46);
      this.btn_Scale.Name = "btn_Scale";
      this.btn_Scale.Size = new System.Drawing.Size(75, 23);
      this.btn_Scale.TabIndex = 19;
      this.btn_Scale.Text = "Scale";
      this.btn_Scale.UseVisualStyleBackColor = true;
      this.btn_Scale.Click += new System.EventHandler(this.btn_Scale_Click);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(915, 437);
      this.Controls.Add(this.btn_Scale);
      this.Controls.Add(this.btn_Translate);
      this.Controls.Add(this.btn_None);
      this.Controls.Add(this.btn_BoundaryFill);
      this.Controls.Add(this.btn_Coloring_Floodfill);
      this.Controls.Add(this.btn_clear);
      this.Controls.Add(this.btn_Polygon);
      this.Controls.Add(this.label_Time);
      this.Controls.Add(this.btn_Color);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.num_LineWidth);
      this.Controls.Add(this.btn_Hexagon);
      this.Controls.Add(this.btn_Pentagon);
      this.Controls.Add(this.btn_Triangle);
      this.Controls.Add(this.btn_Ellipse);
      this.Controls.Add(this.btn_Rectangle);
      this.Controls.Add(this.btn_Circle);
      this.Controls.Add(this.btn_Line);
      this.Controls.Add(this.openGLControl);
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "Form1";
      this.Text = "Form1";
      ((System.ComponentModel.ISupportInitialize)(this.openGLControl)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.num_LineWidth)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion
    private SharpGL.OpenGLControl openGLControl;
    private System.Windows.Forms.Button btn_Line;
    private System.Windows.Forms.Button btn_Circle;
    private System.Windows.Forms.Button btn_Rectangle;
    private System.Windows.Forms.Button btn_Ellipse;
    private System.Windows.Forms.Button btn_Triangle;
    private System.Windows.Forms.Button btn_Pentagon;
    private System.Windows.Forms.Button btn_Hexagon;
    private System.Windows.Forms.NumericUpDown num_LineWidth;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.ColorDialog colorDialog;
    private System.Windows.Forms.Button btn_Color;
    private System.Windows.Forms.Label label_Time;
    private System.Windows.Forms.Button btn_Polygon;
    private System.Windows.Forms.Button btn_clear;
    private System.Windows.Forms.Button btn_Coloring_Floodfill;
    private System.Windows.Forms.Button btn_BoundaryFill;
    private System.Windows.Forms.Button btn_None;
    private System.Windows.Forms.Button btn_Translate;
    private System.Windows.Forms.Button btn_Scale;
  }
}

