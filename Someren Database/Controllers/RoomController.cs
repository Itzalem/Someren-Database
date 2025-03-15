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
        
        //display all rooms as a list
        public async Task<IActionResult> RoomIndex()
        {
            var rooms = await _roomReposiroty.GetAllRoomsAsync();
            return View(rooms);
        }

        //getting create view
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //posting create
        [HttpPost]
        public async Task<IActionResult> Create(Room room)
        {
            if (room != null && ModelState.IsValid)
            {
                //add room to db
                await _roomReposiroty.AddRoomAsync(room);
                return RedirectToAction("RoomIndex");
            }

            //return to create view if room is not valid
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
            // var room = await _roomReposiroty.GetRoomByIdAsync((int)roomnumber);

            if (ModelState.IsValid)
            {
                await _roomReposiroty.UpdateRoomAsync(room);
                return RedirectToAction("RoomIndex");
            }

            return View(room);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int roomnumber)
        {
            var room = await _roomReposiroty.GetRoomByIdAsync(roomnumber);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int roomnumber)
        {
            var room = await _roomReposiroty.GetRoomByIdAsync(roomnumber);
            if (room != null)
            {
                await _roomReposiroty.DeleteRoomAsync(roomnumber);
            }

            return RedirectToAction("RoomIndex");
        }
    }
}
