using FundooNotesManagerLayer.IManager;
using FundooNotesModelLayer.Models;
using FundooNotesRepositoryLayer.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooNotesManagerLayer.Manager
{
    public class LabelManager : ILabelManager

    {
        IlabelRepository labelRepository;
        public LabelManager(IlabelRepository labelRepository)
        {
            this.labelRepository = labelRepository;
        }
        public bool AddLabel(LabelModel labelModel)
        {
            return this.labelRepository.AddLabel(labelModel);
        }

        public bool DeleteLabel(int labelid)
        {
            return this.labelRepository.DeleteLabel(labelid);
        }

        public bool UpdateLabel(int LabelId, string Details)
        {
            return this.labelRepository.UpdateLabel(LabelId, Details);
        }
    }
}
