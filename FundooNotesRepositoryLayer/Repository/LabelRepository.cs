using FundooNotesModelLayer.Models;
using FundooNotesRepositoryLayer.IRepository;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace FundooNotesRepositoryLayer.Repository
{
    public class LabelRepository : IlabelRepository
    {
        private readonly IConfiguration configuration;
        public LabelRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public bool AddLabel(LabelModel labelModel)
        {
            try
            {
                
                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand("sp_AddLabel", _db))
                {
                    cmd.Connection = _db;

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("labelid", labelModel.LabelId);
                    cmd.Parameters.Add("noteid", labelModel.NoteId);
                    cmd.Parameters.Add("label", labelModel.Label);
                    
                    _db.Open();
                    int value = cmd.ExecuteNonQuery();
                    _db.Close();
                    if (value != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteLabel(int labelId)
        {
            try
            {

                using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                using (OracleCommand cmd = new OracleCommand())
                {
                    cmd.Connection = _db;
                    cmd.CommandText = "sp_DeleteLabel";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add("LabelId", labelId);
                    _db.Open();
                    int value = cmd.ExecuteNonQuery();
                    _db.Close();
                    if (value != 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public LabelModel GetLabelByNoteId(int NoteId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateLabel(int LabelId, string Details)
        {
          
                try
                {
                    using (var _db = new OracleConnection(configuration.GetConnectionString("UserDbConnection")))
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.Connection = _db;
                        cmd.CommandText = "sp_UpdateLabel";
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add("LabelId", LabelId);
                        cmd.Parameters.Add("Label", Details);
                        _db.Open();
                        int value = cmd.ExecuteNonQuery();
                        _db.Close();
                        if (value>=1 && Details !=null )
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                        
                    }
                }
             

                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }

        }

    }
}
