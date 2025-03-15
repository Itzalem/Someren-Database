using Microsoft.EntityFrameworkCore;
using Someren_Database.Data;
using Someren_Database.Models;

namespace Someren_Database.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;

        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task AddRoomAsync(Room room)
        {
            await _context.Rooms.AddAsync(room);
            await _context.SaveChangesAsync();
        }

        public async Task<Room> DeleteRoomAsync(int roomNumber)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
                return room;  // Return the deleted room (optional)
            }

            return null;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
			return await _context.Rooms.ToListAsync();
		}

		public async Task<Room> GetRoomByIdAsync(int roomNumber)
        {
			return await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == roomNumber);
		}

        public async Task<Room> UpdateRoomAsync(Room room)
        {
            var existingRoom = await _context.Rooms.FirstOrDefaultAsync(r => r.RoomNumber == room.RoomNumber);

            if (existingRoom != null)
            {
                // Update the room properties with the new values
                existingRoom.RoomType = room.RoomType;
                existingRoom.Building = room.Building;
                existingRoom.Floor = room.Floor;
                existingRoom.Size = room.Size;

                // Save changes to the database
                await _context.SaveChangesAsync();

                // Return the updated room
                return existingRoom;
            }

            // If room is not found, you could throw an exception or return null
            throw new Exception("Room not found.");
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
