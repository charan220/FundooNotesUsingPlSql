using FundooNotesModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesRepositoryLayer.IRepository
{
   public interface IlabelRepository
    {
        bool AddLabel(LabelModel labelModel);
        LabelModel GetLabelByNoteId(int NoteId);
        bool DeleteLabel(int LabelId);
        bool UpdateLabel(int LabelId, string Details);
    }
}
