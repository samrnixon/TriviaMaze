using EntertainmentMaze.Database;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EntertainmentMaze.maze
{
    [Serializable]
    public class Maze : ISerializable
    {
        private enum Location
        {
            Row = 0,
            Column = 1
        }

        private Room[,] _Rooms;
        public Player Player { get; set; }
        private int[] PlayerLocation = new int[2];
        public int Rows { get; set; }
        public int Columns { get; set; }

        public static MazeBuilder CreateBuilder() => new MazeBuilder();
        private Room GetHeroLocation() => (_Rooms[PlayerLocation[(int)Location.Row], PlayerLocation[(int)Location.Column]]);

        
        public Maze(int rows, int columns, Player player)
        {
            Rows = rows;
            Columns = columns;
            Player = player;
        }

        public Maze() { }

        public void CompleteBuild()
        {
            BuildRooms();
            SetHeroLocation(0, 0);
        }

        private void BuildRooms()
        {
            _Rooms = new Room[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _Rooms[i, j] = new Room(i, j, Rows, Columns);
                }
            }
        }

    /*        public Room[,] GetRooms()
            {
                return this._Rooms;
            }*/

        public Room GetLocation()
        {
            return GetHeroLocation();
        }

        private void SetHeroLocation(int rowLocation, int columnLocation)
        {
            PlayerLocation[(int)Location.Row] = rowLocation;
            PlayerLocation[(int)Location.Column] = columnLocation;
            GetHeroLocation().SetPlayerInRoom();
        }


        public void MoveHero(String direction)
        {

            var currentRoomHeroLocatedIn = GetHeroLocation();

            if (direction == "N")
            {
                if (!(currentRoomHeroLocatedIn.NorthDoor is null))
                {
                    GetHeroLocation().RemovePreviousPlayerLocation();
                    SetHeroLocation(PlayerLocation[(int)Location.Row] - 1, PlayerLocation[(int)Location.Column]);
                }
            }
            else if (direction == "E")
            {
                if (!(currentRoomHeroLocatedIn.EastDoor is null))
                {
                    GetHeroLocation().RemovePreviousPlayerLocation();
                    SetHeroLocation(PlayerLocation[(int)Location.Row], PlayerLocation[(int)Location.Column] + 1);
                }
            }
            else if (direction == "S")
            {
                if (!(currentRoomHeroLocatedIn.SouthDoor is null))
                {
                    GetHeroLocation().RemovePreviousPlayerLocation();
                    SetHeroLocation(PlayerLocation[(int)Location.Row] + 1, PlayerLocation[(int)Location.Column]);
                }
            }
            else if (direction == "W")
            {
                if (!(currentRoomHeroLocatedIn.WestDoor is null))
                {
                    GetHeroLocation().RemovePreviousPlayerLocation();
                    SetHeroLocation(PlayerLocation[(int)Location.Row], PlayerLocation[(int)Location.Column] - 1);
                }
            }

        }

        internal void DisplayHeroLocation()
        {
            Console.WriteLine($"{(PlayerLocation[(int)Location.Row] + 1).ToString()}, {(PlayerLocation[(int)Location.Column] + 1).ToString()}");
        }

        public string PrintMaze()
        {
            String entireDungeon = "";
            int j;

            for (int i = 0; i < Rows; i++)
            {

                for (j = 0; j < Columns; j++)
                {
                    entireDungeon += _Rooms[i, j].GetTopOfRoom();
                }

                entireDungeon += "\n";

                for (j = 0; j < Columns; j++)
                {
                    entireDungeon += _Rooms[i, j].GetMiddleOfRoom();
                }

                entireDungeon += "\n";

                for (j = 0; j < Columns; j++)
                {
                    entireDungeon += _Rooms[i, j].GetBottomOfRoom();
                }

                entireDungeon += "\n";

            }

            return entireDungeon;
        }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("_Rooms", _Rooms, typeof(Room[,]));
            info.AddValue("Player", Player, typeof(Player));
            info.AddValue("PlayerLocation", PlayerLocation, typeof(int[]));
            info.AddValue("Rows", Rows, typeof(int));
            info.AddValue("Columns", Columns, typeof(int));
        }

        [OnDeserialized]
        private void OnDeserialization(SerializationInfo info, StreamingContext context)
        {
            _Rooms = (Room[,])info.GetValue("_Rooms", typeof(Room[,]));
            Player = (Player)info.GetValue("Player", typeof(Player));
            PlayerLocation = (int[])info.GetValue("props", typeof(int[]));
            Rows = (int)info.GetValue("Rows", typeof(int));
            Columns = (int)info.GetValue("Columns", typeof(int));
        }




/*        public Maze Load()
        {
            Source.Position = 0;

            using var reader = new StreamReader(Source, leaveOpen: true);
            try
            {
                string? jsonData;

                while ((jsonData = reader.ReadLine()) != null)
                {
                    if (jsonData is "")
                    {
                        return mailboxes;
                    }
                    mailboxes.Add(JsonConvert.DeserializeObject<Mailbox>(jsonData));
                }
            }
            catch (JsonReaderException)
            {
                return null;
            }

            reader.Dispose();
            return mailboxes;
        }

        public void Save(List<Mailbox> mailboxes)
        {
            if (mailboxes is null)
            {
                throw new ArgumentNullException(nameof(mailboxes));
            }

            Source.Position = 0;
            using var writer = new StreamWriter(Source, leaveOpen: true);

            foreach (Mailbox mb in mailboxes)
            {
                string data = JsonConvert.SerializeObject(mb);
                writer.WriteLine(data);
            }
            writer.Dispose();
        }*/
    }
}
