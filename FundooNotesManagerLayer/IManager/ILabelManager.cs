using FundooNotesModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesManagerLayer.IManager
{
   public interface ILabelManager
    {
        bool AddLabel(LabelModel labelModel);
        bool DeleteLabel(int labelid);
        bool UpdateLabel(int LabelId, string Details);

    }
}
