using System;
using System.Collections.Generic;
using System.Text;

namespace EntertainmentMaze.maze
{
    internal class Room
    {
        internal Door NorthDoor { get; set; } = new Door(); 
        internal Door EastDoor { get; set; } = new Door();
        internal Door SouthDoor { get; set; } = new Door();
        internal Door WestDoor { get; set; } = new Door();

        public List<Door> ListOfDoors;

        public int RowLocation { get; }
        public int ColumnLocation { get; }
        public int[] PlayerLocation { get; }

        public Room(int rowLocation, int columnLocation, int numberOfTotalRows, int numberOfTotalColumns)
        {
            RowLocation = rowLocation;
            ColumnLocation = columnLocation;
            
            ListOfDoors = new List<Door>()
            {
                NorthDoor,
                EastDoor,
                SouthDoor,
                WestDoor
            };

            CreateRoomDescription(numberOfTotalRows, numberOfTotalColumns);
        }

        private void CreateRoomDescription(int numberOfTotalRows, int numberOfTotalColumns)
        {
            //Room in the first row
            if (RowLocation == 0)
            {
                NorthDoor = null;
                if (ColumnLocation == 0)
                {
                    WestDoor = null;
                }
                else if (ColumnLocation == (numberOfTotalRows - 1))
                {
                    EastDoor = null;
                }
            }

            //Room in the first column
            else if (ColumnLocation == 0)
            {
                WestDoor = null;
                if (RowLocation == (numberOfTotalColumns - 1))
                {
                    SouthDoor = null;
                }
            }

            //Room in the last row
            else if (RowLocation == (numberOfTotalColumns - 1))
            {
                SouthDoor = null;
                if (ColumnLocation == (numberOfTotalRows - 1))
                {
                    EastDoor = null;
                }
            }

            //Room in the last column
            else if (ColumnLocation == (numberOfTotalRows - 1) && (RowLocation == (numberOfTotalColumns - 1)))
            {
                EastDoor = null;
            }
        }

        public String GetTopOfRoom()
        {
            String top = "*";
            if (!(NorthDoor is null))
            {
                top += "-";
            }
            else
            {
                top += "*";
            }

            top += "*";
            return top;

        }

        public String GetMiddleOfRoom()
        {
            String middle = "";
            if (!(WestDoor is null))
            {
                middle += "|";
            }
            else
            {
                middle += "*";
            }

            middle += " ";

            if (!(EastDoor is null))
            {
                middle += "|";
            }
            else
            {
                middle += "*";
            }
            return middle;
        }

        public String GetBottomOfRoom()
        {
            String bottom = "*";

            if (!(SouthDoor is null))
            {
                bottom += "-";
            }
            else
            {
                bottom += "*";
            }

            bottom += "*";

            return bottom;
        }

        public String PrintRoom()
        {
            String stringRoom = "";

            stringRoom += GetTopOfRoom() + "\n";
            stringRoom += GetMiddleOfRoom() + "\n";
            stringRoom += GetBottomOfRoom();

            return stringRoom;
        }
    }
}
