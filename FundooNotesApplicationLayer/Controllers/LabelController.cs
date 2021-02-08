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
    public class LabelController : ControllerBase
    {
        public ILabelManager labelManager;
        public LabelController(ILabelManager labelManager)
        {
            this.labelManager = labelManager;
        }
        [HttpPost]
        public IActionResult AddLabel(LabelModel labelModel)
        {
            try
            {
                var item = this.labelManager.AddLabel(labelModel);
                if (item == true)
                {
                    return this.Ok(new { Status = true, Message = "label added successfully", Data = item });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "label added unsuccessfully", Data = item });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }


        }
        [HttpDelete]
        [Route("{labelId}")]
        public IActionResult DeleteLabel(int labelId)
        {
            try
            {
                var result = this.labelManager.DeleteLabel(labelId);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "label deleted successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "label deleted unsuccessfully", Data = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }

        }
        [HttpPut]
        public IActionResult UpdateLabel(int LabelId,string Details)
        {
            try
            {
                var result = this.labelManager.UpdateLabel(LabelId,Details);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "label updated successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "label updated unsuccessfully", Data = result });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }

        }
    }
}