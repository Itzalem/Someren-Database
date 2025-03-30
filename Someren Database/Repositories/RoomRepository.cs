using Microsoft.EntityFrameworkCore;
using Someren_Database.Data;
using Someren_Database.Models;

namespace Someren_Database.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IStudentsRepository _studentsRepository;
        private readonly ITeachersRepository _teachersRepository;

        public RoomRepository(ApplicationDbContext context, IStudentsRepository studentsRepository, ITeachersRepository teachersRepository)
        {
            _context = context;
            _studentsRepository = studentsRepository;
            _teachersRepository = teachersRepository;
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
                bool hasStudents = _studentsRepository.ListStudents("").Any(s => s.RoomNumber == roomNumber);
                bool hasTeachers = _teachersRepository.ListTeachers().Any(t => t.RoomNumber == roomNumber);

                if (hasStudents || hasTeachers)
                {
                    return null;
                }

                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
                return room;  
            }

            return null;
        }

        public async Task<IEnumerable<Room>> GetAllRoomsAsync()
        {
			return await _context.Rooms.OrderBy(r => r.RoomNumber).ToListAsync();
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
                existingRoom.RoomType = room.RoomType;
                existingRoom.Building = room.Building;
                existingRoom.Floor = room.Floor;
                existingRoom.Size = room.Size;

                await _context.SaveChangesAsync();

                return existingRoom;
            }

            throw new Exception("Room not found.");
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
