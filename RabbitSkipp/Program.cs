using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace RabbitSkipp
{
    class Program
    {
        private static List<int> bars = new List<int>() { 2, 3, 5, 4, 6, 3 };
        private static int depth = 0;
        private static int maxDepth=0;
        private static Stack<int> posStack;
        private static List<int> longestStack;
        static void Main(string[] args)
        {
            posStack = new Stack<int>();
            longestStack = new List<int>();
            depth = 0;
            maxDepth = 0;
            List<int> longestRoute = new List<int>();

            for (int startPosition = 0; startPosition < GetNoBars(); startPosition++)
            {
                GetChildren(startPosition);
                if(longestStack.Count()>longestRoute.Count())
                {
                    longestRoute = new List<int>();

                    longestStack.ForEach(l=>longestRoute.Add(l));
                }
            }
            Console.WriteLine("the best route:");
            longestRoute.Reverse();
            longestRoute.ForEach(l => Console.WriteLine(l));            
        }

        private static void GetChildren(int position)
        {
            posStack.Push(position);
            depth++;
            //Console.WriteLine("depth: " + depth);
            //Console.WriteLine(position);

            if(depth>maxDepth)
            {
                maxDepth = depth;
                longestStack = new List<int>();
                Console.WriteLine("stack");
                foreach (var pos in posStack)
                {
                    longestStack.Add(pos);
                    Console.WriteLine(pos.ToString());
                }                
                Console.WriteLine("-----");
            }

            List<int> childPositions = new List<int>() { position - 2, position - 1, position + 1, position + 2 };

            int len = GetNoBars();
            var children= childPositions.Where(c => (c >= 0) && (c < len) && (GetBarHeight(c) < GetBarHeight(position))).ToList();

            //children.ForEach(c => Console.Write(c.ToString() + " "));
            //Console.WriteLine();
            foreach (int c in children)
            {
                GetChildren(c);
            }
            depth--;
            posStack.Pop();
        }

        private static int GetBarHeight(int index)
        {
            return bars[index];
        }
        private static int GetNoBars()
        {
            return bars.Count;
        }

    }
}
