using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestEscape
{
    public interface IObstacleDrawingStrategy
    {
        public void Draw(GameObstacle obstacle);
        public string Name { get; }

    }
}
