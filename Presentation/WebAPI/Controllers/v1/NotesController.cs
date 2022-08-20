using Application.Features.Notes.Commands.CreateNoteCommand;
using Application.Features.Notes.Commands.DeleteNoteCommand;
using Application.Features.Notes.Commands.UpdateNoteCommand;
using Application.Features.Notes.Queries.GetNoteQuery;
using Application.Features.Notes.Queries.GetNotesQuery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.v1
{
    [Authorize]
    [ApiVersion("1.0")]
    public class NotesController : BaseApiController
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreateNote(CreateNoteCommand command)
        {
            command.UserID = UserID;
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("delete/{noteID}")]
        public async Task<IActionResult> DeleteNote([FromRoute] string noteID)
        {
            return Ok(await Mediator.Send(new DeleteNoteCommand()
            {
                UserID = UserID,
                NoteID = noteID
            }));
        }

        [HttpPut("update/{noteID}")]
        public async Task<IActionResult> UpdateNote([FromRoute] string noteID, UpdateNoteCommand command)
        {
            command.UserID = UserID;
            command.NoteID = noteID;
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("get/{noteID}")]
        public async Task<IActionResult> Get([FromRoute] string noteID)
        {
            return Ok(await Mediator.Send(new GetNoteQuery() 
            { 
                UserID = UserID, 
                NoteID = noteID
            }));
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await Mediator.Send(new GetNotesQuery() { UserID = UserID}));
        }
    }
}
