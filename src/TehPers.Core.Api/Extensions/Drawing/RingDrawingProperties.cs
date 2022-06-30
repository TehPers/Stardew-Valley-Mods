﻿using Microsoft.Xna.Framework;

namespace TehPers.Core.Api.Extensions.Drawing
{
    public record RingDrawingProperties : IDrawingProperties
    {
        public Vector2 SourceSize => new(16f, 16f);

        public Vector2 Offset(float scaleSize)
        {
            return new(32f * scaleSize, 32f * scaleSize);
        }

        public Vector2 Origin(float scaleSize)
        {
            return new(8f * scaleSize, 8f * scaleSize);
        }

        public float RealScale(float scaleSize)
        {
            return 4f * scaleSize;
        }
    }
}