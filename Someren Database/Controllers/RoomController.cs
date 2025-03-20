using Microsoft.AspNetCore.Mvc;
using Someren_Database.Models;
using Someren_Database.Repositories;

namespace Someren_Database.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomRepository _roomReposiroty;

        public RoomController(IRoomRepository roomReposiroty)
        {
            _roomReposiroty = roomReposiroty;
        }

        // Display all rooms or search by RoomNumber
        public async Task<IActionResult> RoomIndex(int? roomNumber)
        {
            IEnumerable<Room> rooms;

            if (roomNumber.HasValue)
            {
                // Search by RoomNumber using GetRoomByIdAsync
                var room = await _roomReposiroty.GetRoomByIdAsync(roomNumber.Value);
                rooms = room != null ? new List<Room> { room } : new List<Room>(); // Return the found room or an empty list if not found
            }
            else
            {
                // Show all rooms if no search query
                rooms = await _roomReposiroty.GetAllRoomsAsync();
            }

            return View(rooms);
        }

        // Getting create view
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Posting create
        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {
            if (room != null && ModelState.IsValid)
            {
                // Add room to db
                await _roomReposiroty.AddRoomAsync(room);
                return RedirectToAction("RoomIndex");
            }

            // Return to create view if room is not valid
            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int roomnumber)
        {
            var room = await _roomReposiroty.GetRoomByIdAsync(roomnumber);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int roomnumber, Room room)
        {
            if (ModelState.IsValid)
            {
                await _roomReposiroty.UpdateRoomAsync(room);
                return RedirectToAction("RoomIndex");
            }

            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int roomNumber)
        {
            var room = await _roomReposiroty.GetRoomByIdAsync(roomNumber);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int roomNumber)
        {
            var room = await _roomReposiroty.GetRoomByIdAsync(roomNumber);
            if (room != null)
            {
                await _roomReposiroty.DeleteRoomAsync(roomNumber);
            }

            return RedirectToAction("RoomIndex");
        }


		// Action to search by Room Number
		[HttpGet]
		public async Task<IActionResult> EditRoomSearch(int? roomNumber)
		{
			if (roomNumber.HasValue)
			{
				var room = await _roomReposiroty.GetRoomByIdAsync(roomNumber.Value);
				if (room != null)
				{
					// Return the found room to the edit view
					return View("Edit", room);
				}
				else
				{
					// If no room is found, show a message
					ViewBag.Message = "Room not found.";
				}
			}

			// If no room number is entered, or no room is found, return to the search page
			return View();
		}


        // Action to search for a Room and display details for deletion
        [HttpGet]
        public async Task<IActionResult> DeleteRoomSearch(int? roomNumber)
        {
            if (roomNumber.HasValue)
            {
                var room = await _roomReposiroty.GetRoomByIdAsync(roomNumber.Value);
                if (room != null)
                {
                    // Redirect to the Delete action for room deletion
                    return RedirectToAction("Delete", new { roomNumber = room.RoomNumber });
                }
                else
                {
                    // If no room is found, show a message
                    ViewBag.Message = "Room not found.";
                }
            }

            // Return the view for searching if no room number is entered or if no room is found
            return View();
        }


    }
}
