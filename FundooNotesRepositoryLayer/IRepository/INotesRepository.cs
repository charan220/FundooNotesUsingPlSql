using FundooNotesModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesRepositoryLayer.IRepository
{
  public  interface INotesRepository
    {
        object AddNotes(NotesModel notesModel);
        bool DeleteNote(int noteId);

        bool Archive(int NoteId);
        bool TrashAndUnTrash(int NoteId);
        bool UpdateNote(int noteid, string title, string description);

    }
}
