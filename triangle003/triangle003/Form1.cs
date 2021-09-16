using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;

namespace triangle003
{
    public partial class Form1 : Form
    {
        private Device device = null;
        private VertexBuffer vb = null;
        private IndexBuffer ib = null;

        private static int terWidth = 64;
        private static int terLenth = 64;
        private float moveSpeed = 0.2f;
        private float turnSpeed = 0.02f;
        private float rotY = 0;
        private float tempY = 0;

        private const float raiseConst = 0.05f;

        private float rotXZ = 0;
        private float tempXZ = 0;

        private static int vertCount = terLenth * terWidth;
        private static int indCount = (terWidth - 1) * (terLenth - 1) * 6;

        private Vector3 camPosition, camLookat, camUp;

        CustomVertex.PositionColored[] verts = null;

        bool isMiddleMouseDown = false;
        bool isLeftMouseDown = false;

        private static int[] indices = null;

        private FillMode fillMode = FillMode.WireFrame;
        private Color backgroundColor = Color.White;

        private bool invalidating = true;
        private Bitmap heightmap = null;

        public Form1()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            InitializeComponent();
            InitializeGraphics();

            InitializeEventHandler();
        }

        private void InitializeGraphics()
        {
            PresentParameters pp = new PresentParameters();
            pp.Windowed = true;
            pp.SwapEffect = SwapEffect.Discard;

            pp.EnableAutoDepthStencil = true;
            pp.AutoDepthStencilFormat = DepthFormat.D16;

            device = new Device(0, DeviceType.Hardware, this, CreateFlags.HardwareVertexProcessing, pp);


            GenerateVertex();
            GenerateIndex();

            vb = new VertexBuffer(typeof(CustomVertex.PositionColored), vertCount, device, Usage.Dynamic | Usage.WriteOnly, CustomVertex.PositionColored.Format, Pool.Default);
            OnVertexBufferCreate(vb, null);



            ib = new IndexBuffer(typeof(int), indCount, device, Usage.WriteOnly, Pool.Default);
            OnIndexBufferCreate(ib, null);

            //Camera position initialization
            camPosition = new Vector3(2, 4.5f, -3.5f);
            camLookat = new Vector3(2, 3.5f, -2.5f);
            camUp = new Vector3(0, 1, 0);

        }

        private void InitializeEventHandler()
        {
            vb.Created += new EventHandler(OnVertexBufferCreate);
            ib.Created += new EventHandler(OnIndexBufferCreate);

            this.KeyDown += new KeyEventHandler(OnKeyDown);
            this.MouseWheel += new MouseEventHandler(OnMouseScroll);

            this.MouseDown += new MouseEventHandler(OnMouseDown);
            this.MouseUp += new MouseEventHandler(OnMouseUp);
            this.MouseMove += new MouseEventHandler(OnMouseMove);


        }
        private void OnIndexBufferCreate(object sender, EventArgs e)
        {
            IndexBuffer buffer = (IndexBuffer)sender;

            buffer.SetData(indices, 0, LockFlags.None);

        }
        private void OnVertexBufferCreate(object sender, EventArgs e)
        {
            VertexBuffer buffer = (VertexBuffer)sender;

            buffer.SetData(verts, 0, LockFlags.None);

        }

        private void SetupCamera()
        {

            camLookat.X = (float)Math.Sin(rotY) + camPosition.X + (float)(Math.Sin(rotXZ)*Math.Sin(rotY));
            camLookat.Y = (float)Math.Sin(rotXZ)+camPosition.Y;
            camLookat.Z = (float)Math.Cos(rotY) + camPosition.Z+(float)(Math.Sin(rotXZ)*Math.Cos(rotY));

            device.Transform.Projection = Matrix.PerspectiveFovLH((float)Math.PI / 4, this.Width / this.Height, 1.0f, 1000.0f);
            device.Transform.View = Matrix.LookAtLH(camPosition, camLookat, camUp);

            device.RenderState.Lighting = false;
            device.RenderState.CullMode = Cull.CounterClockwise;
            device.RenderState.FillMode = fillMode;
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            device.Clear(ClearFlags.Target | ClearFlags.ZBuffer, backgroundColor, 1, 0);

            SetupCamera();

            device.BeginScene();

            device.VertexFormat = CustomVertex.PositionColored.Format;

            device.SetStreamSource(0, vb, 0);
            device.Indices = ib;

            device.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertCount, 0, indCount / 3);
            device.EndScene();

            device.Present();

            menuStrip1.Update();
            
            if (invalidating)
            {
                this.Invalidate();
            }
            
        }


        private void GenerateVertex()
        {
            verts = new CustomVertex.PositionColored[vertCount];

            int k = 0;

            for (int z = 0; z < terWidth; z++)
            {
                for (int x = 0; x < terLenth; x++)
                {
                    verts[k].Position = new Vector3(x, 0, z);
                    verts[k].Color = Color.Black.ToArgb();
                    k++;
                }
            }
        }

        private void GenerateIndex()
        {
            indices = new int[indCount];
            int k = 0;
            int l = 0;

            for (int i = 0; i < indCount; i += 6)
            {
                indices[i] = k;
                indices[i + 1] = k + terLenth;
                indices[i + 2] = k + terLenth + 1;
                indices[i + 3] = k;
                indices[i + 4] = k + terLenth + 1;
                indices[i + 5] = k + 1;
                k++;
                l++;
                if (l == terLenth - 1)
                {
                    l = 0;
                    k++;
                }
            }


        }

        private void LoadHeightmap()
        {
            verts = new CustomVertex.PositionColored[vertCount];

            int k = 0;

            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Load heightmap";
                ofd.Filter = "Bitmap files(*.bmp)|*.bmp";
                ofd.InitialDirectory = Application.StartupPath;
                if (ofd.ShowDialog(this) == DialogResult.OK)
                {
                    heightmap = new Bitmap(ofd.FileName);
                    Color pixelCol;

                    for (int z = 0; z < terWidth; z++)
                    {
                        for (int x = 0; x < terLenth; x++)
                        {
                            if (heightmap.Size.Width > x && heightmap.Size.Height > z)
                            {
                                pixelCol = heightmap.GetPixel(x, z);
                                verts[k].Position = new Vector3(x, (float)pixelCol.B / 15 - 10, z);
                                verts[k].Color = pixelCol.ToArgb();
                            }
                            else
                            {
                                verts[k].Position = new Vector3(x, 0, z);
                                verts[k].Color = Color.Black.ToArgb();
                            }
                           
                            k++;
                        }
                    }
                }
            }

            
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case (Keys.W):
                    {
                        camPosition.X += moveSpeed * (float)Math.Sin(rotY);
                        camPosition.Z += moveSpeed * (float)Math.Cos(rotY);
                        break;
                    }
                case (Keys.D):
                    {
                        camPosition.X += moveSpeed * (float)Math.Sin(rotY + Math.PI / 2);
                        camPosition.Z += moveSpeed * (float)Math.Cos(rotY + Math.PI / 2);
                        break;
                    }
                case (Keys.S):
                    {
                        camPosition.X -= moveSpeed * (float)Math.Sin(rotY);
                        camPosition.Z -= moveSpeed * (float)Math.Cos(rotY);
                        break;
                    }
                case (Keys.A):
                    {
                        camPosition.X -= moveSpeed * (float)Math.Sin(rotY + Math.PI / 2);
                        camPosition.Z -= moveSpeed * (float)Math.Cos(rotY + Math.PI / 2);
                        break;
                    }
                case (Keys.Q):
                    {
                        rotY -= turnSpeed;
                        break;
                    }
                case (Keys.E):
                    {
                        rotY += turnSpeed;
                        break;
                    }
                case (Keys.Up):
                    {
                        if (rotXZ < Math.PI / 2)
                        {
                            rotXZ += turnSpeed;
                        }
                        break;
                    }
                case (Keys.Down):
                    {
                        if (rotXZ > -Math.PI / 2)
                        {


                            rotXZ -= turnSpeed;
                        }
                        break;
                    }
            }
        }
        private void OnMouseScroll(object sender, MouseEventArgs e)
        {
            camPosition.Y += e.Delta * 0.001f;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (isMiddleMouseDown)
            { 
                rotY = tempY+ e.X * turnSpeed;
                float tmp= tempXZ - e.Y * turnSpeed / 4;
                if (tmp > -Math.PI / 2 && tmp < Math.PI / 2)
                    rotXZ = tmp;
            }
            if (isLeftMouseDown)
            {
                Point mouseMoveLocation = new Point(e.X, e.Y);
                PickingTriangle(mouseMoveLocation);
            }
        }
        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case (MouseButtons.Middle):
                    {
                        tempY = rotY- e.X * turnSpeed;
                        tempXZ = rotXZ + e.Y * turnSpeed / 4;
                       
                        isMiddleMouseDown = true;
                        break;
                    }
                case (MouseButtons.Left):
                    {
                        isLeftMouseDown = true;

                        Point mouseDownLocation = new Point(e.X, e.Y);
                        PickingTriangle(mouseDownLocation);
                        break;
                    }
            }
        }

        private void PickingTriangle(Point mouseLocation)
        {
            IntersectInformation hitLocation;
            Vector3 near, far, direction;

            near = new Vector3(mouseLocation.X, mouseLocation.Y, 0);
            far = new Vector3(mouseLocation.X, mouseLocation.Y, 100);

            near.Unproject(device.Viewport, device.Transform.Projection, device.Transform.View, device.Transform.World);
            far.Unproject(device.Viewport, device.Transform.Projection, device.Transform.View, device.Transform.World);

            direction = near - far;


            for (int i = 0; i < indCount; i += 3)
            {
                if(Geometry.IntersectTri(verts[indices[i]].Position, verts[indices[i + 1]].Position, verts[indices[i + 2]].Position,near,direction,out hitLocation))
                {
                    verts[indices[i]].Color = Color.Red.ToArgb();
                    verts[indices[i+1]].Color = Color.Red.ToArgb();
                    verts[indices[i+2]].Color = Color.Red.ToArgb();

                    verts[indices[i]].Position += new Vector3(0, raiseConst, 0);
                    verts[indices[i+1]].Position += new Vector3(0, raiseConst, 0);
                    verts[indices[i+2]].Position += new Vector3(0, raiseConst, 0);

                    vb.SetData(verts, 0, LockFlags.None);
                }
            }
        }
        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case (MouseButtons.Middle):
                    {
                        isMiddleMouseDown = false;
                        break;
                    }
                case (MouseButtons.Left):
                    {
                        isLeftMouseDown = false;
                        break;
                    }
            }
        }

      
        private void solidToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            fillMode = FillMode.Solid;
            solidToolStripMenuItem.Checked = true;
            wireframeToolStripMenuItem.Checked = false;
            pointToolStripMenuItem.Checked = false;
        }

        private void backgroundColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            invalidating = false;

            if(cd.ShowDialog(this) == DialogResult.OK)
            {
                backgroundColor = cd.Color;
            }
            invalidating = true;
            this.Invalidate();
        }

        private void heightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadHeightmap();
            vb.SetData(verts, 0, LockFlags.None);
        }

        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateVertex();
            vb.SetData(verts, 0, LockFlags.None);
        }

        private void pointToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            fillMode = FillMode.Point;
            solidToolStripMenuItem.Checked = false;
            wireframeToolStripMenuItem.Checked = false;
            pointToolStripMenuItem.Checked = true;
        }

        private void wireframeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            fillMode = FillMode.WireFrame;
            solidToolStripMenuItem.Checked = false;
            wireframeToolStripMenuItem.Checked = true;
            pointToolStripMenuItem.Checked = false;
        }

       
       
    }
}
