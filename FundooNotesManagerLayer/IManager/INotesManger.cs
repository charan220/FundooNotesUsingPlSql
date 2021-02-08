using FundooNotesModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesManagerLayer.IManager
{
    public interface INotesManger
    {
        object AddNotes(NotesModel notesModel);
        bool DeleteNote(int noteId);
        bool Archive(int NoteId);
        bool TrashAndUnTrash(int NoteId);
        bool UpdateNote(int noteid, string title, string description);

    }
}
