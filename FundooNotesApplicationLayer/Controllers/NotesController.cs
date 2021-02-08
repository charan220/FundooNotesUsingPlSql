using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundooNotesManagerLayer.IManager;
using FundooNotesModelLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundooNotesApplicationLayer.Controllers
{
  
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        public INotesManger notesManager;
        public NotesController(INotesManger notesManager)
        {
            this.notesManager = notesManager;
        }
        [HttpPost]
        public IActionResult AddNotes(NotesModel noteModel)
        {
            try
            {
                var item = this.notesManager.AddNotes(noteModel);
                if (item != null)
                {
                    return this.Ok(new { Status = true, Message = "notes added successfully", Data = item });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "notes added unsuccessfully", Data = item });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }


        }
        [HttpDelete]
        [Route("{noteId}")]
        public IActionResult DeleteNote(int noteId)
        {
            try
            {
                var result = this.notesManager.DeleteNote(noteId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "note deleted successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "note deleted unsuccessfully", Data = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }

        }
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateNote(int noteid, string title, string description)
        {
            try
            {
                var result = this.notesManager.UpdateNote(noteid,title,description);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "note updated successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "note updated unsuccessfully", Data = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }

        }
        [HttpPut]
        [Route("Archive")]
        public IActionResult Archive(int noteid)
        {
            try
            {
                var result = this.notesManager.Archive(noteid);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "note archived successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "note archived unsuccessfully", Data = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }

        }
        [HttpPut]
        [Route("trashoruntrash")]
        public IActionResult TrashAndUnTrash(int noteid)
        {
            try
            {
                var result = this.notesManager.TrashAndUnTrash(noteid);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "note trash successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "note trash unsuccessfully", Data = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }

        }

    }
}