using System.Text;
using static AdventOfCode._2023.Day15.Solution;

namespace AdventOfCode._2024.Day15;

public class Solution : ISolution
{
	public void Puzzle1(string[] input)
	{
		var warehouse = new Warehouse(input);
        warehouse.ExecuteRobot();
        Console.WriteLine(warehouse.GetGPS());
	}

	public void Puzzle2(string[] input)
	{
        var warehouse = new WideWarehouse(input);
        warehouse.ExecuteRobot();
        Console.WriteLine(warehouse.GetGPS());
    }

	public class Warehouse
	{
        private int _height;
        private int _width;
		private Dictionary<(int x, int y), char> _grid = [];
		private HashSet<(int x, int y)> _boxes = [];
		private string _robotInstructions;
		private (int x, int y) _robot;

		public Warehouse(string[] input)
		{
            _width = input[0].Length;
            var y = 0;
			for (y = 0; y < input.Length; y++)
			{
				if (input[y] == "") break;
				for (var x = 0; x < input[y].Length; x++)
				{
					if (input[y][x] != '@')
					{
						if (input[y][x] != 'O')
						{
                            _grid.Add((x, y), input[y][x]);
                        }
						else
						{
							_boxes.Add((x, y));
                            _grid.Add((x, y), '.');
                        }
					}
					else
					{
						_robot = (x, y);
                        _grid.Add(_robot, '.');
                    }
                }
			}
            _height = y;

            var sb = new StringBuilder();
			for (++y; y < input.Length; y++)
			{
				sb.Append(input[y]);
			}
			_robotInstructions = sb.ToString();
        }

        public void ExecuteRobot()
        {
            for (var i = 0; i < _robotInstructions.Length; i++)
            {
                //Print();
                Move(_robotInstructions[i]);
            }
        }

        public int GetGPS()
        {
            var result = 0;
            foreach (var (x, y) in _boxes)
            {
                result += (100 * y) + x;
            }
            return result;
        }

        public void Print()
        {
            Console.Clear();
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    var coord = (x, y);
                    if (_boxes.Contains(coord))
                    {
                        Console.Write('O');
                    }
                    else if (_robot == coord)
                    {
                        Console.Write('@');
                    }
                    else
                    {
                        Console.Write(_grid[coord]);
                    }
                }
                Console.WriteLine();
            }
        }

		private void Move(char inst)
		{
			switch (inst)
			{
				case '^':
					MoveNorth();
                    break;
                case '>':
                    MoveEast();
                    break;
                case 'v':
                    MoveSouth();
                    break;
                case '<':
                    MoveWest();
                    break;
				default:
					throw new NotImplementedException();
            }
		}

		private void MoveNorth()
		{
			var next = (_robot.x, _robot.y - 1);
			if (_grid[next] == '#') return;
            PushBoxNorth(next);
            if (!_boxes.Contains(next))
			{
				_robot = next;
			}
        }

		private void PushBoxNorth((int x, int y) box)
		{
			if (!_boxes.Contains(box)) return;
			var next = (box.x, box.y - 1);
			PushBoxNorth(next);
			if (_grid[next] != '#' && !_boxes.Contains(next))
			{
				_boxes.Remove(box);
				_boxes.Add(next);
			}
        }

        private void MoveEast()
        {
            var next = (_robot.x + 1, _robot.y);
            if (_grid[next] == '#') return;
            PushBoxEast(next);
            if (!_boxes.Contains(next))
            {
                _robot = next;
            }
        }

        private void PushBoxEast((int x, int y) box)
        {
            if (!_boxes.Contains(box)) return;
            var next = (box.x + 1, box.y);
            PushBoxEast(next);
            if (_grid[next] != '#' && !_boxes.Contains(next))
            {
                _boxes.Remove(box);
                _boxes.Add(next);
            }
        }

        private void MoveSouth()
        {
            var next = (_robot.x, _robot.y + 1);
            if (_grid[next] == '#') return;
            PushBoxSouth(next);
            if (!_boxes.Contains(next))
            {
                _robot = next;
            }
        }

        private void PushBoxSouth((int x, int y) box)
        {
            if (!_boxes.Contains(box)) return;
            var next = (box.x, box.y + 1);
            PushBoxSouth(next);
            if (_grid[next] != '#' && !_boxes.Contains(next))
            {
                _boxes.Remove(box);
                _boxes.Add(next);
            }
        }

        private void MoveWest()
        {
            var next = (_robot.x - 1, _robot.y);
            if (_grid[next] == '#') return;
            PushBoxWest(next);
            if (!_boxes.Contains(next))
            {
                _robot = next;
            }
        }

        private void PushBoxWest((int x, int y) box)
        {
            if (!_boxes.Contains(box)) return;
            var next = (box.x - 1, box.y);
            PushBoxWest(next);
            if (_grid[next] != '#' && !_boxes.Contains(next))
            {
                _boxes.Remove(box);
                _boxes.Add(next);
            }
        }
    }

    public class WideWarehouse
    {
        private int _height;
        private int _width;
        private Dictionary<(int x, int y), char> _grid = [];
        private HashSet<(int x, int y)> _boxes = [];
        private HashSet<(int x, int y)> _boxVolume = [];
        private Dictionary<(int x, int y), (int x, int y)> _boxPair = [];
        private string _robotInstructions;
        private (int x, int y) _robot;

        public WideWarehouse(string[] input)
        {
            _width = input[0].Length * 2;
            var y = 0;
            for (y = 0; y < input.Length; y++)
            {
                if (input[y] == "") break;
                var faxtX = 0;
                for (var x = 0; x < input[y].Length; x++)
                {
                    if (input[y][x] != '@')
                    {
                        if (input[y][x] != 'O')
                        {
                            _grid.Add((faxtX++, y), input[y][x]);
                            _grid.Add((faxtX++, y), input[y][x]);
                        }
                        else
                        {
                            var left = (faxtX++, y);
                            var right = (faxtX++, y);
                            _boxes.Add(left);
                            _boxVolume.Add(left);
                            _boxVolume.Add(right);
                            _boxPair.Add(left, right);
                            _boxPair.Add(right, left);
                            _grid.Add(left, '.');
                            _grid.Add(right, '.');
                        }
                    }
                    else
                    {
                        _robot = (faxtX++, y);
                        _grid.Add(_robot, '.');
                        _grid.Add((faxtX++, _robot.y), '.');
                    }
                }
            }
            _height = y;

            var sb = new StringBuilder();
            for (++y; y < input.Length; y++)
            {
                sb.Append(input[y]);
            }
            _robotInstructions = sb.ToString();
        }

        public void ExecuteRobot()
        {
            for (var i = 0; i < _robotInstructions.Length; i++)
            {
                //Print();
                //Console.WriteLine($" Move {i} - {_robotInstructions[i]} ");
                Move(_robotInstructions[i]);
            }
        }

        public int GetGPS()
        {
            var result = 0;
            foreach (var (x, y) in _boxes)
            {
                result += (100 * y) + x;
            }
            return result;
        }

        public void Print()
        {
            Console.Clear();
            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
                    var coord = (x, y);
                    if (_boxes.Contains(coord))
                    {
                        Console.Write('[');
                    }
                    else if (_boxVolume.Contains(coord))
                    {
                        Console.Write(']');
                    }
                    else if (_robot == coord)
                    {
                        Console.Write('@');
                    }
                    else
                    {
                        Console.Write(_grid[coord]);
                    }
                }
                Console.WriteLine();
            }
        }

        private void Move(char inst)
        {
            switch (inst)
            {
                case '^':
                    MoveNorth();
                    break;
                case '>':
                    MoveEast();
                    break;
                case 'v':
                    MoveSouth();
                    break;
                case '<':
                    MoveWest();
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private void MoveNorth()
        {
            var next = (_robot.x, _robot.y - 1);
            if (_grid[next] == '#') return;
            PushBoxNorth(next);
        }

        private void PushBoxNorth((int x, int y) box)
        {
            var boxes = new List<(int x, int y)>();
            var cache = new HashSet<(int x, int y)>();
            if (Push(box))
            {
                boxes = boxes.OrderBy(x => x.y).ToList();
                while (boxes.Count > 0)
                {
                    var b = boxes.First();
                    boxes.RemoveAt(0);
                    var b2 = (x: b.x + 1, b.y);
                    var next = (b.x, b.y - 1);
                    var next2 = (b.x + 1, b.y - 1);
                    _boxVolume.Remove(b);
                    _boxVolume.Add(next);
                    _boxVolume.Remove(b2);
                    _boxVolume.Add(next2);
                    if (_boxes.Remove(b))
                    {
                        _boxes.Add(next);
                    }
                    _boxPair.Remove(b);
                    _boxPair.Remove(b2);
                    _boxPair.Add(next, next2);
                    _boxPair.Add(next2, next);
                }
                _robot = box;
            }

            bool Push((int x, int y) box)
            {
                if (!_boxVolume.Contains(box)) return true;
                if (!_boxes.Contains(box)) box = _boxPair[box];
                var next = (box.x, box.y - 1);
                var next2 = (box.x + 1, box.y - 1);
                if (_grid[next] == '#' || _grid[next2] == '#')
                {
                    return false;
                }
                else
                {
                    if (cache.Add(box))
                    {
                        boxes.Add(box);
                    }
                    return Push(next) && Push(next2);
                }
            }
        }

        private void MoveEast()
        {
            var next = (_robot.x + 1, _robot.y);
            if (_grid[next] == '#') return;
            PushBoxEast(next);
            if (!_boxVolume.Contains(next))
            {
                _robot = next;
            }
        }

        private void PushBoxEast((int x, int y) box)
        {
            if (!_boxVolume.Contains(box)) return;
            var next = (box.x + 1, box.y);
            PushBoxEast(next);
            if (_grid[next] != '#' && !_boxVolume.Contains(next))
            {
                _boxVolume.Remove(box);
                _boxVolume.Add(next);
                if (_boxes.Remove(box))
                {
                    _boxes.Add(next);
                }
                var pair = _boxPair[box];
                _boxPair[pair] = next;
                _boxPair.Remove(box);
                _boxPair.Add(next, pair);
            }
        }

        private void MoveSouth()
        {
            var next = (_robot.x, _robot.y + 1);
            if (_grid[next] == '#') return;
            PushBoxSouth(next);
        }

        private void PushBoxSouth((int x, int y) box)
        {
            var boxes = new List<(int x, int y)>();
            var cache = new HashSet<(int x, int y)>();

            if (Push(box))
            {
                boxes = boxes.OrderByDescending(x => x.y).ToList();
                while (boxes.Count > 0)
                {
                    var b = boxes.First();
                    boxes.RemoveAt(0);
                    var b2 = (x: b.x + 1, b.y);
                    var next = (b.x, b.y + 1);
                    var next2 = (b.x + 1, b.y + 1);
                    _boxVolume.Remove(b);
                    _boxVolume.Add(next);
                    _boxVolume.Remove(b2);
                    _boxVolume.Add(next2);
                    if (_boxes.Remove(b))
                    {
                        _boxes.Add(next);
                    }
                    _boxPair.Remove(b);
                    _boxPair.Remove(b2);
                    _boxPair.Add(next, next2);
                    _boxPair.Add(next2, next);
                }
                _robot = box;
            }

            bool Push((int x, int y) box)
            {
                if (!_boxVolume.Contains(box)) return true;
                if (!_boxes.Contains(box)) box = _boxPair[box];
                var next = (box.x, box.y + 1);
                var next2 = (box.x + 1, box.y + 1);
                if (_grid[next] == '#' || _grid[next2] == '#')
                {
                    return false;
                }
                else
                {
                    if (cache.Add(box))
                    {
                        boxes.Add(box);
                    }
                    return Push(next) && Push(next2);
                }
            }
        }

        private void MoveWest()
        {
            var next = (_robot.x - 1, _robot.y);
            if (_grid[next] == '#') return;
            PushBoxWest(next);
            if (!_boxVolume.Contains(next))
            {
                _robot = next;
            }
        }

        private void PushBoxWest((int x, int y) box)
        {
            if (!_boxVolume.Contains(box)) return;
            var next = (box.x - 1, box.y);
            PushBoxWest(next);
            if (_grid[next] != '#' && !_boxVolume.Contains(next))
            {
                _boxVolume.Remove(box);
                _boxVolume.Add(next);
                if (_boxes.Remove(box))
                {
                    _boxes.Add(next);
                }
                var pair = _boxPair[box];
                _boxPair[pair] = next;
                _boxPair.Remove(box);
                _boxPair.Add(next, pair);
            }
        }
    }
}
