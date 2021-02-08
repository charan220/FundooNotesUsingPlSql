using FundooNotesManagerLayer.IManager;
using FundooNotesModelLayer.Models;
using FundooNotesRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesManagerLayer.Manager
{
    public class NotesManager :INotesManger
    {
        INotesRepository notesRepository;
        public  NotesManager(INotesRepository notesRepository)
        {
            this.notesRepository = notesRepository;
        }
        public object AddNotes(NotesModel notesModel)
        {
            return this.notesRepository.AddNotes(notesModel);
        }

        public bool Archive(int NoteId)
        {
            return this.notesRepository.Archive(NoteId);
        }

        public bool DeleteNote(int noteId)
        {
            return this.notesRepository.DeleteNote(noteId);
        }

        public bool TrashAndUnTrash(int NoteId)
        {
            return this.notesRepository.TrashAndUnTrash(NoteId);
        }

        public bool UpdateNote(int noteid, string title, string description)
        {
            return this.notesRepository.UpdateNote(noteid, title, description);
        }
    }
}
