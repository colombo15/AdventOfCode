using AdventOfCode.Common;
using System.Text.RegularExpressions;

namespace AdventOfCode.Puzzles._2022.Day16
{
    internal sealed partial class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            var rooms = ParseInput(input);
            var time = 30;
            var connections = new Dictionary<string, Dictionary<string, int>>();

            foreach (var room in rooms)
            {
                connections.Add(room.Key, new Dictionary<string, int>());

                foreach (var r in rooms)
                {
                    if (r.Key != room.Key)
                    {
                        connections[room.Key].Add(r.Key, BreathSearchFirst(room.Value, r.Value));
                        foreach (var kvp in rooms) kvp.Value.Parent = null;
                    }
                }
            }
            /*
            var retval = 0;
            while (closed.Any() || time < 0)
            {
                var temp = 0;
                Vertex? highest = null;

                foreach (var c in closed)
                {
                    var travel = connections[curr.Name][c.Name];
                    var totalP = (time - (travel + 1)) * c.Flow;

                    if (totalP > temp)
                    {
                        temp = totalP;
                        highest = c;
                    }
                }

                time -= connections[curr.Name][highest!.Name] + 1;
                curr = highest!;
                closed.Remove(curr);
                retval += temp;
            }
            */

            var curr = rooms["AA"];
            var closed = new List<Vertex>(rooms.Where(kvp => kvp.Value.Flow > 0).Select(kvp => kvp.Value));
            var open = new List<Vertex>();
            Vertex? highest = null;

            foreach (var c in closed)
            {
                var temp = 0;
                var travel = connections[curr.Name][c.Name];
                var totalP = (time - (travel + 1)) * c.Flow;

                if (totalP > temp)
                {
                    temp = totalP;
                    highest = c;
                }
            }
        }

        public void PartTwo(string[] input)
        {
            throw new NotImplementedException();
        }

        private int BreathSearchFirst(Vertex node, Vertex goal)
        {
            Vertex retval;
            var q = new Queue<Vertex>();
            var root = node;
            var explored = new HashSet<Vertex>();
            explored.Add(root);

            q.Enqueue(root);

            while (q.Count > 0)
            {
                retval = q.Dequeue();

                if (retval == goal)
                {
                    return GetParentLength(retval);
                }

                foreach (var edge in retval.Vertices)
                {
                    if (!explored.Contains(edge) && edge.Parent == null)
                    {
                        explored.Add(edge);
                        edge.Parent = retval;
                        q.Enqueue(edge);
                    }
                }
            }

            return GetParentLength(root);
        }

        private int GetParentLength(Vertex v)
        {
            if (v != null && v.Parent != null)
                return 1 + GetParentLength(v.Parent);
            return 0;
        }

        private Dictionary<string, Vertex> ParseInput(string[] input)
        {
            var rooms = new Dictionary<string, Vertex>();
            foreach (var item in input)
            {
                var match = MyRegex().Match(item);

                var roomName = match.Groups[1].Value;
                var flowRate = int.Parse(match.Groups[2].Value);
                var connectedRooms = match.Groups[3].Value.Split(", ");
                var room = new Vertex(roomName, flowRate);

                if (rooms.ContainsKey(roomName))
                {
                    rooms[roomName].Flow = flowRate;
                    room = rooms[roomName];
                }
                else
                {
                    rooms.Add(roomName, room);
                }

                foreach (var r in connectedRooms)
                {
                    if (!rooms.ContainsKey(r))
                        rooms.Add(r, new Vertex(r));
                    room.AddChildren(rooms[r]);
                }

            }
            return rooms;
        }

        [GeneratedRegex("Valve (.+) has flow rate=(\\d+); tunnels? leads? to valves? (.+)(,\\s\\.+)*")]
        private static partial Regex MyRegex();

        private sealed record class Vertex
        {
            public string Name { get; set; }
            public int Flow { get; set; }
            public List<Vertex> Vertices { get; set; }
            public Vertex? Parent { get; internal set; }

            public Vertex()
            {
                Vertices = new List<Vertex>();
            }

            public Vertex(string name, int flow = 0) 
            { 
                Vertices = new List<Vertex>();
                Name = name;
                Flow = flow;
            }

            public void AddChildren(Vertex child)
            {
                Vertices.Add(child);
            }
        }
    }
}
