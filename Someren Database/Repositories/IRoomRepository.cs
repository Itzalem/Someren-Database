using Someren_Database.Models;

namespace Someren_Database.Repositories
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRoomsAsync();

        Task<Room> GetRoomByIdAsync(int roomNumber);

        Task AddRoomAsync(Room room);

        Task<Room> UpdateRoomAsync(Room room);
        Task<Room> DeleteRoomAsync(int roomNumber);

        Task SaveAsync();
    }
}
