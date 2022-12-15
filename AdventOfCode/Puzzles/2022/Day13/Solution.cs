using AdventOfCode.Common;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static AdventOfCode.Puzzles._2022.Day13.Solution;

namespace AdventOfCode.Puzzles._2022.Day13
{
    internal sealed partial class Solution : ISolution
    {
        private int _result;

        public void PartOne(string[] input)
        {
            for (var i = 0; i < input.Length; i += 3)
            {
                var packet1 = new Packet(input[i]);
                var packet2 = new Packet(input[i + 1]);
                if (ComparePackets(packet1, packet2))
                {
                    _result += (int)Math.Ceiling(i / 3.0) + 1;
                }
            }
        }

        public void PartTwo(string[] input)
        {
            var myPackets = new List<Packet>();

            for (var i = 0; i < input.Length; i += 3)
            {
                var packet1 = new Packet(input[i]);
                var packet2 = new Packet(input[i + 1]);

                myPackets.Add(packet1);
                myPackets.Add(packet2);
            }

            var two = new Packet("[[2]]");
            var six = new Packet("[[6]]");

            myPackets.Add(two);
            myPackets.Add(six);

            myPackets.Sort((x, y) => ComparePackets(x, y) ? -1 : 1);
            _result = (myPackets.IndexOf(two) + 1) * (myPackets.IndexOf(six) + 1);
        }

        public class Packet
        {
            public List<Node> Nodes { get; set; }

            public Packet(string packet) 
            {
                var open = 0;
                var closed = 0;

                Nodes = new List<Node>();

                for (var i = 1; i < packet.Length - 1; i++)
                {
                    if (packet[i] == '[')
                    {
                        var subStart = i + 1;
                        open++;
                        while (open > closed)
                        {
                            i++;
                            if (packet[i] == '[')
                                open++;
                            else if (packet[i] == ']')
                                closed++;
                        }

                        Nodes.Add(new Node(packet[subStart..i]));
                    }
                    else if (packet[i] != ',')
                    {
                        var nextComma = packet.IndexOf(',', i);
                        if (nextComma == -1) 
                            nextComma = packet.Length - 1;

                        Nodes.Add(new Node(int.Parse(packet.Substring(i, nextComma - i))));
                        i = nextComma;
                    }
                }
            }
        }

        public class Node
        {
            public int Value { get; set; }
            public List<Node>? Nodes { get; set; }

            public Node(string packet)
            {
                Value = -1;
                Nodes = new List<Node>();

                if (packet == "") return;

                if (!packet.Contains('['))
                {
                    foreach (var node in packet.Split(',').Select(x => new Node(int.Parse(x))))
                    {
                        Nodes.Add(node);
                    }
                }
                else
                {
                    var open = 0;
                    var closed = 0;

                    for (var i = 0; i < packet.Length; i++)
                    {
                        if (packet[i] == '[')
                        {
                            var subStart = i + 1;
                            open++;
                            while (open > closed)
                            {
                                i++;
                                if (packet[i] == '[')
                                    open++;
                                else if (packet[i] == ']')
                                    closed++;
                            }

                            Nodes.Add(new Node(packet[subStart..i]));
                        }
                        else if (packet[i] != ',')
                        {
                            var nextComma = packet.IndexOf(',', i);
                            if (nextComma == -1)
                                nextComma = packet.Length;

                            Nodes.Add(new Node(int.Parse(packet.Substring(i, nextComma - i))));
                            i = nextComma;
                        }
                    }
                }
            }

            public Node (int value) 
            { 
                Value = value;
            }

            public Node(Node node)
            {
                Value = -1;
                Nodes = new List<Node>() { node };
            }

            public List<int> GetListOfInt()
            {
                var list = new List<int>();

                if (Nodes != null)
                {
                    foreach (var node in Nodes)
                    {
                        node.GetListOfInt().ForEach(x => list.Add(x));
                    }
                }
                else
                {
                    list.Add(Value);
                }

                return list;
            }
        }

        private bool ComparePackets(Packet packet1, Packet packet2)
        {
            if (packet1.Nodes == null) return false;
            if (packet2.Nodes.Count == 0) return false;

            for (var i = 0; i < packet1.Nodes.Count; i++)
            {
                if (packet2.Nodes.Count <= i) return false;
                var comp = CompareNodes(packet1.Nodes[i], packet2.Nodes[i]);
                if (comp != 0) return comp == 1;
            }

            return true;
        }

        private int CompareNodes(Node node1, Node node2)
        {
            if (node1.Value != -1 && node2.Value != -1)
            {
                if (node1.Value < node2.Value)
                    return 1;
                if (node1.Value > node2.Value)
                    return -1;
                return 0;
            }

            if (node1.Value != -1 && node2.Value == -1) 
            {
                var newParentNode = new Node(new Node(node1.Value));
                return CompareNodes(newParentNode, node2);
            }

            if (node1.Value == -1 && node2.Value != -1)
            {
                var newParentNode = new Node(new Node(node2.Value));
                return CompareNodes(node1, newParentNode);
            }

            if (node1.Value == -1 && node2.Value != -1)
            {
                var newParentNode = new Node(new Node(node2.Value));
                return CompareNodes(node1, newParentNode);
            }

            if (node1.Value == -1 && node2.Value == -1)
            {
                for (var i = 0; i < node1.Nodes!.Count; i++)
                {
                    if (node2.Nodes!.Count == 0) 
                        return -1;
                    if (node2.Nodes!.Count <= i) 
                        return -1;

                    var comp = CompareNodes(node1.Nodes[i], node2.Nodes[i]);

                    if (comp != 0) 
                        return comp;
                }

                if (node1.Nodes.Count < node2.Nodes!.Count) return 1;
            }

            return 0;
        }
    }
}
