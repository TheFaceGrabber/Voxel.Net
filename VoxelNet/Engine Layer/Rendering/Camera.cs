﻿using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using VoxelNet.Buffers;

namespace VoxelNet.Rendering
{
    public struct CameraUniformBuffer
    {
        public Matrix4 ProjectionMat;
        public Matrix4 ViewMat;
        public Vector4 Position;
    }

    public enum CameraProjectionType
    {
        Perspective,
        Orthographic
    }

    public class Camera
    {
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }

        public CameraProjectionType ProjectionType { get; set; } = CameraProjectionType.Perspective;

        public Matrix4 ViewMatrix { get; private set; }
        public Matrix4 ProjectionMatrix { get; private set; }

        /// <summary>
        /// This is only used when the camera is Orthographic
        /// </summary>
        public Vector2 CameraSize { get; set; } = Vector2.One;

        public float NearPlane { get; } = 0.1f;
        public float FarPlane { get; } = 900f;

        public Frustum Frustum { get; } = new Frustum(Matrix4.Identity);

        private CameraUniformBuffer bufferData = new CameraUniformBuffer();

        public void Update()
        {
            ViewMatrix = Matrix4.LookAt(Position, Position + GetForward(), GetUp());
            switch (ProjectionType)
            {
                case CameraProjectionType.Perspective:
                    ProjectionMatrix = Matrix4.CreatePerspectiveFieldOfView(
                        MathHelper.DegreesToRadians(Program.Settings.FieldOfView),
                        (float)Window.WindowWidth / (float)Window.WindowHeight, NearPlane, FarPlane);
                    break;
                case CameraProjectionType.Orthographic:
                    ProjectionMatrix = Matrix4.CreateOrthographic(CameraSize.X,
                        CameraSize.Y, NearPlane, FarPlane);
                    break;
            }

            Frustum.UpdateMatrix(ViewMatrix * ProjectionMatrix);

            bufferData.ProjectionMat = ProjectionMatrix;
            bufferData.ViewMat = ViewMatrix;
            bufferData.Position = new Vector4(Position, 1);

            UniformBuffers.WorldCameraBuffer.Update(bufferData);
        }

        public Vector3 GetForward()
        {
            float yaw = MathHelper.DegreesToRadians(Rotation.Y + 90);
            float pitch = MathHelper.DegreesToRadians(Rotation.X);

            float x = (float) (Math.Cos(yaw) * Math.Cos(pitch));
            float y = (float)Math.Sin(pitch);
            float z = (float)(Math.Cos(pitch) * Math.Sin(yaw));

            return new Vector3(-x, -y, -z).Normalized();
        }

        public Vector3 GetRight()
        {
            float yaw = MathHelper.DegreesToRadians(Rotation.Y);

            float x = (float)Math.Cos(yaw);
            float z = (float)Math.Sin(yaw);

            return new Vector3(x, 0, z).Normalized();
        }

        public Vector3 GetUp()
        {
            float pitch = MathHelper.DegreesToRadians(Rotation.X + 90);

            float y = (float)Math.Sin(pitch);

            return new Vector3(0, y, 0).Normalized();
        }
    }
}
