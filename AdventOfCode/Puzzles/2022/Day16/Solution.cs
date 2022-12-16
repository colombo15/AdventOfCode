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

            _result = FindHighestPreasure(rooms["AA"], time, 0);

            throw new NotImplementedException();
        }

        public void PartTwo(string[] input)
        {
            throw new NotImplementedException();
        }

        private int FindHighestPreasure(Vertex room, int time, int currflow)
        {
            if (time <= 0)
                return currflow;

            if (room.Flow > 0) 
            {
                currflow += room.Flow * time;
                room.Flow = 0;
                time -= 1;
            }

            var highest = 0;
            foreach (var child in room.Vertices)
            {
                var retval = FindHighestPreasure(child, time - 1, currflow);
                if (retval > highest)
                    highest = retval;
            }

            return highest;
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
