using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMaze.maze
{
    internal class Room
    {
        private Door NorthDoor = new Door();
        private Door EastDoor = new Door();
        private Door SouthDoor = new Door();
        private Door WestDoor = new Door();

        List<Door> DoorsInRoom;

        public Room()
        {
            DoorsInRoom = new List<Door>()
            {
                NorthDoor,
                EastDoor,
                SouthDoor,
                WestDoor
            };
        }
    }
}
