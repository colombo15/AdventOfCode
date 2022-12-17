using AdventOfCode.Common;
using System.Diagnostics;
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
            Dictionary<string, Dictionary<string, int>> connections = new();

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

            var closed = new List<Vertex>(rooms.Where(kvp => kvp.Value.Flow > 0).Select(kvp => kvp.Value));

            _result = HighestPreasure(rooms["AA"], closed, time);

            int HighestPreasure(Vertex node, List<Vertex> available, int t, int runningTotal = 0)
            {
                if (t < 0) return 0;
                runningTotal += t * node.Flow;

                if (available.Count == 0) 
                    return runningTotal;
                var highest = runningTotal;

                foreach (var item in available)
                {
                    var newAvailable = new List<Vertex>(available);
                    newAvailable.Remove(item);
                    var travelTime = connections[node.Name][item.Name];
                    var temp = HighestPreasure(item, newAvailable, t - travelTime - 1, runningTotal);
                    if (temp > highest)
                        highest = temp;
                }

                return highest;
            }
        }

        public void PartTwo(string[] input)
        {
            var rooms = ParseInput(input);
            var time = 30;
            Dictionary<string, Dictionary<string, int>> connections = new();

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

            var closed = new List<Vertex>(rooms.Where(kvp => kvp.Value.Flow > 0).Select(kvp => kvp.Value));
            var stuff = new List<Vertex>();
            var res = HighestPreasure(rooms["AA"], closed, time);
            _result = res;

            int HighestPreasure(Vertex node, List<Vertex> available, int t, int runningTotal = 0)
            {
                if (t < 0) return 0;
                runningTotal += t * node.Flow;

                if (available.Count == 0)
                {
                    if (runningTotal == 1751) stuff.Add(node);
                    return runningTotal;
                }

                var highest = runningTotal;

                foreach (var item in available)
                {
                    var newAvailable = new List<Vertex>(available);
                    newAvailable.Remove(item);
                    var travelTime = connections[node.Name][item.Name];
                    var temp = HighestPreasure(item, newAvailable, t - travelTime - 1, runningTotal);
                    if (temp > highest)
                        highest = temp;
                }

                if (highest == 1751) stuff.Add(node);

                return highest;
            }

            //int HighestPreasure(Vertex node, Vertex eleNode, List<Vertex> available, int t, int t2, int runningTotal = 0)
            //{
            //    if (t < 0 && t2 < 0) return 0;

            //    if (t >= 0)
            //        runningTotal += t * node.Flow;

            //    if (t2 >= 0)
            //        runningTotal += t2 * eleNode.Flow;

            //    if (available.Count == 0)
            //        return runningTotal;

            //    var highest = runningTotal;

            //    foreach (var item in available)
            //    {
            //        var newAvailable = new List<Vertex>(available);
            //        newAvailable.Remove(item);
            //        foreach (var item2 in newAvailable)
            //        {
            //            var newAvailable2 = new List<Vertex>(newAvailable);
            //            var travelTime = -1;
            //            var travelTime2 = -1;

            //            if (t >= 0)
            //            {
            //                newAvailable2.Remove(item);
            //                travelTime = t - connections[node.Name][item.Name] - 1;
            //            }
            //            if (t2 >= 0)
            //            {
            //                newAvailable2.Remove(item2);
            //                travelTime2 = t2 - connections[eleNode.Name][item2.Name] - 1;
            //            }

            //            var temp = HighestPreasure(item, item2, newAvailable2, travelTime, travelTime2, runningTotal);
            //            if (temp > highest)
            //                highest = temp;
            //        }
            //    }

            //    return highest;
            //}
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
