﻿using UnityEngine;

namespace Noobik_Thaumcraft.Extensions
{
    public static class ColorExtensions
    {
        public static Color SetAlpha(this Color color, float alpha) =>
            new Color(color.r, color.g, color.b, alpha);
    }
}