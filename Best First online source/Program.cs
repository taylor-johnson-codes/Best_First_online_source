// Code source: https://www.geeksforgeeks.org/best-first-search-informed-search/
// I updated some formatting and variable names

namespace Best_First_online_source
{
    internal class Program
    {
        public static LinkedList<Tuple<int, int>>[] graph;

        // Function for adding edges to graph
        public static void addedge(int x, int y, int cost)
        {
            graph[x].AddLast(new Tuple<int, int>(cost, y));
            graph[y].AddLast(new Tuple<int, int>(cost, x));
        }

        // Function for finding the minimum weight element.
        public static Tuple<int, int> get_min(LinkedList<Tuple<int, int>> priorityQ)
        {
            // Assuming the maximum wt can be of 1e5.
            Tuple<int, int> current_min = new Tuple<int, int>(100000, 100000);
            foreach (var ele in priorityQ)
            {
                if (ele.Item1 == current_min.Item1)
                {
                    if (ele.Item2 < current_min.Item2)
                    {
                        current_min = ele;
                    }
                }
                else
                {
                    if (ele.Item1 < current_min.Item1)
                    {
                        current_min = ele;
                    }
                }
            }

            return current_min;
        }

        // Function for implementing Best First Search (gives output path having lowest cost)
        public static void best_first_search(int actual_Src, int target, int n)
        {
            int[] visited = new int[n];
            for (int i = 0; i < n; i++)
                visited[i] = 0;

            // MIN HEAP priority queue
            LinkedList<Tuple<int, int>> priorityQ = new LinkedList<Tuple<int, int>>();

            // sorting in priorityQ gets done by first value of pair
            priorityQ.AddLast(new Tuple<int, int>(0, actual_Src));
            int s = actual_Src;
            visited[s] = 1;

            Console.WriteLine("Lowest cost path from source to goal: ");
            while (priorityQ.Count > 0)
            {
                Tuple<int, int> current_min = get_min(priorityQ);
                int x = current_min.Item2;
                priorityQ.Remove(current_min);

                // Displaying the path having lowest cost
                Console.Write(x + " ");

                if (x == target)
                    break;

                LinkedList<Tuple<int, int>> list = graph[x];
                foreach (var value in list)
                {
                    if (visited[value.Item2] == 0)
                    {
                        visited[value.Item2] = 1;
                        priorityQ.AddLast(new Tuple<int, int>(value.Item1, value.Item2));
                    }
                }
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int numberOfNodes = 14;

            graph = new LinkedList<Tuple<int, int>>[numberOfNodes];

            for (int i = 0; i < graph.Length; ++i)
                graph[i] = new LinkedList<Tuple<int, int>>();

            // corresponds with image of weighted binary search tree
            addedge(0, 1, 3);
            addedge(0, 2, 6);
            addedge(0, 3, 5);
            addedge(1, 4, 9);
            addedge(1, 5, 8);
            addedge(2, 6, 12);
            addedge(2, 7, 14);
            addedge(3, 8, 7);
            addedge(8, 9, 5);
            addedge(8, 10, 6);
            addedge(9, 11, 1);
            addedge(9, 12, 10);
            addedge(9, 13, 2);

            int source = 0;
            int target = 9;

            best_first_search(source, target, numberOfNodes);
        }
    }
}