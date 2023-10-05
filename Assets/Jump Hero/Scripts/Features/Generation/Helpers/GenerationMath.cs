using Leopotam.EcsLite;
using System.ComponentModel;
using System.Runtime;
using UnityEngine;

namespace GenerationAssembly
{
#if ENABLE_IL2CPP
        using Unity.IL2CPP.CompilerServices;

        [Il2CppSetOption(Option.NullChecks, false)]
        [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
        [Il2CppSetOption(Option.DivideByZeroChecks, false)]
#endif

    internal static class GenerationMath
    {
        public delegate void DoSomething(Vector2 setupPosition);
        /// <summary>
        /// Iterate over setups positions in generation rectangle
        /// </summary>
        /// <param name="generationStart"></param>
        /// <param name="generationEnd"></param>
        /// <param name="setupSize"></param>
        /// <param name="doSomething"></param>
        public static void ForEachSetupPosition(GenerationRectangle generationRectangle, Vector2Int setupSize, DoSomething doSomething)
        {
            for (float x = generationRectangle.LeftDownCorner.x; x <= generationRectangle.RightUpCorner.x; x += setupSize.x)
                for (float y = generationRectangle.LeftDownCorner.y; y <= generationRectangle.RightUpCorner.y; y += setupSize.y)
                    doSomething(GetSetupPoint(new Vector2(x, y), setupSize));
        }
        public static Vector2 GetSetupPoint(Vector2 insidePosition, Vector2Int setupSize)
        {
            //just clamp our coordinates
            float x = insidePosition.x - insidePosition.x % setupSize.x;
            float y = insidePosition.y - insidePosition.y % setupSize.y;

            return new Vector2(x, y);
        }
    }
}

